﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Marvin.StreamExtensions;
using System.IO;
using System.Security.Cryptography;
using System.Linq;
using Techrunch.TecVas.Entities.BillPayments;
using Techrunch.TecVas.Entities.BillPayments.AbujaDisco;
using static Techrunch.TecVas.Entities.BillPayments.Multichoice.DstvRequest;
using static Techrunch.TecVas.Entities.BillPayments.ServiceListResponse;
using Techrunch.TecVas.Entities.BillPayments.Multichoice;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Techrunch.TecVas.Entities.BillPayments.IkejaElectric;
using Techrunch.TecVas.Entities.BillPayments.BulkSMS;
using Techrunch.TecVas.Entities.BillPayments.Carpaddy;
using Techrunch.TecVas.Entities.BillPayments.Cornerstone;
using Techrunch.TecVas.Entities.BillPayments.EkoElectric;
using Techrunch.TecVas.Entities.IbadanDisco;
using Techrunch.TecVas.Services.BillPayments.Jamb;
using Techrunch.TecVas.Entities.BillPayments.JosElectricity;
using Techrunch.TecVas.Entities.BillPayments.Kaduna;
using Techrunch.TecVas.Entities.BillPayments.Kedco;
using Techrunch.TecVas.Entities.BillPayments.Mutual;
using Techrunch.TecVas.Entities.BillPayments.PortharcourtElectric;
using Techrunch.TecVas.Entities.BillPayments.Showmax;
using Techrunch.TecVas.Entities.BillPayments.Smile;
using Techrunch.TecVas.Entities.BillPayments.Spectranet;
using Techrunch.TecVas.Entities.BillPayments.Startimes;
using Techrunch.TecVas.Entities.BillPayments.Waec;
using Techrunch.TecVas.Services.BillPayments.Proxy;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxyCornerStoneCarModels;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxyEkoPrepaid;

namespace Techrunch.TecVas.Services.BillPayments.AbujaDisco
{
    public class BillerPaymentsService : IBillerPaymentsService
    {
        private readonly IHttpClientFactory _httpFactory;
        private readonly ILogger<IBillerPaymentsService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public BillerPaymentsService(
            ILogger<IBillerPaymentsService> logger,
            IConfiguration configuration,
            IMapper mapper,
            IHttpClientFactory httpFactory)
        {
            _httpFactory = httpFactory;
            _logger = logger;
            _configuration = configuration;
            _mapper = mapper;


        }



        #region General Bill payments

        public async Task<BillPaymentsResponse> BillerPayAsync(BillpaymentRequest paymentRequest, CancellationToken cancellationToken)
        {

            _logger.LogInformation($"Inside BillerPayAsync service request with service id: {paymentRequest.serviceId}");
            string requestBodyString = string.Empty;
            object requestBodyObj  = null;

            switch (paymentRequest.serviceId)
            {
                //var dto =_mapper.Map<DestinationDto>(SourceDto);
                case "CIB":
                    requestBodyObj = _mapper.Map<BillpaymentRequest, AbujaPostpaidRequest>(paymentRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "CIA":
                    requestBodyObj = _mapper.Map<BillpaymentRequest, AbujaPrepaidRequest>(paymentRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "AOA":
                    requestBodyObj = _mapper.Map<BillpaymentRequest, BulkSMSRequest>(paymentRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "CLA":
                    requestBodyObj = _mapper.Map<BillpaymentRequest, CarpaddyRequest>(paymentRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "BCA":
                    if(paymentRequest.details.subriskCode!=null)
                    {
                        requestBodyObj = _mapper.Map<BillpaymentRequest, MutualMortorInsuranceRequest>(paymentRequest);
                        requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    }
                    else {
                        requestBodyObj = _mapper.Map<BillpaymentRequest, CornerstoneRequest>(paymentRequest);
                        requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    }
                    
                    break;
                case "AVA":
                    requestBodyObj = _mapper.Map<BillpaymentRequest, EkoElectricPostpaidRequest>(paymentRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "BAA":
                    requestBodyObj = _mapper.Map<BillpaymentRequest, EkoElectricPrepaidRequest>(paymentRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "AUA":
                    requestBodyObj = _mapper.Map<BillpaymentRequest, IbadanDiscoPostpaidRequest>(paymentRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;

                case "AUB":
                    requestBodyObj = _mapper.Map<BillpaymentRequest, IbadanDiscoPrepaidRequest>(paymentRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "APA":
                    requestBodyObj = _mapper.Map<BillpaymentRequest, IkejaElectricPostpaidRequest>(paymentRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "APB":
                    requestBodyObj = _mapper.Map<BillpaymentRequest, IkejaElectricTokenPurchaseRequest>(paymentRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "ACA":
                    requestBodyObj = _mapper.Map<BillpaymentRequest, JambPINRequest>(paymentRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "CKB":
                    requestBodyObj = _mapper.Map<BillpaymentRequest, JosElectricPostPaidRequest>(paymentRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "CKA":
                    requestBodyObj = _mapper.Map<BillpaymentRequest, JosElectricPostPaidRequest>(paymentRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "CDA":
                    requestBodyObj = _mapper.Map<BillpaymentRequest, KadunaElectricPrepaidRequest>(paymentRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "CDB":
                    requestBodyObj = _mapper.Map<BillpaymentRequest, KadunaElectricPostpaidRequest>(paymentRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "AVB":
                    requestBodyObj = _mapper.Map<BillpaymentRequest, KedcoElectricPrepaidRequest>(paymentRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "AVC":
                    requestBodyObj = _mapper.Map<BillpaymentRequest, KedcoElectricPostpaidRequest>(paymentRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "AQA":
                    if(paymentRequest.details.productsCodes.Length > 0)
                    {
                        requestBodyObj = _mapper.Map<BillpaymentRequest, DstvRequest>(paymentRequest);
                        requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    }
                    else
                    {
                        requestBodyObj = _mapper.Map<BillpaymentRequest, DstvBoxOfficeRequest>(paymentRequest);
                        requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    }
                    
                    break;
                case "AQC":
                    if(paymentRequest.details.productsCodes.Length> 0)
                    {
                        requestBodyObj = _mapper.Map<BillpaymentRequest, GotvRenew>(paymentRequest);
                        requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    }
                    else
                    {
                        requestBodyObj = _mapper.Map<BillpaymentRequest, GotvRequest>(paymentRequest);
                        requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    }
                    break;
                
                case "BIA":
                    requestBodyObj = _mapper.Map<BillpaymentRequest, PortHarcourtElectricPrepaidRequest>(paymentRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "BIB":
                    requestBodyObj = _mapper.Map<BillpaymentRequest, PortHarcourtElectricPostpaidRequest>(paymentRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "CPA":
                    requestBodyObj = _mapper.Map<BillpaymentRequest, ShowmaxVoucherRequest>(paymentRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "ANA":
                    requestBodyObj = _mapper.Map<BillpaymentRequest, SmileCommRechargeRequest>(paymentRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "ANB":
                    requestBodyObj = _mapper.Map<BillpaymentRequest, SmileCommBundleRequest>(paymentRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "BGB":
                    requestBodyObj = _mapper.Map<BillpaymentRequest, SpectranetPaymentPlanRequest>(paymentRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "BGA":
                    requestBodyObj = _mapper.Map<BillpaymentRequest, SpectranetPINRequest>(paymentRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "BGC":
                    requestBodyObj = _mapper.Map<BillpaymentRequest, SpectranetRefillRequest>(paymentRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "AWA":
                    requestBodyObj = _mapper.Map<BillpaymentRequest, StartimesRequest>(paymentRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "ASA":
                    requestBodyObj = _mapper.Map<BillpaymentRequest, WaecPINRequest>(paymentRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                default:
                    break;
            }


            BillPaymentsResponse result = new BillPaymentsResponse();

            string gatewayURL = _configuration["BaxiBillsAPI:URL"];

            var httpClient = _httpFactory.CreateClient("BaxiBillsAPI");

            string baxi_Username = _configuration["BaxiBillsAPI:BAXI_USERNAME"];
            string BAXI_SEC_TOKEN = _configuration["BaxiBillsAPI:BAXI_SEC_TOKEN"];
            string serveletPath = _configuration["BaxiBillsAPI:ServletPath"];

            _logger.LogInformation($"baxiusername :  {baxi_Username}");
            _logger.LogInformation($"BAXI_SEC_TOKEN :  {BAXI_SEC_TOKEN}");
            _logger.LogInformation($"serveletPath :  {serveletPath}");


            
            //string requestBody = JsonConvert.SerializeObject(paymentRequest);

            _logger.LogInformation($"requestBody :  {requestBodyString}");

            string sha256 = sha256hash(requestBodyString);

            //_logger.LogInformation($"sha256 :  {sha256}");

            string hashedPayload = HexString2B64String(sha256);

            //_logger.LogInformation($"hashedPayload :  {hashedPayload}");

            var x_mspdate = DateTime.Now.ToString("R");  //"Wed, 12 Oct 2022 16:26:36 GMT"; //  // Thu, 06 Oct 2022 19:49:43 GMT  //DateTime.UtcNow; 

            _logger.LogInformation($"x_mspdate :  {x_mspdate}");

            DateTime x = DateTime.Parse(x_mspdate);

            var unixTimestamp = ((DateTimeOffset)x).ToUnixTimeSeconds();

            //_logger.LogInformation($"unixTimestamp :  {unixTimestamp}");
            


            StringBuilder strToSign = new StringBuilder();
            strToSign.Append("POST");

            strToSign.Append(serveletPath);

            strToSign.Append(unixTimestamp);
            strToSign.Append(hashedPayload);


            string signature = HexString2B64String(ComputeHMAC_SHA1(strToSign.ToString(), BAXI_SEC_TOKEN));

            _logger.LogInformation($"signature :  {signature}");

            string authHeader = "Baxi" + " " + baxi_Username + ":" + signature;

            _logger.LogInformation($"authHeader :  {authHeader}");

            httpClient.DefaultRequestHeaders.Add("Authorization", authHeader); //[{"key":"x-msp-date","value":"{{x-msp-date}}","type":"text"}]
            httpClient.DefaultRequestHeaders.Add("x-msp-date", x_mspdate); // x_mspdate.ToString("R"));


            var httpRequest = new HttpRequestMessage(HttpMethod.Post, gatewayURL);
            _logger.LogInformation($"Calling BillerPayAsync  {httpRequest.RequestUri}");

            using (var httpContent = CreateHttpContent(requestBodyObj))
            {
                httpRequest.Content = httpContent;

                using (var response = await httpClient
                    .SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead)
                    .ConfigureAwait(false))
                {
                    //response.EnsureSuccessStatusCode();
                    if (!response.IsSuccessStatusCode)
                    {
                        _logger.LogInformation($"api call BillerPayAsync not SuccessStatus . it returned with statusCode {response.StatusCode} reason: {response.ReasonPhrase}");
                        var errorStream = await response.Content.ReadAsStringAsync();

                        //var validationErrors = errorStream.ReadAndDeserializeFromJson();
                        _logger.LogWarning($"api call BillerPayAsync returned with status code: {response.StatusCode} validationErrors: -- {errorStream} --");
                        //var dstverror = JsonConvert.DeserializeObject<DstvError>(errorStream.ToString());

                        result = JsonConvert.DeserializeObject<BillPaymentsResponse>(errorStream);

                    }
                    if (response.IsSuccessStatusCode)
                    {
                        string contentStream = await response.Content.ReadAsStringAsync();
                        _logger.LogInformation($"api call BillerPayAsync returned with contentstream  {contentStream}");
                        result = JsonConvert.DeserializeObject<BillPaymentsResponse>(contentStream);
                    }

                }
            }

            return result;
        }

        public async Task<BillPaymentsResponse> BillerPayAsyncWOrkingVersion(BillpaymentRequest paymentRequest, CancellationToken cancellationToken)
        {

            _logger.LogInformation($"Inside BillerPayAsync service request");

            BillPaymentsResponse result = new BillPaymentsResponse();

            string gatewayURL = _configuration["BaxiBillsAPI:URL"];

            var httpClient = _httpFactory.CreateClient("BaxiBillsAPI");

            string baxi_Username = _configuration["BaxiBillsAPI:BAXI_USERNAME"];
            string BAXI_SEC_TOKEN = _configuration["BaxiBillsAPI:BAXI_SEC_TOKEN"];
            string serveletPath = _configuration["BaxiBillsAPI:ServletPath"];

            _logger.LogInformation($"baxiusername :  {baxi_Username}");
            _logger.LogInformation($"BAXI_SEC_TOKEN :  {BAXI_SEC_TOKEN}");
            _logger.LogInformation($"serveletPath :  {serveletPath}");



            string requestBody = JsonConvert.SerializeObject(paymentRequest);

            _logger.LogInformation($"requestBody :  {requestBody}");

            string sha256 = sha256hash(requestBody);

            //_logger.LogInformation($"sha256 :  {sha256}");

            string hashedPayload = HexString2B64String(sha256);

            //_logger.LogInformation($"hashedPayload :  {hashedPayload}");

            var x_mspdate = DateTime.Now.ToString("R");  //"Wed, 12 Oct 2022 16:26:36 GMT"; //  // Thu, 06 Oct 2022 19:49:43 GMT  //DateTime.UtcNow; 

            _logger.LogInformation($"x_mspdate :  {x_mspdate}");

            DateTime x = DateTime.Parse(x_mspdate);

            var unixTimestamp = ((DateTimeOffset)x).ToUnixTimeSeconds();

            //_logger.LogInformation($"unixTimestamp :  {unixTimestamp}");



            StringBuilder strToSign = new StringBuilder();
            strToSign.Append("POST");

            strToSign.Append(serveletPath);

            strToSign.Append(unixTimestamp);
            strToSign.Append(hashedPayload);


            string signature = HexString2B64String(ComputeHMAC_SHA1(strToSign.ToString(), BAXI_SEC_TOKEN));

            _logger.LogInformation($"signature :  {signature}");

            string authHeader = "Baxi" + " " + baxi_Username + ":" + signature;

            _logger.LogInformation($"authHeader :  {authHeader}");

            httpClient.DefaultRequestHeaders.Add("Authorization", authHeader); //[{"key":"x-msp-date","value":"{{x-msp-date}}","type":"text"}]
            httpClient.DefaultRequestHeaders.Add("x-msp-date", x_mspdate); // x_mspdate.ToString("R"));


            var httpRequest = new HttpRequestMessage(HttpMethod.Post, gatewayURL);
            _logger.LogInformation($"Calling BillerPayAsync  {httpRequest.RequestUri}");

            using (var httpContent = CreateHttpContent(paymentRequest))
            {
                httpRequest.Content = httpContent;

                using (var response = await httpClient
                    .SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead)
                    .ConfigureAwait(false))
                {
                    //response.EnsureSuccessStatusCode();
                    if (!response.IsSuccessStatusCode)
                    {
                        _logger.LogInformation($"api call BillerPayAsync not SuccessStatus . it returned with statusCode {response.StatusCode} reason: {response.ReasonPhrase}");
                        var errorStream = await response.Content.ReadAsStringAsync();

                        //var validationErrors = errorStream.ReadAndDeserializeFromJson();
                        _logger.LogWarning($"api call BillerPayAsync returned with status code: {response.StatusCode} validationErrors: -- {errorStream} --");
                        //var dstverror = JsonConvert.DeserializeObject<DstvError>(errorStream.ToString());

                        result = JsonConvert.DeserializeObject<BillPaymentsResponse>(errorStream);

                    }
                    if (response.IsSuccessStatusCode)
                    {
                        string contentStream = await response.Content.ReadAsStringAsync();
                        _logger.LogInformation($"api call BillerPayAsync returned with contentstream  {contentStream}");
                        result = JsonConvert.DeserializeObject<BillPaymentsResponse>(contentStream);
                    }

                }
            }

            return result;
        }
        public async Task<BillPaymentsResponse> BillerPayAsyncV2(string paymentRequest, CancellationToken cancellationToken)
        {

            _logger.LogInformation($"Inside BillerPayAsync service request");

            BillPaymentsResponse result = new BillPaymentsResponse();

            string gatewayURL = _configuration["BaxiBillsAPI:URL"];

            var httpClient = _httpFactory.CreateClient("BaxiBillsAPI");

            string baxi_Username = _configuration["BaxiBillsAPI:BAXI_USERNAME"];
            string BAXI_SEC_TOKEN = _configuration["BaxiBillsAPI:BAXI_SEC_TOKEN"];
            string serveletPath = _configuration["BaxiBillsAPI:ServletPath"];

            _logger.LogInformation($"baxiusername :  {baxi_Username}");
            _logger.LogInformation($"BAXI_SEC_TOKEN :  {BAXI_SEC_TOKEN}");
            _logger.LogInformation($"serveletPath :  {serveletPath}");

            string requestBody = JsonConvert.SerializeObject(paymentRequest);

            _logger.LogInformation($"requestBody :  {requestBody}");


            string sha256 = sha256hash(requestBody);

            //_logger.LogInformation($"sha256 :  {sha256}");

            string hashedPayload = HexString2B64String(sha256);
            
            //_logger.LogInformation($"hashedPayload :  {hashedPayload}");

            var x_mspdate = DateTime.Now.ToString("R");  // "Thu, 06 Oct 2022 20:41:45 GMT"; //   //DateTime.UtcNow; 

            //_logger.LogInformation($"x_mspdate :  {x_mspdate}");

            DateTime x = DateTime.Parse(x_mspdate);

            var unixTimestamp = ((DateTimeOffset)x).ToUnixTimeSeconds();

            //_logger.LogInformation($"unixTimestamp :  {unixTimestamp}");
            


            StringBuilder strToSign = new StringBuilder();
            strToSign.Append("POST");
            
            strToSign.Append(serveletPath);
            
            strToSign.Append(unixTimestamp);
            strToSign.Append(hashedPayload);


            string signature = HexString2B64String(ComputeHMAC_SHA1(strToSign.ToString(), BAXI_SEC_TOKEN));

            //_logger.LogInformation($"signature :  {signature}");

            string authHeader = "Baxi" + " " + baxi_Username + ":" + signature;


            _logger.LogInformation($"authHeader :  {authHeader}");

            httpClient.DefaultRequestHeaders.Add("Authorization", authHeader); //[{"key":"x-msp-date","value":"{{x-msp-date}}","type":"text"}]
            httpClient.DefaultRequestHeaders.Add("x-msp-date", x_mspdate); // x_mspdate.ToString("R"));


            var httpRequest = new HttpRequestMessage(HttpMethod.Post, gatewayURL);
            _logger.LogInformation($"Calling BillerPayAsync  {httpRequest.RequestUri}");

            using (var httpContent = CreateHttpContent(paymentRequest))
            {
                httpRequest.Content = httpContent;

                using (var response = await httpClient
                    .SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead)
                    .ConfigureAwait(false))
                {
                    //response.EnsureSuccessStatusCode();
                    if (!response.IsSuccessStatusCode)
                    {
                        _logger.LogInformation($"api call BillerPayAsync returned with statusCode {response.StatusCode} reason: {response.ReasonPhrase}");
                        var errorStream = await response.Content.ReadAsStringAsync();

                        //var validationErrors = errorStream.ReadAndDeserializeFromJson();
                        _logger.LogWarning($"api call BillerPayAsync returned with status code: {response.StatusCode} validationErrors: -- {errorStream} --");
                        //var dstverror = JsonConvert.DeserializeObject<DstvError>(errorStream.ToString());

                        result = JsonConvert.DeserializeObject<BillPaymentsResponse>(errorStream.ToString());

                    }
                    if (response.IsSuccessStatusCode)
                    {
                        string contentStream = await response.Content.ReadAsStringAsync();
                        _logger.LogInformation($"api call BillerPayAsync returned with contentstream  {contentStream}");
                        result = JsonConvert.DeserializeObject<BillPaymentsResponse>(contentStream);
                    }

                }
            }

            return result;
        }

        public async Task<ProxyResponse> ProxyAsync(ProxyRequest proxyRequest, CancellationToken cancellationToken)
        {

            _logger.LogInformation($"Inside ProxyAsync service request with service id: {proxyRequest.serviceId}");
            string requestBodyString = string.Empty;
            object requestBodyObj = null;

            switch (proxyRequest.serviceId)
            {
                //var dto =_mapper.Map<DestinationDto>(SourceDto);
                case "CIA":
                    requestBodyObj = _mapper.Map<ProxyRequest, ProxyAbujaPrepaid>(proxyRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "CIB":
                    requestBodyObj = _mapper.Map<ProxyRequest, ProxyAbujaPostpaid>(proxyRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "ANB":
                    requestBodyObj = _mapper.Map<ProxyRequest, ProxyValidateSmileBundleCustomer>(proxyRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "AWA":
                    if(proxyRequest.details.smartCardNumber != null)
                    {
                        requestBodyObj = _mapper.Map<ProxyRequest, ProxyStartimes>(proxyRequest);
                        requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    }
                    else
                    {
                        requestBodyObj = _mapper.Map<ProxyRequest, ProxyStartimesGetBundle>(proxyRequest);
                        requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    }
                    
                    break;
                case "BCA":
                    if(proxyRequest.details.vehicleManufacter != null)
                    {
                        requestBodyObj = _mapper.Map<ProxyRequest, ProxyCornerStoneCarModels>(proxyRequest);
                        requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    }
                    else if (proxyRequest.details.policy != null)
                    {
                        requestBodyObj = _mapper.Map<ProxyRequest, ProxyCornerSton3rdpartyInsuranceQuotation>(proxyRequest);
                        requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    }
                    else if (proxyRequest.details.policyId != null)
                    {
                        requestBodyObj = _mapper.Map<ProxyRequest, ProxyCornerStoneGetPolicyDetails>(proxyRequest);
                        requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    }
                    else
                    {
                        requestBodyObj = _mapper.Map<ProxyRequest, ProxyCornerstone>(proxyRequest);
                        requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    }
                    
                    break;
                case "AQA":
                    if (proxyRequest.details.number != null)
                    {
                        requestBodyObj = _mapper.Map<ProxyRequest, ProxyMultichoiceValidateSmartcardNo>(proxyRequest);
                        requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    }
                    else if (proxyRequest.details.invoicePeriod != null && proxyRequest.details.number != null)
                    {
                        requestBodyObj = _mapper.Map<ProxyRequest, ProxyMultichoiceGetBouquet>(proxyRequest);
                        requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    }
                    else if (proxyRequest.details.primaryProductCode != null)
                    {
                        requestBodyObj = _mapper.Map<ProxyRequest, ProxyMultichoiceProductAddon>(proxyRequest);
                        requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    }
                    else
                    {
                        requestBodyObj = _mapper.Map<ProxyRequest, ProxyMultichoice>(proxyRequest);
                        requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    }
                    break;
                case "AQC":
                    if (proxyRequest.details.number != null)
                    {
                        requestBodyObj = _mapper.Map<ProxyRequest, ProxyMultichoiceValidateSmartcardNo>(proxyRequest);
                        requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    }
                    else if (proxyRequest.details.invoicePeriod != null && proxyRequest.details.number != null)
                    {
                        requestBodyObj = _mapper.Map<ProxyRequest, ProxyMultichoiceGetBouquet>(proxyRequest);
                        requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    }
                    else if (proxyRequest.details.primaryProductCode != null)
                    {
                        requestBodyObj = _mapper.Map<ProxyRequest, ProxyMultichoiceProductAddon>(proxyRequest);
                        requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    }
                    else
                    {
                        requestBodyObj = _mapper.Map<ProxyRequest, ProxyMultichoice>(proxyRequest);
                        requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    }
                    break;
                case "BIA":
                    requestBodyObj = _mapper.Map<ProxyRequest, ProxyPortharcoutPrepaid>(proxyRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "BIB":
                    requestBodyObj = _mapper.Map<ProxyRequest, ProxyPortharcoutPostpaid>(proxyRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "BAA":
                    requestBodyObj = _mapper.Map<ProxyRequest, ProxyEkoPrepaid>(proxyRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "AVA":
                    requestBodyObj = _mapper.Map<ProxyRequest, ProxyEkoPostpaid>(proxyRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "CKA":
                    requestBodyObj = _mapper.Map<ProxyRequest, ProxyJosPrepaid>(proxyRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "CKB":
                    requestBodyObj = _mapper.Map<ProxyRequest, ProxyJosPostpaid>(proxyRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "APA":
                    requestBodyObj = _mapper.Map<ProxyRequest, ProxyIkejaPostpaid>(proxyRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "APB":
                    requestBodyObj = _mapper.Map<ProxyRequest, ProxyIkejaPrepaid>(proxyRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "AUB":
                    requestBodyObj = _mapper.Map<ProxyRequest, ProxyIbadanPrepaid>(proxyRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "AUA":
                    requestBodyObj = _mapper.Map<ProxyRequest, ProxyIbadanPostpaid>(proxyRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "AVB":
                    requestBodyObj = _mapper.Map<ProxyRequest, ProxyKedcoPrepaid>(proxyRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "AVC":
                    requestBodyObj = _mapper.Map<ProxyRequest, ProxyKedcoPostpaid>(proxyRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "CDA":
                    requestBodyObj = _mapper.Map<ProxyRequest, ProxyKadunaPrepaid>(proxyRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "CDB":
                    requestBodyObj = _mapper.Map<ProxyRequest, ProxyKadunaPostpaid>(proxyRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "ASA":
                    requestBodyObj = _mapper.Map<ProxyRequest, ProxyWAECPIN>(proxyRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "AOA":
                    requestBodyObj = _mapper.Map<ProxyRequest, ProxyBulkSMS>(proxyRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "ANA":
                    requestBodyObj = _mapper.Map<ProxyRequest, ProxySmileRecharge>(proxyRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "BGA":
                    requestBodyObj = _mapper.Map<ProxyRequest, ProxySpectranetPIN>(proxyRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "BGB":
                    requestBodyObj = _mapper.Map<ProxyRequest, ProxySpectranetValidateAcctNo>(proxyRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "CLA":
                    requestBodyObj = _mapper.Map<ProxyRequest, ProxyCarpaddy>(proxyRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                case "CPA":
                    requestBodyObj = _mapper.Map<ProxyRequest, ProxyShowmaxVouchers>(proxyRequest);
                    requestBodyString = JsonConvert.SerializeObject(requestBodyObj);
                    break;
                
                default:
                    break;
            }

            ProxyResponse result = new ProxyResponse();

            string gatewayURL = _configuration["BaxiBillsAPI:URL"];
            var httpClient = _httpFactory.CreateClient("BaxiBillsAPI");
            string baxi_Username = _configuration["BaxiBillsAPI:BAXI_USERNAME"];
            string BAXI_SEC_TOKEN = _configuration["BaxiBillsAPI:BAXI_SEC_TOKEN"];
            string serveletPath = _configuration["BaxiBillsAPI:ServletPath"] + "/proxy"; ;

            _logger.LogInformation($"baxiusername :  {baxi_Username}");
            _logger.LogInformation($"BAXI_SEC_TOKEN :  {BAXI_SEC_TOKEN}");
            _logger.LogInformation($"serveletPath :  {serveletPath}");


            _logger.LogInformation($"requestBody :  {requestBodyString}");

            string sha256 = sha256hash(requestBodyString);

            //_logger.LogInformation($"sha256 :  {sha256}");

            string hashedPayload = HexString2B64String(sha256);

            //_logger.LogInformation($"hashedPayload :  {hashedPayload}");

            var x_mspdate = DateTime.Now.ToString("R");  //"Wed, 12 Oct 2022 16:26:36 GMT"; //  // Thu, 06 Oct 2022 19:49:43 GMT  //DateTime.UtcNow; 

            _logger.LogInformation($"x_mspdate :  {x_mspdate}");

            DateTime x = DateTime.Parse(x_mspdate);

            var unixTimestamp = ((DateTimeOffset)x).ToUnixTimeSeconds();



            StringBuilder strToSign = new StringBuilder();
            strToSign.Append("POST");
            //strToSign.Append( Environment.NewLine);
            strToSign.Append(serveletPath);
            //strToSign.Append(Environment.NewLine);
            strToSign.Append(unixTimestamp);
            strToSign.Append(hashedPayload);



            string signature = HexString2B64String(ComputeHMAC_SHA1(strToSign.ToString(), BAXI_SEC_TOKEN));

            _logger.LogInformation($"signature :  {signature}");

            string authHeader = "Baxi" + " " + baxi_Username + ":" + signature;

            _logger.LogInformation($"authHeader :  {authHeader}");

            httpClient.DefaultRequestHeaders.Add("Authorization", authHeader); 
            httpClient.DefaultRequestHeaders.Add("x-msp-date", x_mspdate); 


            var httpRequest = new HttpRequestMessage(HttpMethod.Post, gatewayURL + "/proxy");
            _logger.LogInformation($"Calling ProxyAsync  {httpRequest.RequestUri}");

            using (var httpContent = CreateHttpContent(requestBodyObj))
            {
                httpRequest.Content = httpContent;

                using (var response = await httpClient
                    .SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead)
                    .ConfigureAwait(false))
                {
                    //response.EnsureSuccessStatusCode();
                    if (!response.IsSuccessStatusCode)
                    {
                        _logger.LogInformation($"api call ProxyAsync returned with statusCode {response.StatusCode} reason: {response.ReasonPhrase}");
                        var errorStream = await response.Content.ReadAsStringAsync();

                        //var validationErrors = errorStream.ReadAndDeserializeFromJson();
                        _logger.LogWarning($"api call ProxyAsync returned with status code: {response.StatusCode} validationErrors: -- {errorStream} --");
                        //var dstverror = JsonConvert.DeserializeObject<DstvError>(errorStream.ToString());

                        result = JsonConvert.DeserializeObject<ProxyResponse>(errorStream.ToString());

                    }
                    if (response.IsSuccessStatusCode)
                    {
                        string contentStream = await response.Content.ReadAsStringAsync();
                        _logger.LogInformation($"api call ProxyAsync returned with contentstream  {contentStream}");
                        result = JsonConvert.DeserializeObject<ProxyResponse>(contentStream);
                    }

                }
            }

            return result;
        }
        public async Task<List<ServiceListResponse>> ServiceListAsync()
        {
            _logger.LogInformation($"Inside ServiceListAsync service request");

            List<ServiceListResponse> result = new List<ServiceListResponse>();

            string gatewayURL = _configuration["BaxiBillsAPI:URL"];

            var httpClient = _httpFactory.CreateClient("BaxiBillsAPI");

            string baxi_Username = _configuration["BaxiBillsAPI:BAXI_USERNAME"];
            string BAXI_SEC_TOKEN = _configuration["BaxiBillsAPI:BAXI_SEC_TOKEN"];
            string serveletPath = _configuration["BaxiBillsAPI:ServletPath"] + "/services";

            _logger.LogInformation($"baxiusername :  {baxi_Username}");
            _logger.LogInformation($"BAXI_SEC_TOKEN :  {BAXI_SEC_TOKEN}");
            _logger.LogInformation($"serveletPath :  {serveletPath}");

            string requestBody = string.Empty;
            string sha256 = string.Empty;
            string hashedPayload = "";

            
            //string jsonRequest = ""; // JsonConvert.SerializeObject(paymentRequest); 

            //if (!string.IsNullOrEmpty(jsonRequest))
            //{
            //    sha256 = sha256hash(JsonConvert.SerializeObject(paymentRequest););
            //}
            

            
            //Console.WriteLine("hashedPayload=" + hashedPayload);
            var x_mspdate = DateTime.Now.ToString("R");  

            _logger.LogInformation($"x_mspdate :  {x_mspdate}");

            DateTime x = DateTime.Parse(x_mspdate);

            var unixTimestamp = ((DateTimeOffset)x).ToUnixTimeSeconds();


            StringBuilder strToSign = new StringBuilder();
            strToSign.Append("GET");
            //strToSign.Append( Environment.NewLine);
            strToSign.Append(serveletPath);
            //strToSign.Append(Environment.NewLine);
            strToSign.Append(unixTimestamp);
            strToSign.Append(hashedPayload);


            string signature = HexString2B64String(ComputeHMAC_SHA1(strToSign.ToString(), BAXI_SEC_TOKEN));

            _logger.LogInformation($"signature :  {signature}");

            string authHeader = "Baxi" + " " + baxi_Username + ":" + signature;

            _logger.LogInformation($"authHeader :  {authHeader}");


            httpClient.DefaultRequestHeaders.Add("Authorization", authHeader); 
            httpClient.DefaultRequestHeaders.Add("x-msp-date", x_mspdate); 


            var httpRequest = new HttpRequestMessage(HttpMethod.Get, gatewayURL + "/services");
            _logger.LogInformation($"Calling ServiceListAsync  {httpRequest.RequestUri}");

            using (var response = await httpClient
                   .SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead)
                   .ConfigureAwait(false))
            {
                //response.EnsureSuccessStatusCode();
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"api call ServiceListAsync returned with statusCode {response.StatusCode} reason: {response.ReasonPhrase}");
                    var errorStream = await response.Content.ReadAsStringAsync();

                    //var validationErrors = errorStream.ReadAndDeserializeFromJson();
                    _logger.LogWarning($"api call ServiceListAsync returned with status code: {response.StatusCode} validationErrors: -- {errorStream} --");
                    //var dstverror = JsonConvert.DeserializeObject<DstvError>(errorStream.ToString());

                    result = JsonConvert.DeserializeObject<List<ServiceListResponse>>(errorStream.ToString());

                }
                if (response.IsSuccessStatusCode)
                {
                    string contentStream = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation($"api call ServiceListAsync returned with contentstream  {contentStream}");
                    result = JsonConvert.DeserializeObject<List<ServiceListResponse>>(contentStream);
                }

            }

            return result;
        }

        #endregion
        /// <summary>
        /// serialize to stream instead of string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="stream"></param>
        public static void SerializeJsonIntoStream(object value, Stream stream)
        {
            using (var sw = new StreamWriter(stream, new UTF8Encoding(false), 1024, true))
            using (var jtw = new JsonTextWriter(sw) { Formatting = Formatting.None })
            {
                var js = new JsonSerializer();
                js.Serialize(jtw, value);
                jtw.Flush();
            }
        }
        private static HttpContent CreateHttpContent(object content)
        {
            HttpContent httpContent = null;
            if (content != null)
            {
                var ms = new MemoryStream();
                SerializeJsonIntoStream(content, ms);
                ms.Seek(0, SeekOrigin.Begin);
                httpContent = new StreamContent(ms);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }
            return httpContent;
        }
        

        public static string ComputeHMAC_SHA1(string input, string keystring)
        {
            byte[] key = Encoding.UTF8.GetBytes(keystring);
            HMACSHA1 myhmacsha1 = new HMACSHA1(key);
            byte[] byteArray = Encoding.ASCII.GetBytes(input);
            MemoryStream stream = new MemoryStream(byteArray);
            return myhmacsha1.ComputeHash(stream).Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);
        }

        
        public static string sha256hash(string stringtohash)
        {

            //SHA256Managed hasher = new SHA256Managed();

            //byte[] pwdBytes = new UTF8Encoding().GetBytes(stringtohash);
            //byte[] keyBytes = hasher.ComputeHash(pwdBytes);

            //hasher.Dispose();
            //return Convert.ToBase64String(keyBytes);


            Encoding enc = Encoding.UTF8;
            var hashBuilder = new StringBuilder();
            using var hash = SHA256.Create();
            byte[] result = hash.ComputeHash(enc.GetBytes(stringtohash));
            foreach (var b in result)
                hashBuilder.Append(b.ToString("x2"));
            string hashResult = hashBuilder.ToString();
            return hashResult;

            //SHA256Managed is obsolete
            //var crypt = new System.Security.Cryptography.SHA256Managed();
            //var hash = new System.Text.StringBuilder();
            //byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(stringtohash));
            //foreach (byte theByte in crypto)
            //{
            //    hash.Append(theByte.ToString("x2"));
            //}
            //return hash.ToString();
        }

        
        public static string HexString2B64String(string input)
        {
            return System.Convert.ToBase64String(HexStringToHex(input));
        }
        public static byte[] HexStringToHex(string inputHex)
        {
            var resultantArray = new byte[inputHex.Length / 2];
            for (var i = 0; i < resultantArray.Length; i++)
            {
                resultantArray[i] = System.Convert.ToByte(inputHex.Substring(i * 2, 2), 16);
            }
            return resultantArray;
        }
        private static string sha256generator_(string strtoHash)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(strtoHash));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
        private static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

    }
}
