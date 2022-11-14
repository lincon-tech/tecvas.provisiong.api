using Techrunch.TecVas.Entities.BillPayments;
using Techrunch.TecVas.Entities.IbadanDisco;
using System.Threading;
using System.Threading.Tasks;

namespace Techrunch.TecVas.Services.BillPayments.IbadanDisco
{
    public interface IIbadanDiscoPaymentsService
    {
        Task<BillPaymentsResponse> IbadanDiscoPostpaidAsync(IbadanDiscoPostpaidRequest paymentRequest, CancellationToken cancellationToken);
        Task<BillPaymentsResponse> IbadanDiscoPrepaidAsync(IbadanDiscoPrepaidRequest paymentRequest, CancellationToken cancellationToken);
    }
}