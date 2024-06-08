using AutoMapper;
using Hotel_Cabins.Repository.IRepository;
using Hotel_Cabins.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using VillaAPI.DTOs.Role;

namespace Hotel_Cabins.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles ="Admin")]
    public class RolesController
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _dbRole;
        internal APIResponse res;

        public RolesController(IRoleRepository dbRole, IMapper mapper)
        {
            _dbRole = dbRole;
            _mapper = mapper;
            res = new();
        }

        [HttpPost]
        public async Task<ActionResult<APIResponse>> AddRole(CreateRoleDTO createRole)
        {
            var roleModel = _mapper.Map<IdentityRole>(createRole);

            var result = await _dbRole.CreateOneRole(roleModel);

            if (!result.Succeeded) throw new AppError("Somethins Went Very wrong", HttpStatusCode.BadRequest);

            res.Result = new { role = createRole.Name };
            res.StatusCode = HttpStatusCode.Created;

            return res;
        }


        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAllRoles()
        {

            var roles = await _dbRole.GetAllRoles();


            res.Result = roles;
            res.StatusCode = HttpStatusCode.OK;

            return res;
        }
    }
}
