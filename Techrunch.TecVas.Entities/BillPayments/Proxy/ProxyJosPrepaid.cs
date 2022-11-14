﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Techrunch.TecVas.Services.BillPayments.Proxy
{
    public class ProxyJosPrepaid
    {

        public JosPrepaidProxyDetails details { get; set; }
        public string serviceId { get; set; }


        public class JosPrepaidProxyDetails
        {
            public string customerNumber { get; set; }
            
        }


    }
}
