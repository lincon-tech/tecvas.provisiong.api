using System;
using System.Collections.Generic;
using System.Text;

namespace Techrunch.TecVas.Services.BillPayments.Proxy
{
    public class ProxyKedcoPrepaid
    {

        public KedcoPrepaidProxyDetails details { get; set; }
        public string serviceId { get; set; }


        public class KedcoPrepaidProxyDetails
        {
            public string customerReference { get; set; }
            
        }


    }
    public class ProxyKedcoPostpaid
    {

        public KedcoPostpaidProxyDetails details { get; set; }
        public string serviceId { get; set; }


        public class KedcoPostpaidProxyDetails
        {
            public string customerReference { get; set; }

        }


    }
}
