using MvcExercise.Dtos;
using MvcExercise.Models;

namespace MvcExercise.Interfaces
{
    public interface IUserService
    {
       User Register(User user);
        User GetUserLogin(LoginDtos loginDtos);
    }
}
