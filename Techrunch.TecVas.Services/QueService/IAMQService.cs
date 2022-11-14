using Techrunch.TecVas.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Techrunch.TecVas.Services.QueService
{
    public interface IAMQService
    {
        Task<bool> SubmitTopupOrder(RechargeRequest rechargeRequest);
    }
}
