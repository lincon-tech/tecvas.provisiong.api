using System;
using System.Collections.Generic;
using System.Text;

namespace Techrunch.TecVas.Services.BillPayments.Proxy
{
    public class ProxyJosPostpaid
    {

        public JosPostpaidProxyDetails details { get; set; }
        public string serviceId { get; set; }


        public class JosPostpaidProxyDetails
        {
            public string customerNumber { get; set; }
            
        }


    }
}
