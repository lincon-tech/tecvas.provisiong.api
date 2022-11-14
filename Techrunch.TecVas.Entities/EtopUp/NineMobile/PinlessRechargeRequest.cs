using System;
using System.Collections.Generic;
using System.Text;

namespace Techrunch.TecVas.Entities.EtopUp
{
    public class PinlessRechargeRequest
    {
        public string Msisdn { get; set; }
        public decimal Amount { get; set; }
        public string transId { get; set; }
        public int rechargeType { get; set; }
        public string ProductCode { get; set; }

    }
}
