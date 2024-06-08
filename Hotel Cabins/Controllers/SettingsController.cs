using AutoMapper;
using Hotel_Cabins.DTOs.SettingsDTOs;
using Hotel_Cabins.Models;
using Hotel_Cabins.Repository.IRepository;
using Hotel_Cabins.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Hotel_Cabins.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsRepository _dbSettings;
        private readonly IMapper _mapper;
        internal APIResponse res;

        public SettingsController(ISettingsRepository dbSettings , IMapper mapper)
        {
            _dbSettings = dbSettings;
            _mapper = mapper;
            res = new();
        }

        [HttpGet(Name = "GetSettings")]
        public async Task<ActionResult<APIResponse>> GetAllSettings() {
            var settings =await _dbSettings.GetAllAsync();

            res.Result =_mapper.Map<SettingsDTO>(settings.FirstOrDefault()) ;
            res.StatusCode= HttpStatusCode.OK;

            return Ok(res);
        }



        [HttpPost]
        public async Task<ActionResult<APIResponse>> CreateSettings(SettingsCreateDTO settingsCreateDTO)
        {
            var settings = _mapper.Map<Setting>(settingsCreateDTO);

            await _dbSettings.CreateOneAsync(settings);

            res.Result = _mapper.Map<SettingsDTO>(settings);    
            res.StatusCode = HttpStatusCode.Created;

            return CreatedAtRoute("GetSettings" , res);
        }



        [HttpPut]
        public async Task<ActionResult<APIResponse>> UpdateSettings(SettingsUpdateDTO settingsUpdateDTO)
        {
            var settingsModel = await _dbSettings.GetOneAsync(g => g.Id == settingsUpdateDTO.Id, tracked: false);

            if (settingsModel is null) throw new AppError("No Guest Found With This Id", HttpStatusCode.NotFound);


            var settings = _mapper.Map<Setting>(settingsUpdateDTO);

            await _dbSettings.UpdateOneAsync(settings);

            res.Result = _mapper.Map<SettingsDTO>(settings);
            res.StatusCode = HttpStatusCode.Created;

            return CreatedAtRoute("GetSettings", res);
        }
    }
}
