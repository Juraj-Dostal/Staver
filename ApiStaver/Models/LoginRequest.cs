namespace ApiStaver.Models
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public byte[] Hashword { get; set; }
    }
}
