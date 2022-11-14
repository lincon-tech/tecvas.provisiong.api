using Techrunch.TecVas.Entities.BillPayments;
using Techrunch.TecVas.Entities.BillPayments.Smile;
using Techrunch.TecVas.Entities.BillPayments.Waec;
using System.Threading;
using System.Threading.Tasks;

namespace Techrunch.TecVas.Services.BillPayments.Smile
{
    public interface ISmilePaymentsService
    {
        Task<BillPaymentsResponse> SmileBundlePaymentAsync(SmileCommBundleRequest paymentRequest, CancellationToken cancellationToken);
        Task<BillPaymentsResponse> SmileRechargePaymentAsync(SmileCommRechargeRequest paymentRequest, CancellationToken cancellationToken);
    }
}