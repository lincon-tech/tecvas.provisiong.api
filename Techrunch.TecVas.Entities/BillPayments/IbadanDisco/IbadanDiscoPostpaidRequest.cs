﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Techrunch.TecVas.Entities.IbadanDisco
{
    public class IbadanDiscoPostpaidRequest
    {

        public IbadanDiscoPostpaidRequestDetails details { get; set; }
        public string id { get; set; }
        public string paymentCollectorId { get; set; }
        public string paymentMethod { get; set; }
        public string serviceId { get; set; }

        public class IbadanDiscoPostpaidRequestDetails
        {
            public string customerReference { get; set; }
            public string customerType { get; set; }
            public string customerName { get; set; }
            public string thirdPartyCode { get; set; }
            public string serviceBand { get; set; }
            public int amount { get; set; }
        }

    }
}
