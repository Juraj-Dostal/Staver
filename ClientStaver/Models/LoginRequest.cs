namespace ClientStaver.Models
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public string Token { get; set; }
    }

    public static class Token
    {
        public static string? Value { get; set; }
    }
}
