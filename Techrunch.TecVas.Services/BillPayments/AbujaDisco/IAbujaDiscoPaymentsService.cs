using Techrunch.TecVas.Entities.BillPayments;
using Techrunch.TecVas.Entities.BillPayments.AbujaDisco;
using System.Threading;
using System.Threading.Tasks;

namespace Techrunch.TecVas.Services.BillPayments.AbujaDisco
{
    public interface IAbujaDiscoPaymentsService
    {
        Task<BillPaymentsResponse> AbujaPostpaidAsync(AbujaPostpaidRequest paymentRequest, CancellationToken cancellationToken);
        Task<BillPaymentsResponse> AbujaPrepaidAsync(AbujaPrepaidRequest paymentRequest, CancellationToken cancellationToken);
    }
}