using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using DemoApp.API.Data;
using DemoApp.API.Dtos;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using DemoApp.API.Models;
using System.Linq;

namespace DemoApp.API.Controllers
{
      [Authorize]
    [Route("api/users/{userId}/photos")]
    [ApiController]
    public class PhotosControllers : ControllerBase
    {
        private readonly IZawajRepository _repo;
        private readonly IMapper _mapper;
        private readonly object Convert;
        private IHostingEnvironment _env;
         private readonly IConfiguration _config;
        public PhotosControllers(IZawajRepository repo, IMapper mapper, IHostingEnvironment env,IConfiguration  config)
        {
            _mapper = mapper;
            _repo = repo;
            _env = env;
             _config=config;
        }
        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photoFromRepository = await _repo.GetPhoto(id);
            var photo = _mapper.Map<photoForReturnDto>(photoFromRepository);

            return Ok(photo);
        }
        [HttpPost()]
        public async Task<IActionResult> AddPhotoForUser(int userId,[FromForm] photoForCreateDto photoforrcreate)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            var userFromRepo = await _repo.GetUser(userId);
            var file = photoforrcreate.File;
            if (file != null && file.Length > 0)
            {
                photoforrcreate.Url = AddFolderAndImage(file);
                var photo = _mapper.Map<Photo>(photoforrcreate);
                if (!userFromRepo.Photos.Any(x => x.IsMain == true))
                    photo.IsMain = true;
                userFromRepo.Photos.Add(photo);
                if (await _repo.SaveAll())
                {
                    var phototoreturn = _mapper.Map<photoForReturnDto>(photo);
                    return CreatedAtRoute("GetPhoto", new { id = photo.Id }, phototoreturn.Id);
                }
                else
                {

                    return BadRequest("خطا اثناء تحميل الصورة");
                }


            }
            else
            {
                    return BadRequest("خطا اثناء تحميل الصورة");
                

            }



        }

        public string AddFolderAndImage(IFormFile file)
        {
           // var webRoot = _env.WebRootPath;
            //var PathWithFolderName = System.IO.Path.Combine(webRoot, "Files");

           // string fname = (DateTime.Now.Year + "" + DateTime.Now.Month + "" + DateTime.Now.Day + ".txt").ToString();
           var pa = Directory.GetCurrentDirectory();
                
                string targetPath2 = pa + "/wwwroot/Files/Users" ;
               
                 
            string filePath = "";
            if (!Directory.Exists(targetPath2))
            {
                DirectoryInfo di = Directory.CreateDirectory(targetPath2);

            }
            // Try to create the directory.

             string dbpath=_config.GetSection("AppSettings:CurrentDomain").Value+"Files/Users/";
            if (file.Length > 0)
            {
                dbpath=dbpath+file.FileName;
                filePath = Path.Combine(targetPath2, file.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyToAsync(fileStream);
                }
            }


            return dbpath;

        }
   
   [HttpPost("{id}/setMain")]
        public async Task<IActionResult> SetMainPhoto(int userId,int id)
        {
             if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            var userFromRepo = await _repo.GetUser(userId);
            if(!userFromRepo.Photos.Any(p=>p.Id==id))
            return Unauthorized();
            var DesiredMainPhoto = await _repo.GetPhoto(id);
            if(DesiredMainPhoto.IsMain)
            return BadRequest("هذه هي الصورة الأساسية بالفعل");
            var CurrentMainPhoto = await _repo.GetMainPhotoForUser(userId);
            CurrentMainPhoto.IsMain=false;
            DesiredMainPhoto.IsMain=true;
            if(await _repo.SaveAll())
            return NoContent();
            return BadRequest("لايمكن تعديل الصورة الأساسية");
            
        }
        
         [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePhoto(int userId,int id){
        if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            var userFromRepo = await _repo.GetUser(userId);
            if(!userFromRepo.Photos.Any(p=>p.Id==id))
            return Unauthorized();
            var Photo = await _repo.GetPhoto(id);
            if(Photo.IsMain)
            return BadRequest("لايمكن حذف الصورة الأساسية");
              _repo.Delete(Photo);
             

            if(await _repo.SaveAll())
            return Ok();
            return BadRequest("فشل حذف الصورة");
            
    }
   
    }
}