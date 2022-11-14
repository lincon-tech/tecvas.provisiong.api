using Techrunch.TecVas.Entities.BillPayments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Techrunch.TecVas.Entities.BillPayments.Multichoice;
using static Techrunch.TecVas.Entities.BillPayments.Multichoice.GotvRenew;

namespace Techrunch.TecVas.Services.BillPayments.Multichoice
{
    public interface IMultichoicePaymentsService
    {
        Task<BillPaymentsResponse> DstvPaymentAsync(DstvRenewRequest dstvRequest, CancellationToken cancellationToken);
        Task<BillPaymentsResponse> DstvRenewRequestAsync(DstvRenewRequest dstvRequest, CancellationToken cancellationToken);
        Task<BillPaymentsResponse> DstvBoxOfficeRequestAsync(DstvBoxOfficeRequest dstvRequest, CancellationToken cancellationToken);
        Task<BillPaymentsResponse> GotvRequestAsync(GotvRequest dstvRequest, CancellationToken cancellationToken);
        Task<BillPaymentsResponse> GotvRenewAsync(GotvRenew dstvRequest, CancellationToken cancellationToken);
    }
}
