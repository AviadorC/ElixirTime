namespace ElixirTime.Service.Model
{
    public class BankModel
    {
        public string BankName { get; set; }

        public string Identifier { get; set; }

        public override string ToString()
        {
            return BankName;
        }
    }
}