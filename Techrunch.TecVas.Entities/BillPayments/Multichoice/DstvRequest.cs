﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Techrunch.TecVas.Entities.BillPayments.Multichoice
{
    public class DstvRequest
    {

        public DstvRequestDetails details { get; set; }
        public string id { get; set; }
        public string paymentCollectorId { get; set; }
        public string paymentMethod { get; set; }
        public string serviceId { get; set; }

        public class DstvRequestDetails
        {
            public string[] productsCode { get; set; }
            public int customerNumber { get; set; }
            public string smartcardNumber { get; set; }
            public string customerName { get; set; }
            public int invoicePeriod { get; set; }
            public int monthsPaidFor { get; set; }
            public string subscriptionType { get; set; }
            public int amount { get; set; }
        }

    }
}
