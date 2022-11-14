using System;
using System.Collections.Generic;
using System.Text;

namespace Techrunch.TecVas.Entities.BillPayments.JosElectricity
{
    public class JosElectricPostPaidRequest
    {

        public JosElectricityPostPaidDetails details { get; set; }
        public string id { get; set; }
        public string paymentCollectorId { get; set; }
        public string paymentMethod { get; set; }
        public string serviceId { get; set; }

        public class JosElectricityPostPaidDetails
        {
            public string accessCode { get; set; }
            public string customerPhoneNumber { get; set; }
            public int amount { get; set; }
        }

    }
}
