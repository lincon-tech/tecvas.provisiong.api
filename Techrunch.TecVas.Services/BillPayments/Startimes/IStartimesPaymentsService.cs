using Techrunch.TecVas.Entities.BillPayments;
using Techrunch.TecVas.Entities.BillPayments.Startimes;

using System.Threading;
using System.Threading.Tasks;

namespace Techrunch.TecVas.Services.BillPayments.Startimes
{
    public interface IStartimesPaymentsService
    {
        Task<BillPaymentsResponse> StartimesPaymentAsync(StartimesRequest paymentRequest, CancellationToken cancellationToken);
    }
}