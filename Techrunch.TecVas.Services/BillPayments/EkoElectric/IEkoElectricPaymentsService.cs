using Techrunch.TecVas.Entities.BillPayments;
using Techrunch.TecVas.Entities.BillPayments.EkoElectric;
using System.Threading;
using System.Threading.Tasks;

namespace Techrunch.TecVas.Services.BillPayments.EkoElectric
{
    public interface IEkoElectricPaymentsService
    {
        Task<BillPaymentsResponse> EkoElectricPostpaidAsync(EkoElectricPostpaidRequest paymentRequest, CancellationToken cancellationToken);
        Task<BillPaymentsResponse> EkoElectricPrepaidAsync(EkoElectricPrepaidRequest paymentRequest, CancellationToken cancellationToken);
    }
}