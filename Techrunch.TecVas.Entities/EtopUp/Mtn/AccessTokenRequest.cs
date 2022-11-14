using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Techrunch.TecVas.Entities.EtopUp.Mtn
{
    public class AccessTokenRequest
    {
        public string grant_type { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        [Required]
        public string scope { get; set; }
    }
}
