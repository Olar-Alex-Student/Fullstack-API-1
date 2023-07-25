using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FullStack.API.Models
{
    public class User
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Nickname { get; set; }
        public string Picture { get; set; }
        public Guid? RoleId { get; set; }
        public Role? Role { get; set; }
    }
}
