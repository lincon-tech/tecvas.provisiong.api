using System;
using System.Collections.Generic;
using System.Text;

namespace Techrunch.TecVas.Entities.BillPayments.ProxyResponses
{
    public class ProxySmileResponses
    {

        public class ProxySmileValidateCustomer
        {
            public ProxySmileValidateCustomerDetails details { get; set; }
        }

        public class ProxySmileValidateCustomerDetails
        {
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string middleName { get; set; }
        }

       

        public class ProxySmileGetbundlesDetails
        {
            public Bundle[] bundles { get; set; }
        }

        public class Bundle
        {
            public bool isAvailable { get; set; }
            public string name { get; set; }
            public int price { get; set; }
            public string datacode { get; set; }
            public string validity { get; set; }
            public string allowance { get; set; }
        }

    }
}
