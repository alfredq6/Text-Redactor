using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TextRedactor.Data.Interfaces;

namespace TextRedactor.Data.Models
{
    public class User : IModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
