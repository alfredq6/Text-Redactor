using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextRedactor.Buiseness
{
    public class DetectionInfo
    {
        public string text { get; set; }
        public List<LanguagesInfo> languages { get; set; }
    }
}
