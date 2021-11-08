using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace bMobile.Shared
{
    public class SecurityKey
    {
        [Required(ErrorMessage = "SKEY:USER NAME EMPTY!", AllowEmptyStrings = false)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "SKEY:USER KEY EMPTY!", AllowEmptyStrings = false)]
        public string UserKey { get; set; }
    }
}
