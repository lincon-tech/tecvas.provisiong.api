using System;
using System.Collections.Generic;
using System.Text;

namespace Techrunch.TecVas.Entities.EtopUp.Mtn
{
    public class MtnSubscriptionResponse
    {
        public string subscriptionDescription { get; set; }
        public string subscriptionStatus { get; set; }
        public int amountCharged { get; set; }
        public bool sendSMSNotification { get; set; }
        public string beneficiaryId { get; set; }
        public bool autoRenew { get; set; }
        public float amountBefore { get; set; }
        public float amountAfter { get; set; }
        public string correlationId { get; set; }
        public string[] links { get; set; }
        public string statusCode { get; set; }
        public string statusMessage { get; set; }
        public string transactionId { get; set; }
        public bool isCVMoffer { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string timestamp { get; set; }
        public string error { get; set; }
        public string path { get; set; }
    }


  

}
