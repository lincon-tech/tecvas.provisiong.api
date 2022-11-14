using Techrunch.TecVas.Entities.BillPayments;
using Techrunch.TecVas.Entities.BillPayments.Waec;
using System.Threading;
using System.Threading.Tasks;

namespace Techrunch.TecVas.Services.BillPayments.Waec
{
    public interface IWaecPaymentsService
    {
        Task<BillPaymentsResponse> WaecPINPaymentAsync(WaecPINRequest paymentRequest, CancellationToken cancellationToken);
    }
}