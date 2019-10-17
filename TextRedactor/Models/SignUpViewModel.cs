using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TextRedactor.Models
{
    public class SignUpViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Field is required")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "Name length should be > 1 and < 11")]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Field is required")]
        [MinLength(4, ErrorMessage = "Password min length is 4")]
        public string Password { get; set; }
        public string RepeatePassword { get; set; }
    }
}
