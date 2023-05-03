using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeWarsBackend.Models
{
    public class KataModel
    {
        public int Id { get; set; }
        public string KataName { get; set; }
        public string KataDescription { get; set; }
        public string Language { get; set; }
        public string UserAssigned { get; set; }
        public bool IsCompleted { get; set; }

        public KataModel() { }
    }
}