using System;
using System.Collections.Generic;

namespace ElixirTime.Common.Model
{
    public class BankModel
    {
        public string Identifier { get; set; }

        public string BankName { get; set; }

        public List<DateTime> IncomeSessions { get; set; }

        public List<DateTime> OutcomeSessions { get; set; }

        public override string ToString()
        {
            return BankName;
        }
    }
}
