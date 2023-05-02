using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeWarsBackend.Models.DTO
{
    public class AdminLoginDTO
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? password { get; set; }
        public bool IsAdmin { get; set; }
    }
}