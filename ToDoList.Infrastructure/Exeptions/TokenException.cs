namespace ToDoList.Infrastructure.Exeptions
{
    public class TokenException : Exception
    {
        public TokenException(string errorMessage) : base(errorMessage) { }
    }
}
