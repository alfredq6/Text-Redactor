using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextRedactor.Models
{
    public class AuthorizeViewModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string RepeatePassword { get; set; }
    }
}
