using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DemoApp.API.Data;
using DemoApp.API.Dtos;
using DemoApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DemoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
           private readonly IMapper _mapper;
        public AuthController(IAuthRepository repo,IConfiguration  config, IMapper mapper)
        {
            this._repo = repo;
            this._config=config;
              this._mapper = mapper;

        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]userforregisterdto userdto)
        {
            userdto.username=userdto.username.ToLower();
            if(await _repo.UserExists(userdto.username))
            return BadRequest("user exist befor");
            var usertocreate= _mapper.Map<User>(userdto);
          var createduser=await  _repo.Register(usertocreate,userdto.password);
          var userToreturn=_mapper.Map<userfordetailsdto>(createduser);
          return CreatedAtRoute("GetUser",new {controller="Users",id=createduser.Id},userToreturn);
           


        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]userforlogindto userdto)
        {
            
              
          var UserResponse=await  _repo.Login(userdto.Username,userdto.Password);
          if(UserResponse==null) return Unauthorized();
          var calims=new []{
              new Claim(ClaimTypes.NameIdentifier,UserResponse.Id.ToString()),
              new Claim(ClaimTypes.Name,UserResponse.Username)
          }; 
          var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value)); 
          var cred=new SigningCredentials(key,SecurityAlgorithms.HmacSha512);
          var tokenDescriptor= new SecurityTokenDescriptor{
              Subject=new ClaimsIdentity(calims),
              Expires=DateTime.Now.AddDays(1),
              SigningCredentials=cred
          };
          var tokenHandler=new  JwtSecurityTokenHandler();
          var token=tokenHandler.CreateToken(tokenDescriptor);
          var user = _mapper.Map<userforlistdto>(UserResponse);
            
            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                user
            });
           



        }
    }
}