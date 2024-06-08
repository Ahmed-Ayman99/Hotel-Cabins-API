using AutoMapper;
using Hotel_Cabins.DTOs.GuestDOTs;
using Hotel_Cabins.Models;
using Hotel_Cabins.Repository.IRepository;
using Hotel_Cabins.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Hotel_Cabins.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestController : ControllerBase
    {
        internal APIResponse res;
        private readonly IGuestRepository _dbGuest;
        private readonly IMapper _mapper;

        public GuestController(IGuestRepository dbGuest , IMapper mapper)
        {
            res = new();
            _dbGuest = dbGuest;
            _mapper = mapper;
        }



        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAllGuests(int page = 1, int pageSize = 10)
        {
            var guestsModels = await _dbGuest.GetAllAsync(page:page, pageSize: pageSize);

            var pagination = new Pagination()
            {
                Page = page,
                PageSize = pageSize,
            };

            Response.Headers.Add("x-Pagination", JsonSerializer.Serialize(pagination));


            res.Result =  _mapper.Map<List<GuestDTO>>(guestsModels);
            res.StatusCode= HttpStatusCode.OK;
            
            return Ok(res);
        }

        [HttpGet("{id:int}",Name ="GetGuest")]
        public async Task<ActionResult<APIResponse>> GetGuest(int id)
        {
            var guestsModel = await _dbGuest.GetOneAsync(g=>g.Id == id);

            if (guestsModel is null) throw new AppError("No Guest Found With This Id", HttpStatusCode.NotFound);


            res.Result = _mapper.Map<GuestDTO>(guestsModel);
            res.StatusCode = HttpStatusCode.OK;

            return Ok(res);
        }


        [HttpPost]
        public async Task<ActionResult<APIResponse>> CreateGuest(GuestCreateDTO guestCreateDTO)
        {
            var guestModel = _mapper.Map<Guest>(guestCreateDTO);

            await _dbGuest.CreateOneAsync(guestModel);

            var guest = _mapper.Map<GuestDTO>(guestModel);

            res.Result = guest;
            res.StatusCode = HttpStatusCode.Created;

            return CreatedAtRoute("GetGuest", new { id = guestModel.Id }, res);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<APIResponse>> DeleteOneGuest(int id)
        {
            var guestsModel = await _dbGuest.GetOneAsync(g => g.Id == id);

            if (guestsModel is null) throw new AppError("No Guest Found With This Id", HttpStatusCode.NotFound);

            await _dbGuest.DeleteOneAsync(guestsModel);

            res.StatusCode = HttpStatusCode.OK;
            res.Result= "Guest is deleted";

            return Ok(res);
        }

        [HttpPut]
        public async Task<ActionResult<APIResponse>> UpdateGuest(GuestUpdateDTO guestUpdateDTO)
        {
            var guestsModel = await _dbGuest.GetOneAsync(g => g.Id == guestUpdateDTO.Id, tracked: false);

            if (guestsModel is null) throw new AppError("No Guest Found With This Id", HttpStatusCode.NotFound);

            var guest = _mapper.Map<Guest>(guestUpdateDTO);


            await _dbGuest.UpdateOneAsync(guest);

            res.StatusCode = HttpStatusCode.OK;
            res.Result = _mapper.Map<GuestDTO>(guest);

            return CreatedAtRoute("GetGuest", new { id = guestsModel.Id }, res);

        }
    }
}
