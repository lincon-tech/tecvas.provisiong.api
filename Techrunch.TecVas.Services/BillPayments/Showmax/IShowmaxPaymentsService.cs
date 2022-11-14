using Techrunch.TecVas.Entities.BillPayments;
using Techrunch.TecVas.Entities.BillPayments.Showmax;
using System.Threading;
using System.Threading.Tasks;

namespace Techrunch.TecVas.Services.BillPayments.Showmax
{
    public interface IShowmaxPaymentsService
    {
        Task<BillPaymentsResponse> ShowmaxVoucehrPaymentAsync(ShowmaxVoucherRequest paymentRequest, CancellationToken cancellationToken);
    }
}