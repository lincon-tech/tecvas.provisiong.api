using System;
using System.Collections.Generic;
using System.Text;

namespace Techrunch.TecVas.Entities.BillPayments.Spectranet
{
    public class SpectranetPaymentPlanRequest
    {

        public SpectranetPaymentPlanDetails details { get; set; }
        public string id { get; set; }
        public string paymentCollectorId { get; set; }
        public string paymentMethod { get; set; }
        public string serviceId { get; set; }

        public class SpectranetPaymentPlanDetails
        {
            public string accountNumber { get; set; }
            public string customerName { get; set; }
            public string operation { get; set; }
            public string packageId { get; set; }
            public int amount { get; set; }
        }

    }
}
