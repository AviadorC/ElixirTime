using ElixirTime.Service.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElixirTime.Logic
{
    public interface IElixirLogic
    {
        Task<List<BankModel>> GetBankList();

        string GetBankIdFromIBAN(string iban);

        Task<DateTime> GetTransferTime(DateTime sendTime, BankModel source, BankModel target);
    }
}