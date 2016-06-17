using ElixirTime.Logic;
using ElixirTime.Service;
using ElixirTime.Service.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ElixirTime.Data
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private IElixirLogic elixirLogic;

        private List<BankModel> banks;

        public MainViewModel()
        {
            elixirLogic = new ElixirLogic(new ElixirService());
            //elixirLogic = new ElixirLogic(new StubElixirService());
        }

        public List<BankModel> Banks
        {
            get { return banks; }
            set
            {
                banks = value;
                OnPropertyChanged("Banks");
            }
        }

        public DateTime SendTime { get; set; }

        public BankModel Source { get; set; }

        public BankModel Target { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public async void Initialize()
        {
            Banks = await elixirLogic.GetBankList();
        }

        public async Task<DateTime?> CalculateTransfer(string sourceIban = null, string targetIban = null)
        {
            DateTime? result = null;

            if (!string.IsNullOrEmpty(sourceIban))
            {
                var sourceId = elixirLogic.GetBankIdFromIBAN(sourceIban);
                Source = Banks.Where(b => b.Identifier == sourceId).FirstOrDefault();
            }

            if (!string.IsNullOrEmpty(targetIban))
            {
                var targetId = elixirLogic.GetBankIdFromIBAN(targetIban);
                Target = Banks.Where(b => b.Identifier == targetId).FirstOrDefault();
            }

            try
            {
                result = await elixirLogic.GetTransferTime(SendTime, Source, Target);
            }
            catch (ArgumentException ex)
            {
                OnPropertyChanged("BankInvalid");
            }

            return result;
        }

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
