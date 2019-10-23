using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TextRedactor.Data.Interfaces;

namespace TextRedactor.Data.Models
{
    public class TopRequests : IModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long QueriesCount { get; set; }
        public DateTime LastTimeLoginId { get; set; }
        public DateTime AverageTimeBetweenQueries { get; set; }
    }
}
