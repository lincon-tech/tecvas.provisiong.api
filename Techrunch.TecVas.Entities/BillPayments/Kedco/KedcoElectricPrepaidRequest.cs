using System;
using System.Collections.Generic;
using System.Text;

namespace Techrunch.TecVas.Entities.BillPayments.Kedco
{
    public class KedcoElectricPrepaidRequest
    {

        public KedcoElectricityPrepaidDetails details { get; set; }
        public string id { get; set; }
        public string paymentCollectorId { get; set; }
        public string paymentMethod { get; set; }
        public string serviceId { get; set; }

        public class KedcoElectricityPrepaidDetails
        {
            public string customerReference { get; set; }
            public string tariffCode { get; set; }
            public int amount { get; set; }
        }

    }
}
