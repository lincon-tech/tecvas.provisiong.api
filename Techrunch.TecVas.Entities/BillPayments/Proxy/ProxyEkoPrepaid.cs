using System;
using System.Collections.Generic;
using System.Text;

namespace Techrunch.TecVas.Services.BillPayments.Proxy
{
    public class ProxyEkoPrepaid
    {

        public EkoPrepaidProxyDetails details { get; set; }
        public string serviceId { get; set; }


        public class EkoPrepaidProxyDetails
        {
            public string meterNumber { get; set; }
            
        }


    }
}
