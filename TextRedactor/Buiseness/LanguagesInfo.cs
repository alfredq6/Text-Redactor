using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextRedactor.Buiseness
{
    public class LanguagesInfo
    {
        public string language { get; set; }
        public bool isReliable { get; set; }
        public float confidence { get; set; }
    }
}
