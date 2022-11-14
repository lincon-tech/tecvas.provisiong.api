using Techrunch.TecVas.Entities.BillPayments;
using Techrunch.TecVas.Entities.BillPayments.JosElectricity;
using System.Threading;
using System.Threading.Tasks;

namespace Techrunch.TecVas.Services.BillPayments.JosElectric
{
    public interface IJosElectricPaymentsService
    {
        Task<BillPaymentsResponse> JosElectricPostpaidAsync(JosElectricPostPaidRequest paymentRequest, CancellationToken cancellationToken);
        Task<BillPaymentsResponse> JosElectricPrepaidAsync(JosElectricPrePaidRequest paymentRequest, CancellationToken cancellationToken);
    }
}