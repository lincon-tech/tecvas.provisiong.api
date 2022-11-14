using Techrunch.TecVas.Entities.BillPayments;
using Techrunch.TecVas.Entities.BillPayments.BulkSMS;
using Techrunch.TecVas.Entities.BillPayments.Carpaddy;
using System.Threading;
using System.Threading.Tasks;

namespace Techrunch.TecVas.Services.BillPayments.Carpaddy
{
    public interface ICarpaddyPaymentsService
    {
        Task<BillPaymentsResponse> CarpaddyPaymentAsync(CarpaddyRequest paymentRequest, CancellationToken cancellationToken);
    }
}