using Techrunch.TecVas.Entities.BillPayments;
using Techrunch.TecVas.Entities.BillPayments.Kaduna;
using Techrunch.TecVas.Entities.BillPayments.Kedco;
using System.Threading;
using System.Threading.Tasks;

namespace Techrunch.TecVas.Services.BillPayments.Kaduna
{
    public interface IKadunaPaymentsService
    {
        Task<BillPaymentsResponse> KadunaPostpaidAsync(KadunaElectricPrepaidRequest paymentRequest, CancellationToken cancellationToken);
        Task<BillPaymentsResponse> KadunaPrepaidAsync(KadunaElectricPrepaidRequest paymentRequest, CancellationToken cancellationToken);
    }
}