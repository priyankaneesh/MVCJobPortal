namespace MvcExercise.Exceptions
{
    public class UserAlreadyExistException:Exception
    {
        public UserAlreadyExistException(string message):base(message)
        {

        }
        public UserAlreadyExistException(string message,Exception inner):base(message,inner)
        {

        }
    }
}
