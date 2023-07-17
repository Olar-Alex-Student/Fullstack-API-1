using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FullStack.API.Models
{
    public class User
    {
        public string CreatedAt { get; set; }
        public string Email { get; set; }
        public bool EmailVerified { get; set; }
        public Identities Identities { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string Picture { get; set; }
        public string UpdatedAt { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string LastIp { get; set; }
        public string LastLogin { get; set; }
        public int LoginsCount { get; set; }
        //public ArrayList BlockedFor { get; set; }
        //public ArrayList GuardianAuthenticators { get; set; }
    }
}
