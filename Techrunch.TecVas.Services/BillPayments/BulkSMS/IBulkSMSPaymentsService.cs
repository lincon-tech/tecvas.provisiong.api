using Techrunch.TecVas.Entities.BillPayments;
using Techrunch.TecVas.Entities.BillPayments.BulkSMS;
using System.Threading;
using System.Threading.Tasks;

namespace Techrunch.TecVas.Services.BillPayments.BulkSMS
{
    public interface IBulkSMSPaymentsService
    {
        Task<BillPaymentsResponse> BulkSMSPaymentAsync(BulkSMSRequest paymentRequest, CancellationToken cancellationToken);
    }
}