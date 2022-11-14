using Techrunch.TecVas.Entities.BillPayments;
using Techrunch.TecVas.Entities.BillPayments.IkejaElectric;
using System.Threading;
using System.Threading.Tasks;

namespace Techrunch.TecVas.Services.BillPayments.IkejaELectric
{
    public interface IIkejaElectricPaymentsService
    {
        Task<BillPaymentsResponse> IkejaElectricPostpaidAsync(IkejaElectricPostpaidRequest paymentRequest, CancellationToken cancellationToken);
        Task<BillPaymentsResponse> IkejaElectricPrepaidAsync(IkejaElectricTokenPurchaseRequest paymentRequest, CancellationToken cancellationToken);
    }
}