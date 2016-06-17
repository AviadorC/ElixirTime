using ElixirTime.Service.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElixirTime.Service
{
    public class StubElixirService : IElixirService
    {
        public Task<IEnumerable<BankModel>> GetBanks()
        {
            return Task.FromResult(new List<BankModel>()
            {
                new BankModel()
                {
                    BankName = "Alior Bank",
                    Identifier = "249"
                },
                new BankModel()
                {
                    BankName = "Bank BPH",
                    Identifier = "106"
                },
                new BankModel()
                {
                    BankName = "Bank Millennium",
                    Identifier = "116"
                },
                new BankModel()
                {
                    BankName = "Bank Pocztowy",
                    Identifier = "132"
                },
                new BankModel()
                {
                    BankName = "BGŻ",
                    Identifier = "203"
                },
                new BankModel()
                {
                    BankName = "Citi Handlowy",
                    Identifier = "254"
                },
                new BankModel()
                {
                    BankName = "Deutsche Bank Polska",
                    Identifier = "191"
                },
                new BankModel()
                {
                    BankName = "Eurobank",
                    Identifier = "147"
                },
                new BankModel()
                {
                    BankName = "Getin Bank",
                    Identifier = "248"
                },
                new BankModel()
                {
                    BankName = "Idea Bank",
                    Identifier = "195"
                },
                new BankModel()
                {
                    BankName = "ING Bank",
                    Identifier = "105"
                },
                new BankModel()
                {
                    BankName = "mBank",
                    Identifier = "114"
                },
                new BankModel()
                {
                    BankName = "Pekao SA",
                    Identifier = "124"
                },
                new BankModel()
                {
                    BankName = "Raiffeisen",
                    Identifier = "175"
                },
                new BankModel()
                {
                    BankName = "Santander",
                    Identifier = "212"
                },
            } as IEnumerable<BankModel>);
        }

        public Task<TransferResultModel> GetElixirTime(TransferModel model)
        {
            var result = new TransferResultModel()
            {
                TransferTime = new DateTime(2016, 1, 1, 10, 0, 0)
            };

            return Task.FromResult(result);
        }
    }
}
