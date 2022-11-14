using System;
using System.Collections.Generic;
using System.Text;

namespace Techrunch.TecVas.Entities.BillPayments.ProxyResponses
{
    public class ProxyStartimesResponses
    {

        public Bouquets bouquets { get; set; }

        public class Bouquets
        {
            public Item[] items { get; set; }
        }

        public class Item
        {
            public string name { get; set; }
            public string code { get; set; }
            public int price { get; set; }
        }



        public class ProxyStartimesValidateCustomerDetails
        {
            public string returnCode { get; set; }
            public int customerType { get; set; }
            public int billAmount { get; set; }
            public float balance { get; set; }
            public string returnMessage { get; set; }
            public string smartCardNumber { get; set; }
            public string customerNumber { get; set; }
            public string customerName { get; set; }
        }

    }
}
