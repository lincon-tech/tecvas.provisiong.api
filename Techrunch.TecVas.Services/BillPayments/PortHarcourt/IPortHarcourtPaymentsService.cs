using Techrunch.TecVas.Entities.BillPayments;
using Techrunch.TecVas.Entities.BillPayments.PortharcourtElectric;
using System.Threading;
using System.Threading.Tasks;

namespace Techrunch.TecVas.Services.BillPayments.PortHarcourt
{
    public interface IPortHarcourtPaymentsService
    {
        Task<BillPaymentsResponse> PortHarcourtPostpaidAsync(PortHarcourtElectricPostpaidRequest paymentRequest, CancellationToken cancellationToken);
        Task<BillPaymentsResponse> PortHarcourtPrepaidAsync(PortHarcourtElectricPrepaidRequest paymentRequest, CancellationToken cancellationToken);
    }
}