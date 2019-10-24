using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextRedactor.Buiseness.JSONModels
{
    public class TopRequestsInfo
    {
        public int number { get; set; }
        public string userName { get; set; }
        public long queriesCount { get; set; }
        public string lastLoginTime { get; set; }
        public long averageTimeBetweenQueriesInMinutes { get; set; }
    }
}
