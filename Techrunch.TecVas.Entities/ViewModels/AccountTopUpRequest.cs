using System;
using System.Collections.Generic;
using System.Text;

namespace Techrunch.TecVas.Entities.ViewModels
{
    public  class AccountTopUpRequest
    {
        public int TenantId { get; set; }
        public int PartnerId { get; set; }
        public decimal Amount { get; set; }
        
        public int ProductCategoryId { get; set; }
        public string  CreatedBy { get; set; }
        public string AuthorisedBy { get; set; }
    }
}
