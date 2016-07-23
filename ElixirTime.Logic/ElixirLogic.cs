using ElixirTime.Service;
using ElixirTime.Service.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace ElixirTime.Logic
{
    public class ElixirLogic : IElixirLogic
    {
        private IElixirService elixirService;

        public ElixirLogic(IElixirService elixirService)
        {
            this.elixirService = elixirService;
        }

        public async Task<List<BankModel>> GetBankList()
        {
            var result = await elixirService.GetBanks();

            return result.ToList();
        }

        public string GetBankIdFromIBAN(string iban)
        {
            var result = string.Empty;

            if(iban.Length > 5)
            {
                result = iban.Substring(2, 3);
            }

            return result;
        }

        public async Task<DateTime> GetTransferTime(DateTime sendTime, BankModel source, BankModel target)
        {
            if(source == null || target == null)
            {
                throw new ArgumentException("Source and Target cannot be null");
            }

            if (source.Identifier == target.Identifier)
            {
                return sendTime;
            }

            var model = new TransferModel()
            {
                SendTime = sendTime,
                SenderBank = source.Identifier,
                TargetBank = source.Identifier
            };

            var result = await elixirService.GetElixirTime(model);

            return result.TransferTime;
        }
    }
}
