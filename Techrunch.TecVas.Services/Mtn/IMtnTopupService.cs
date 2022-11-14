using Techrunch.TecVas.Entities.Common;
using Techrunch.TecVas.Entities.EtopUp;
using Techrunch.TecVas.Entities.EtopUp.Mtn;
using Techrunch.TecVas.Entities.EtopUp.NineMobile;
using System.Threading;
using System.Threading.Tasks;

namespace Techrunch.TecVas.Services.Mtn
{
    public interface IMtnTopupService
    {
        Task<MtnResponseEnvelope.Envelope> AirtimeRecharge(PinlessRechargeRequest pinlessRechargeRequest);
        Task<MtnResponseEnvelope.Envelope> DataRecharge(PinlessRechargeRequest pinlessRechargeRequest);
        Task<AccessTokenResponse> GetAccessToken();
        Task<MtnSubscriptionResponse> MtnSubscription(PinlessRechargeRequest pinlessRechargeRequest);
        Task<QueryTxnStatusResponse> QueryTransactionStatus(QueryTransactionStatusRequest statusRequest);
        Task<MtnQTxResponseEnvelope.Envelope> QueryTransactionStatusbyERSRef(QueryTransactionStatusRequest queryTransaction);
        Task<MtnQTxResponseEnvelope.Envelope> QueryTransactionStatusbyClientRef(QueryTransactionStatusRequest queryTransaction);
        Task<ProductListResponse> GetMtnProducts();
    }
}