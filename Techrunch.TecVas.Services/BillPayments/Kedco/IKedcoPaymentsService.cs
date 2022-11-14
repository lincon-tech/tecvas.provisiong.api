using Techrunch.TecVas.Entities.BillPayments;
using Techrunch.TecVas.Entities.BillPayments.Kedco;
using System.Threading;
using System.Threading.Tasks;

namespace Techrunch.TecVas.Services.BillPayments.Kedco
{
    public interface IKedcoPaymentsService
    {
        Task<BillPaymentsResponse> KedcoPostpaidAsync(KedcoElectricPrepaidRequest paymentRequest, CancellationToken cancellationToken);
        Task<BillPaymentsResponse> KedcoPrepaidAsync(KedcoElectricPrepaidRequest paymentRequest, CancellationToken cancellationToken);
    }
}