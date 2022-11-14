using System;
using System.Collections.Generic;
using System.Text;

namespace Techrunch.TecVas.Entities.BillPayments
{
    public class ProxyRequest
    {
        public ProxyRequestDetails details { get; set; }
        public string serviceId { get; set; }

        public class ProxyRequestDetails
        {
            public string requestType { get; set; }
            public string accountType { get; set; }
            public string customerReference { get; set; }
            public string customerReferenceType { get; set; }
            public string customerAccountId { get; set; }
            public string customerNumber { get; set; }
            public string accountNumber { get; set; }
            public string smartCardNumber { get; set; }

            public string meterNumber { get; set; }
            public Policy policy { get; set; }
            public string phone { get; set; }
            public string registrationNumber { get; set; }
            public string vehicleManufacter { get; set; }
            public string policyId { get; set; }
            public Int64? number { get; set; }
            public int? invoicePeriod { get; set; }
            public string primaryProductCode { get; set; }

            public string returnCode { get; set; }
            public int customerType { get; set; }
            public int billAmount { get; set; }
            public float balance { get; set; }
            public string returnMessage { get; set; }
            public string customerName { get; set; }
        }
        public class Policy
        {
            public Client client { get; set; }
            public Vehicle vehicle { get; set; }
        }
        public class Client
        {
            public string address { get; set; }
            public string companyName { get; set; }
            public string dateOfBirth { get; set; }
            public string email { get; set; }
            public string firstName { get; set; }
            public string gender { get; set; }
            public string lastName { get; set; }
            public string mobile { get; set; }
            public string referredBy { get; set; }
            public string referrerDetails { get; set; }
        }

        public class Vehicle
        {
            public string chassisNumber { get; set; }
            public string engineNumber { get; set; }
            public string insuranceType { get; set; }
            public string policyholderType { get; set; }
            public string registrationNumber { get; set; }
            public string vehicleClass { get; set; }
            public string vehicleManufacturer { get; set; }
            public string vehicleModel { get; set; }
            public int yearOfManufacture { get; set; }
            public int yearOfPurchase { get; set; }
        }

    }



    

}
