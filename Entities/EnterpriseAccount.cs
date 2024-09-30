using System.Globalization;

namespace PhoenixBank.Entities
{
    internal class EnterpriseAccount : VerificationAccount
    {
        public ulong CNPJ { get; set; }
        public double InitialBalance { get; set; }
        public double WithdrawLimit { get; set; }
        public double Credit { get; set; }

        public EnterpriseAccount() { }

        public EnterpriseAccount(string firstName, string lastName, string email, ulong rg, DateTime birthdayDate, char gender, ulong cnpj, double initialBalance, double withdrawLimit, double credit)
            : base(firstName,lastName, email, rg, birthdayDate, gender)
        {
            CNPJ = cnpj;
            InitialBalance = initialBalance;
            WithdrawLimit = 100000.00;
            Credit = credit;
        }

        public void Deposit(double amount)
        {
            InitialBalance += amount;
        }
        public void Withdraw(double amount)
        {
            if(amount > WithdrawLimit) Console.WriteLine("The amount to be withdrawn is greater than the withdrawal limit.");
            else InitialBalance -= amount * 0.5;
        }

        // Entrando na conta...
        public void EnterEnterpriseAccount()
        {
            Console.Clear();
            Console.WriteLine("Name: " + FirstName + " " + LastName);
            Console.WriteLine("Account Type: Enterprise Account");
            Console.WriteLine("Initial Balance: " + InitialBalance);
            Console.WriteLine("Withdraw Limit: " + WithdrawLimit);

            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1 - Deposit");
            Console.WriteLine("2 - Withdraw");
            Console.WriteLine("3 - Exit");
            int op = int.Parse(Console.ReadLine());

            switch (op)
            {
                // deposit
                case 1:
                    Console.Write("Enter the amount you wish to deposit: "); double dAmount = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                    Deposit(dAmount);
                    Console.WriteLine("New Balance: " + InitialBalance);
                    break;

                // withdraw
                case 2:
                    Console.WriteLine("Enter the amount you wish to withdraw: "); double wAmount = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                    Withdraw(wAmount);
                    Console.WriteLine("New Balance: " + InitialBalance);
                    break;

                // exit
                case 3:
                    System.Environment.Exit(0);
                    break;

                // a different number
                default:
                    Console.WriteLine("Error! Invalid Number.");
                    break;
            }
        }
    }
}
