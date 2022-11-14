using Techrunch.TecVas.Entities.BillPayments;
using Techrunch.TecVas.Entities.BillPayments.AbujaDisco;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Techrunch.TecVas.Services.BillPayments.AbujaDisco
{
    public interface IBillerPaymentsService
    {
        Task<BillPaymentsResponse> BillerPayAsync(BillpaymentRequest paymentRequest, CancellationToken cancellationToken);
        Task<ProxyResponse> ProxyAsync(ProxyRequest proxyRequest, CancellationToken cancellationToken);
        Task<List<ServiceListResponse>> ServiceListAsync();
    }
}