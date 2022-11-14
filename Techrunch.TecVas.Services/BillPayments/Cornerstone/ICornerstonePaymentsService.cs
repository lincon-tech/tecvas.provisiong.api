using Techrunch.TecVas.Entities.BillPayments;
using Techrunch.TecVas.Entities.BillPayments.Cornerstone;
using System.Threading;
using System.Threading.Tasks;

namespace Techrunch.TecVas.Services.BillPayments.CornerStone
{
    public interface ICornerstonePaymentsService
    {
        Task<BillPaymentsResponse> CornerstonePaymentAsync(CornerstoneRequest paymentRequest, CancellationToken cancellationToken);
    }
}