using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TextRedactor.Data.Interfaces;

namespace TextRedactor.Data.Models
{
    public class LoginTimeLog : IModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public DateTime LastTimeLogin { get; set; }
    }
}
