 

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DemoApp.API.Data;
using DemoApp.API.Dtos;

namespace DemoApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IZawajRepository _repo;
        private readonly IMapper _mapper;
        public UsersController(IZawajRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();
            var usertoreturn=_mapper.Map<IEnumerable< userforlistdto>>(users);
            return Ok(usertoreturn);

        }


        [HttpGet("{id}",Name="GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);
            var usertoreturn=_mapper.Map<userfordetailsdto>(user);
            return Ok(usertoreturn);

        }
        [HttpPut("{id}")]
         public async Task<IActionResult> UpdateUser(int id,UserForUpdateDto userForUpdateDto){
             if(id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
             return Unauthorized();
             var userFromRepo = await _repo.GetUser(id);
             _mapper.Map(userForUpdateDto,userFromRepo);
             if(await _repo.SaveAll())
                 return NoContent();
             

             throw new Exception($"حدثت مشكلة في تعديل بيانات المشترك رقم {id}");
             
             
         }
    }
}