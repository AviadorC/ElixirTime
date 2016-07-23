using ElixirTime.Service.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ElixirTime.Service.Test
{
    [TestClass]
    public class ElixirServiceTest
    {
        [TestMethod]
        public void GetBanks_ShouldGet()
        {
            var elixir = new ElixirService();
            var banks = elixir.GetBanks().Result;

            Assert.IsNotNull(banks);
        }

        [TestMethod]
        public void GetElixirTime_ShouldGet()
        {
            TransferModel model = new TransferModel {
                SenderBank = "254",
                TargetBank = "191",
                SendTime = new DateTime(2016, 06, 17, 13, 0, 0)
            };

            var elixir = new ElixirService();
            var banks = elixir.GetElixirTime(model).Result;

            Assert.IsNotNull(banks);
        }
    }
}
