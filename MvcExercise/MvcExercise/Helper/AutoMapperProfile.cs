using AutoMapper;
using MvcExercise.Dtos;
using MvcExercise.Models;

namespace MvcExercise.Helper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDtos, User>().ReverseMap();
        }
    }
}
