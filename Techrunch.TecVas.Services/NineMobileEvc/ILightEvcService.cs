using Techrunch.TecVas.Entities.EtopUp;
using Techrunch.TecVas.Entities.EtopUp.NineMobile;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Techrunch.TecVas.Services.NineMobileEvc
{
    public interface ILightEvcService
    {
        
        Task<RechargeResponseEnvelope.Envelope> PinlessRecharge(PinlessRechargeRequest pinlessRechargeRequest);
        Task<QueryBalanceResponseEnvelope.Envelope> QueryEvcBalance(QueryBalanceRequest queryBalanceRequest);
        Task<QueryTxnStatusResponse> QueryTransactionStatus(QueryTransactionStatusRequest statusRequest);
    }
}
