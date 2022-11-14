using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxyValidateSmileBundleCustomer;

namespace Techrunch.TecVas.Services.BillPayments.Proxy
{
    public class ProxyMultichoiceValidateSmartcardNo
    {

        public MultichoiceValidateSmartcardNoProxyDetails details { get; set; }
        public string serviceId { get; set; }

        public class MultichoiceValidateSmartcardNoProxyDetails
        {
            public string number { get; set; }
            public string requestType { get; set; }
        }

    }
    public class ProxyMultichoiceGetBouquet
    {

        public MultichoiceGetBouquetProxyDetails details { get; set; }
        public string serviceId { get; set; }

        public class MultichoiceGetBouquetProxyDetails
        {
            public int invoicePeriod { get; set; }
            public string number { get; set; }
            public string requestType { get; set; }
        }

    }
    public class ProxyMultichoice
    {

        public FindStandaloneProductsProxyDetails details { get; set; }
        public string serviceId { get; set; }

        public class FindStandaloneProductsProxyDetails
        {
            public string requestType { get; set; }
        }

    }
    public class ProxyMultichoiceProductAddon
    {

        public MultichoiceProductAddonProxyDetails details { get; set; }
        public string serviceId { get; set; }

        public class MultichoiceProductAddonProxyDetails
        {
            public string primaryProductCode { get; set; }
            public string requestType { get; set; }
        }

    }



}
