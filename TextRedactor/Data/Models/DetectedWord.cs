using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TextRedactor.Data.Interfaces;

namespace TextRedactor.Data.Models
{
    public class DetectedWord : IModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long QueryId { get; set; }
        public string Word { get; set; }
        public string Language { get; set; }
        public double Confidence { get; set; }
    }
}
