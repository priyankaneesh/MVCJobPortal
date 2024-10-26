using MvcExercise.Dtos;
using MvcExercise.Interfaces;
using MvcExercise.Models;

namespace MvcExercise.Repo
{
    public class UserRepo:IUserRepo
    {
        private readonly MvcExerciseDbContext _context;

        public UserRepo(MvcExerciseDbContext context)
        {
            _context = context;
        }
        public User LoggedUser = new User();
        public User Register(User user)
        {
            if(_context.Users.Where(u=>u.Email==user.Email).Count()>0)
            {
                return null;
            }
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }
        public User GetUserLogin(LoginDtos loginDtos)
        {
            return _context.Users.Where(u=>u.Email==loginDtos.Email && u.password==loginDtos.password).FirstOrDefault();
        }
    }
}
