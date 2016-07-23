using ElixirTime.Logic;
using ElixirTime.Service;
using ElixirTime.Service.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace ElixirTime.Logic.Test
{
    [TestClass]
    public class LogicTest
    {
        private IElixirLogic elixirLogic;

        [TestInitialize]
        public void Setup()
        {
            var elixirService = new StubElixirService();
            elixirLogic = new ElixirLogic(elixirService);
        }

        [TestMethod]
        public void GetBankList_ShouldGet()
        {
            var result = elixirLogic.GetBankList().Result;

            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void GetIdentifierFromIban_ShouldGet()
        {
            var result = elixirLogic.GetBankIdFromIBAN("98124013691138220834089189");

            Assert.AreEqual(result, "124");
        }

        [TestMethod]
        public void GetTransferTime_ShouldGet()
        {
            var source = new BankModel()
            {
                BankName = "Alior Bank",
                Identifier = "249"
            };

            var target = new BankModel()
            {
                BankName = "Bank BPH",
                Identifier = "106"
            };

            var result = elixirLogic.GetTransferTime(new DateTime(2016, 1, 1, 12, 0, 0), source, target).Result;

            Assert.AreEqual(result, new DateTime(2016, 1, 1, 10, 0, 0));
        }
    }
}
