

using System;
using System.Collections.Generic;
using System.Text;

namespace Techrunch.TecVas.Entities.BillPayments.Cornerstone
{
    public class CornerstoneRequest
    {
        public CornerstoneRequestDetails details { get; set; }
        public string id { get; set; }
        public string paymentCollectorId { get; set; }
        public string paymentMethod { get; set; }
        public string serviceId { get; set; }

        public class CornerstoneRequestDetails
        {
            public int amount { get; set; }
            public string policyId { get; set; }
        }
    }
}
