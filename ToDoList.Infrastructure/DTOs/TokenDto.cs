namespace ToDoList.Infrastructure.DTOs
{
    public class TokenDto
    {
        public string Token { get; set; } = default!;

        public string RefreshToken { get; set; } = default!;
    }
}
