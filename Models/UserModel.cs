using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeWarsBackend.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public int UserScore { get; set; }
        public int CompletedKatas { get; set; }
        public string? PendingKata { get; set; }
        public string? Salt { get; set; }
        public string? Hash { get; set; }
        public bool IsAdmin { get; set; }

        public UserModel() { }
    }
}