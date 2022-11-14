using Techrunch.TecVas.Entities.EtopUp;
using Techrunch.TecVas.Entities.EtopUp.Glo;
using System.Threading.Tasks;

namespace Techrunch.TecVas.Services.GloTopup
{
    public interface IGloTopupService
    {
        Task<GloAirtimeResultEnvelope.Envelope> GloAirtimeRecharge(PinlessRechargeRequest pinRechargeRequest);
        Task<GloDataResultEnvelope.Envelope> GloDataRecharge(PinlessRechargeRequest pinRechargeRequest);
        Task<QueryTxnStatusResponse> QueryTransactionStatus(QueryTransactionStatusRequest queryTransaction);
    }
}