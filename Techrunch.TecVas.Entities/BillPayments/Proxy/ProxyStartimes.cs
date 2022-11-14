using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxyValidateSmileBundleCustomer;

namespace Techrunch.TecVas.Services.BillPayments.Proxy
{
    public class ProxyStartimes
    {

        public StartimesProxyDetails details { get; set; }
        public string serviceId { get; set; }

        public class StartimesProxyDetails
        {
            public string smartCardNumber { get; set; }
            public string requestType { get; set; }
        }

    }
    public class ProxyStartimesGetBundle
    {

        public StartimesGetBundleProxyDetails details { get; set; }
        public string serviceId { get; set; }

        public class StartimesGetBundleProxyDetails
        {
            
            public string requestType { get; set; }
        }

    }


}
