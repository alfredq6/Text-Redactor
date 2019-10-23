using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TextRedactor.Data.Interfaces;

namespace TextRedactor.Data.Models
{
    public class Query : IModel
    {
        public long Id { get; set; }
        public DateTime Time { get; set; }
        public long UserId { get; set; }
    }
}
