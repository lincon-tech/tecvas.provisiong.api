﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Techrunch.TecVas.Services.BillPayments.Proxy
{
    public class ProxyEkoPostpaid
    {

        public EkoPostpaidProxyDetails details { get; set; }
        public string serviceId { get; set; }


        public class EkoPostpaidProxyDetails
        {
            public string customerReference { get; set; }
            
        }


    }
}
