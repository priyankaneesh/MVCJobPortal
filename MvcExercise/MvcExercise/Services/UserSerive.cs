using MvcExercise.Dtos;
using MvcExercise.Interfaces;
using MvcExercise.Models;

namespace MvcExercise.Services
{
    public class UserSerive:IUserService
    {
        private  IUserRepo _userRepository;

        public UserSerive(IUserRepo userRepository)
        {
            _userRepository = userRepository;
        }
        public User Register(User user)
        {

            return _userRepository.Register(user);
        }
        public User GetUserLogin(LoginDtos loginDtos)
        {

            return _userRepository.GetUserLogin(loginDtos);
        }
    }
}
