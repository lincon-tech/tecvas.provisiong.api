using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxyValidateSmileBundleCustomer;

namespace Techrunch.TecVas.Services.BillPayments.Proxy
{
    public class ProxyValidateSmileBundleCustomer
    {

        public SmileBundleProxyDetails details { get; set; }
        public string serviceId { get; set; }

        public class SmileBundleProxyDetails
        {
            public string customerAccountId { get; set; }
            public string requestType { get; set; }
        }

        

    }
    public class ProxySmileGetBundles
    {

        public SmileGetBundlesProxyDetails details { get; set; }
        public string serviceId { get; set; }

        public class SmileGetBundlesProxyDetails
        {
            public string customerAccountId { get; set; }
            public string requestType { get; set; }
        }



    }

}
