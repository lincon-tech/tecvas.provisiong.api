﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Techrunch.TecVas.Entities.BillPayments.IkejaElectric
{
    public class IkejaElectricPostpaidRequest
    {

        public IkejaElectricPostpaidDetails details { get; set; }
        public string id { get; set; }
        public string paymentCollectorId { get; set; }
        public string paymentMethod { get; set; }
        public string serviceId { get; set; }

        public class IkejaElectricPostpaidDetails
        {
            public string accountNumber { get; set; }
            public decimal amount { get; set; }
            public string phoneNumber { get; set; }
            public string email { get; set; }
            public string customerName { get; set; }
            public string customerAddress { get; set; }
            public string customerAccountType { get; set; }
            public string customerDtNumber { get; set; }
            public string contactType { get; set; }
        }

    }
}
