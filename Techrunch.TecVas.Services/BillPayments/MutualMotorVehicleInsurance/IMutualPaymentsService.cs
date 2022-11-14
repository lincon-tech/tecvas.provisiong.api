using Techrunch.TecVas.Entities.BillPayments;
using Techrunch.TecVas.Entities.BillPayments.Mutual;
using Techrunch.TecVas.Entities.BillPayments.Waec;
using Techrunch.TecVas.Services.BillPayments.Jamb;
using System.Threading;
using System.Threading.Tasks;

namespace Techrunch.TecVas.Services.BillPayments.MutualMotorVehicleInsurance
{
    public interface IMutualPaymentsService
    {
        Task<BillPaymentsResponse> MutualMVPaymentAsync(MutualMortorInsuranceRequest paymentRequest, CancellationToken cancellationToken);
    }
}