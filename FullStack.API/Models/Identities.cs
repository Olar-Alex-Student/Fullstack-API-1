using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FullStack.API.Models
{
    public class Identities
    {
        [Key]
        public int Id { get; set; }
        public string Connection { get; set; }
        public string Provider { get; set; }
        public string UserId { get; set; }
        public bool IsSocial { get; set; }

        public User User { get; set; }
    }
}
