using AutoMapper;
using Hotel_Cabins.DTOs.CabinsDTOs;
using Hotel_Cabins.Models;
using Hotel_Cabins.Repository.IRepository;
using Hotel_Cabins.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Hotel_Cabins.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CabinsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICabinsRepository _dbCabins;
        internal APIResponse res;

        public CabinsController(IMapper mapper ,ICabinsRepository dbCabins )
        {
            _mapper = mapper;
            _dbCabins = dbCabins;
            res = new();
        }

        [HttpGet("topFiveCheapest")]
        [ResponseCache(Duration =30)]
        public async Task<ActionResult<APIResponse>> GetTopFiveCheapest()
        {
            int page = 1;
            int pageSize = 5;
            var cabinsModels = await _dbCabins.GetAllAsync(page: page, pageSize: pageSize);

            res.Result = _mapper.Map<List<CabinDTO>>(cabinsModels);
            res.StatusCode = HttpStatusCode.OK;

            return Ok(res);
        }

        [HttpGet("getCabinsStats")]
        public async Task<ActionResult<APIResponse>> GetCabinsStats()
        {
            var cabinsStats= await _dbCabins.GetCabinsStats();

            res.Result = cabinsStats;
            res.StatusCode = HttpStatusCode.OK;

            return Ok(res);
        }


        [HttpGet]
        [ResponseCache(Duration = 30)]
        public async Task<ActionResult<APIResponse>> GetAllCabins(int page = 1, int pageSize = 10)
        {
            var cabinsModels = await _dbCabins.GetAllAsync(page: page, pageSize: pageSize);

            var pagination = new Pagination()
            {
                Page = page,
                PageSize = pageSize,
            };

            Response.Headers.Add("x-Pagination", JsonSerializer.Serialize(pagination));


            res.Result = _mapper.Map<List<CabinDTO>>(cabinsModels);
            res.StatusCode = HttpStatusCode.OK;

            return Ok(res);
        }


        [HttpGet("{id:int}", Name = "GetCabin")]
        public async Task<ActionResult<APIResponse>> GetCabin(int id)
        {
            var cabinModel = await _dbCabins.GetOneAsync(g => g.Id == id);

            if (cabinModel is null) throw new AppError("No Cabin Found With This Id", HttpStatusCode.NotFound);


            res.Result = _mapper.Map<CabinDTO>(cabinModel);
            res.StatusCode = HttpStatusCode.OK;

            return Ok(res);
        }


        [HttpPost]
        public async Task<ActionResult<APIResponse>> CreateCabin(CabinCreateDTO cabinCreateDTO)
        {
            if ((await _dbCabins.GetOneAsync(v => v.Name == cabinCreateDTO.Name)) != null) throw new AppError("Cabin Name is existed", HttpStatusCode.Conflict);

            var cabinModel = _mapper.Map<Cabin>(cabinCreateDTO);

            await _dbCabins.CreateOneAsync(cabinModel);

            // Upload Image
            if (cabinCreateDTO.Image != null)
            {
                string fileName = "cabin" + cabinModel.Id + Path.GetExtension(cabinCreateDTO.Image.FileName);
                string filePath = @"wwwroot\cabinsImages\" + fileName;

                var directoryLocation = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                FileInfo file = new FileInfo(directoryLocation);

                if (file.Exists) file.Delete();

                using (var fileStream = new FileStream(directoryLocation, FileMode.Create))
                {
                    cabinCreateDTO.Image.CopyTo(fileStream);
                }

                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                cabinModel.ImageUrl = baseUrl + "/cabinsImages/" + fileName;
                cabinModel.ImageLocalPath = filePath;

            }
            else
            {
                cabinModel.ImageUrl = "https://placehold.co/600x400";

            }

            await _dbCabins.UpdateOneAsync(cabinModel);


            var guest = _mapper.Map<CabinDTO>(cabinModel);

            res.Result = _mapper.Map<CabinDTO>(cabinModel);
            res.StatusCode = HttpStatusCode.Created;

            return CreatedAtRoute("GetCabin", new { id = cabinModel.Id }, res);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<APIResponse>> DeleteOneGuest(int id)
        {
            var cabinModel = await _dbCabins.GetOneAsync(g => g.Id == id);

            if (cabinModel is null) throw new AppError("No Cabin Found With This Id", HttpStatusCode.NotFound);

            await _dbCabins.DeleteOneAsync(cabinModel);

            if (!string.IsNullOrEmpty(cabinModel.ImageLocalPath))
            {
                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), cabinModel.ImageLocalPath);

                FileInfo file = new FileInfo(oldFilePath);

                if (file.Exists) file.Delete();
            }

            res.StatusCode = HttpStatusCode.OK;
            res.Result = "Cabin is deleted";

            return Ok(res);
        }


        [HttpPut]
        public async Task<ActionResult<APIResponse>> UpdateGuest(CabinUpdateDTO cabinUpdateDTO)
        {
            var cabinModel = await _dbCabins.GetOneAsync(g => g.Id == cabinUpdateDTO.Id, tracked: false);

            if (cabinModel is null) throw new AppError("No Cabin Found With This Id", HttpStatusCode.NotFound);

            var cabin = _mapper.Map<Cabin>(cabinUpdateDTO);


            await _dbCabins.UpdateOneAsync(cabin);


            if (cabinUpdateDTO.Image != null)
            {
                if (!string.IsNullOrEmpty(cabinModel.ImageLocalPath))
                {
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), cabinModel.ImageLocalPath);

                    FileInfo file = new FileInfo(oldFilePath);

                    if (file.Exists) file.Delete();
                }

                string fileName = "cabin" + cabinModel.Id + Path.GetExtension(cabinUpdateDTO.Image.FileName);
                string filePath = @"wwwroot\cabinsImages\" + fileName;

                var directoryLocation = Path.Combine(Directory.GetCurrentDirectory(), filePath);

                using (var fileStream = new FileStream(directoryLocation, FileMode.Create))
                {
                    cabinUpdateDTO.Image.CopyTo(fileStream);
                }

                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                cabinModel.ImageUrl = baseUrl + "/cabinsImages/" + fileName;
                cabinModel.ImageLocalPath = filePath;
            }

            res.StatusCode = HttpStatusCode.OK;
            res.Result = _mapper.Map<CabinDTO>(cabin);

            return CreatedAtRoute("GetCabin", new { id = cabin.Id }, res);

        }
    }
}
