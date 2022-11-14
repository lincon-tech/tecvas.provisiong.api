using AutoMapper;
using Techrunch.TecVas.Entities.BillPayments;
using Techrunch.TecVas.Entities.BillPayments.AbujaDisco;
using Techrunch.TecVas.Entities.BillPayments.ProxyResponses;
using Techrunch.TecVas.Entities.Common;
using Techrunch.TecVas.Entities.ViewModels;
using Techrunch.TecVas.Services.BillPayments;
using Techrunch.TecVas.Services.BillPayments.AbujaDisco;
using Techrunch.TecVas.Services.BillPayments.Proxy;
using Techrunch.TecVas.Services.QueService;
using Techrunch.TecVas.Services.TransactionRecordService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Techrunch.TecVas.Entities.BillPayments.ProxyResponse;
using static Techrunch.TecVas.Entities.BillPayments.ProxyResponses.ProxySmileResponses;
using static Techrunch.TecVas.Entities.BillPayments.ProxyResponses.ProxyStartimesResponses;
using Techrunch.TecVas.Provisioning.Api.Api.Helpers.Validation;
//using static Techrunch.TecVas.Provisioning.Entities.BillPayments.ProxyResponses.ProxyIkejaResponse;

namespace Techrunch.TecVas.Provisioning.Api.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/api/[controller]")]
    [ApiController]
    [Produces("application/json")]

    public class billerController : Controller
    {
        private readonly ILogger<billerController> _logger;

        private readonly IBillerPaymentsService _billspaymentService;
        private readonly IMapper _mapper;
        private readonly ITransactionRecordService _transactionRecordService;



        public billerController(
            ILogger<billerController> logger,
            ITransactionRecordService transactionRecordService,
            IBillerPaymentsService billspaymentService,
            IMapper mapper
            )
        {
            _logger = logger;
            _transactionRecordService = transactionRecordService;
            _billspaymentService = billspaymentService;
            _mapper = mapper;
        }


        [HttpPost("exchange")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BillPaymentsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BillPay(BillpaymentRequest renewRequest, CancellationToken cancellation)
        {
            //public async Task<IActionResult> BillPay([FromBody] string renewRequest, CancellationToken cancellation)
            await Task.Delay(0, cancellation).ConfigureAwait(false);
            try
            {

                /*
                 * 
                //string rawRequest = await this.Request.Content.ReadAsStringAsync();
                Encoding encoding = null;
                if (!Request.Body.CanSeek)
                {
                    // We only do this if the stream isn't *already* seekable,
                    // as EnableBuffering will create a new stream instance
                    // each time it's called
                    Request.EnableBuffering();
                }
                Request.Body.Position = 0;
                var reader = new StreamReader(Request.Body, encoding ?? Encoding.UTF8);
                var body = await reader.ReadToEndAsync().ConfigureAwait(false);
                Request.Body.Position = 0
                
                */
                //var rawRequestBody = await Request.GetRawBodyAsync();

                _logger.LogInformation($"Inside API Call biller.exchange with request : {JsonConvert.SerializeObject(renewRequest)}");



                if (ModelState.IsValid)
                {

                    string apikey = (string)HttpContext.Request.Headers["x-api-key"];

                    //_logger.LogInformation($"my API Key : {apikey}");
                    //apikey = "CHAMSS-PHExG8qddpxcKduT72VesGoa4Z";
                    var partnerKey = await _transactionRecordService.GetPartnerbyAPIkey(apikey);

                    if (partnerKey == null)
                    {
                        return Unauthorized(new
                        {
                            status = "2001",
                            responseMessage = "Authorization Error : Invalid API KEY"
                        });
                    }
                    // check balance


                    int billpayentsCategory = (int)ProductCategory.BillPayments;
                    _logger.LogInformation($"Fetching wallet balance for partnerId {partnerKey.PartnerId}, productCategory {billpayentsCategory}");

                    var epurseBalance = _transactionRecordService.GetEpurseByPartnerIdCategoryId(partnerKey.PartnerId, billpayentsCategory);
                    if (epurseBalance == null)
                    {
                        return Ok(new
                        {
                            status = "20008",
                            responseMessage = "Product category not authorized for this partner"
                        });
                    }
                    //var newrequest = _mapper.Map<BillpaymentRequest, BillpaymentBaxiRequest>(renewRequest);
                    //var dto =_mapper.Map<DestinationDto>(SourceDto);

                    var apiresponse = await _billspaymentService.BillerPayAsync(renewRequest, cancellation);

                    return Ok(new
                    {
                        apiresponse
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        status = "99",
                        message = ModelState.GetErrorMessages()

                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Api failure in biller exchange with error message {ex.Message}  error details {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    status = "99",
                    message = $"Failed to submit biller exchange {JsonConvert.SerializeObject(renewRequest)}"

                });
            }

        }


        [HttpPost("proxy")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Proxy(
            ProxyRequest proxyRequest,
            CancellationToken cancellation)
        {
            await Task.Delay(0, cancellation).ConfigureAwait(false);
            try
            {
                _logger.LogInformation($"Inside API Call biller.proxy with request {JsonConvert.SerializeObject(proxyRequest)}");



                if (ModelState.IsValid)
                {
                    var apikey = (string)HttpContext.Request.Headers["x-api-key"];

                    var partner = _transactionRecordService.GetPartnerbyAPIkey(apikey);

                    if (partner == null)
                    {
                        _logger.LogInformation($"failed to fetch partner info");
                        return Unauthorized(new
                        {
                            status = "2001",
                            responseMessage = "Authorization Error : Invalid API KEY"
                        });
                    }
                    var apiRsponse = await _billspaymentService.ProxyAsync(proxyRequest, cancellation);

                    _logger.LogInformation($"mapping {proxyRequest.serviceId} back to response model .apiRsponse : {JsonConvert.SerializeObject(apiRsponse)}");
                    switch (proxyRequest.serviceId)
                    {
                        case "APB":
                            _logger.LogInformation($"case APB  ");
                            ProxyIkejaResponseDetails ikejarsp = _mapper.Map<ProxyResponseDetails, ProxyIkejaResponseDetails>(apiRsponse.details);
                            _logger.LogInformation($"ikj response : {JsonConvert.SerializeObject(ikejarsp)} ");
                            return Ok(

                                ikejarsp

                            );
                            break;
                        case "ANB":
                            if (proxyRequest.details.requestType == "VALIDATE_ACCOUNT")
                            {
                                ProxySmileValidateCustomerDetails smilearsp2 = _mapper.Map<ProxyResponseDetails, ProxySmileValidateCustomerDetails>(apiRsponse.details);
                                return Ok(

                                smilearsp2

                            );

                            }
                            else
                            {
                                ProxySmileGetbundlesDetails smilearsp1 = _mapper.Map<ProxyResponseDetails, ProxySmileGetbundlesDetails>(apiRsponse.details);
                                return Ok(

                                smilearsp1

                                );
                            }
                        case "AWA":
                            if (proxyRequest.details.requestType == "GET_CUSTOMER_BALANCE")
                            {
                                ProxyStartimesValidateCustomerDetails rsp2 = _mapper.Map<ProxyResponseDetails, ProxyStartimesValidateCustomerDetails>(apiRsponse.details);
                                return Ok(

                                rsp2

                            );

                            }
                            else
                            {
                                ProxyStartimesValidateCustomerDetails rsp1 = _mapper.Map<ProxyResponseDetails, ProxyStartimesValidateCustomerDetails>(apiRsponse.details);
                                return Ok(

                                rsp1

                                );
                            }

                            break;
                        default:
                            return Ok(new
                            {
                                apiRsponse
                            }); ;
                    }

                    //return Ok(new
                    //{
                    //    apiRsponse
                    //});
                }
                else
                {
                    return BadRequest(new
                    {
                        status = "99",
                        message = ModelState.GetErrorMessages()

                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Api failure in proxy api with error message {ex.Message}  error details {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    status = "99",
                    message = $"Failed to submit proxy api  {JsonConvert.SerializeObject(proxyRequest)}"

                });
            }

        }


    }
    public static class RawRequestHelper
    {
        /// <summary>
        /// this method helps to accept raw string from a request 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static async Task<string> GetRawBodyAsync(this HttpRequest request, Encoding encoding = null)
        {
            if (!request.Body.CanSeek)
            {
                // We only do this if the stream isn't *already* seekable,
                // as EnableBuffering will create a new stream instance
                // each time it's called
                request.EnableBuffering();
            }

            request.Body.Position = 0;

            var reader = new StreamReader(request.Body, encoding ?? Encoding.UTF8);

            var body = await reader.ReadToEndAsync().ConfigureAwait(false);

            request.Body.Position = 0;

            return body;
        }
    }
}
