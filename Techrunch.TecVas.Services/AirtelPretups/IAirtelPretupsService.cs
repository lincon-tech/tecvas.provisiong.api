using Techrunch.TecVas.Entities.EtopUp;
using Techrunch.TecVas.Entities.EtopUp.Pretups;
using System.Threading.Tasks;

namespace Techrunch.TecVas.Services.AirtelPretups
{
    public interface IAirtelPretupsService
    {
        Task<PretupsRechargeResponseEnvelope.COMMAND> AirtimeRecharge(PinlessRechargeRequest pinRechargeRequest);
        Task<PretupsRechargeResponseEnvelope.COMMAND> DataRecharge(PinlessRechargeRequest pinRechargeRequest);
        Task<QueryTxnStatusResponse> QueryTransactionStatus(QueryTransactionStatusRequest queryTransactionStatusRequest);
    }
}