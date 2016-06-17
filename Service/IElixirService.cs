using ElixirTime.Service.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElixirTime.Service
{
    public interface IElixirService
    {
        Task<IEnumerable<BankModel>> GetBanks();

        Task<TransferResultModel> GetElixirTime(TransferModel model);
    }
}
