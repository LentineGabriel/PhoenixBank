namespace PhoenixBank.Entities.Accounts
{
    internal class SavingsAccount
    {
        public double InitialBalance { get; set; }
        public double Deposit { get; set; }

        public SavingsAccount() { }

        public SavingsAccount(double initialBalance, double deposit)
        {
            InitialBalance = initialBalance;
            Deposit = deposit;
        }

        public double SavingsDeposit(double initialBalance, double amount)
        {
            double res = InitialBalance += amount;
            return res;
        }
    }
}
