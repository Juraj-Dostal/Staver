using System.Text.Json.Serialization;

namespace ClientStaver.Models
{
    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public byte[] Hashword { get; set; }
    }

    public class LoginResponse
    {
        public string Token { get; set; }
    }

    public static class Token
    {
        public static string? Value { get; set; }
    }

    public class Signup
    {
        public string Username { get; set; }
        public string Password { get; set; }

        [JsonIgnore]
        public RoleEnum Role { get; set; } // e.g., "Admin", "User", etc.
    }

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] Hashword { get; set; } // Password in hash

        [JsonIgnore]
        public ICollection<Role> Roles { get; set; } = new List<Role>(); // e.g., "Admin", "User", etc.

    }

    public class UserRequest
    {
        public string Username { get; set; }
        public byte[] Hashword { get; set; }
        public List<RoleEnum> Roles { get; set; }
    }

    public class Role
    {
        public int Id { get; set; }
        public RoleEnum RoleName { get; set; } // e.g., "Admin", "User", etc.

        public ICollection<User> Users { get; set; } = new List<User>(); // Users with this role
    }

    public enum RoleEnum
    {
        Admin,
        Editor,
        User,
    }

}
