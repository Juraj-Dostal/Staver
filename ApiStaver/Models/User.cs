using Humanizer.Bytes;
using System.Text.Json.Serialization;

namespace ApiStaver.Models
{
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

        [JsonIgnore]
        public ICollection<User> Users { get; set; } = new List<User>(); // Users with this role
    }

    public enum RoleEnum
    {
        Admin,
        Editor,
        User,
    }
}
