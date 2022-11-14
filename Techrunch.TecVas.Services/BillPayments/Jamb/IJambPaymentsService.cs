using Techrunch.TecVas.Entities.BillPayments;
using Techrunch.TecVas.Entities.BillPayments.Waec;
using Techrunch.TecVas.Services.BillPayments.Jamb;
using System.Threading;
using System.Threading.Tasks;

namespace Techrunch.TecVas.Services.BillPayments.Jamb
{
    public interface IJambPaymentsService
    {
        Task<BillPaymentsResponse> JambPINPaymentAsync(JambPINRequest paymentRequest, CancellationToken cancellationToken);
    }
}