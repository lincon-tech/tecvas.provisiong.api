using Techrunch.TecVas.Entities.BillPayments;
using Techrunch.TecVas.Entities.BillPayments.Spectranet;
using Techrunch.TecVas.Entities.BillPayments.Waec;
using System.Threading;
using System.Threading.Tasks;

namespace Techrunch.TecVas.Services.BillPayments.Spectranet
{
    public interface ISpectranetPaymentsService
    {
        Task<BillPaymentsResponse> SpectranetRefillAsync(SpectranetRefillRequest paymentRequest, CancellationToken cancellationToken);
        Task<BillPaymentsResponse> SpectranetPaymentPlanAsync(SpectranetPaymentPlanRequest paymentRequest, CancellationToken cancellationToken);
        Task<BillPaymentsResponse> SpectranetPINAsync(SpectranetPINRequest paymentRequest, CancellationToken cancellationToken);
    }
}