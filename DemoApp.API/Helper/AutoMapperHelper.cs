using System.Linq;
using AutoMapper;
using DemoApp.API.Dtos;
using DemoApp.API.Models;

namespace DemoApp.API.Helper
{
    public class AutoMapperHelper:Profile
    {
        public AutoMapperHelper()
        {
            CreateMap<User,userforlistdto>()
            .ForMember(dest =>dest.photoUrl,opt =>{opt.MapFrom(src =>src.Photos.FirstOrDefault(p =>p.IsMain).Url);})
                        .ForMember(dest =>dest.Age,opt =>{opt.ResolveUsing(src =>src.DateOfBirth.CalculateAge());});
            CreateMap<User,userfordetailsdto>()
            .ForMember(dest =>dest.photoUrl,opt =>{opt.MapFrom(src =>src.Photos.FirstOrDefault(p =>p.IsMain).Url);})
            .ForMember(dest =>dest.Age,opt =>{opt.ResolveUsing(src =>src.DateOfBirth.CalculateAge());});
            CreateMap<Photo,photofordetailsdto>();
            CreateMap<UserForUpdateDto,User>();
        }
    }
}