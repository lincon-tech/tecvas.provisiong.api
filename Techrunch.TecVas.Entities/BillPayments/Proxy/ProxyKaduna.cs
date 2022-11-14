using System;
using System.Collections.Generic;
using System.Text;

namespace Techrunch.TecVas.Services.BillPayments.Proxy
{
    public class ProxyKadunaPrepaid
    {

        public KadunaPrepaidProxyDetails details { get; set; }
        public string serviceId { get; set; }


        public class KadunaPrepaidProxyDetails
        {
            public string meterNumber { get; set; }
            
        }


    }
    public class ProxyKadunaPostpaid
    {

        public KadunaPostpaidProxyDetails details { get; set; }
        public string serviceId { get; set; }


        public class KadunaPostpaidProxyDetails
        {
            public string meterNumber { get; set; }

        }


    }
}
