using PhoenixBank.Entities.Exceptions;
using System.Globalization;

namespace PhoenixBank.Entities.Accounts
{
    internal class TeenageAccount : VerificationAccount
    {
        public string MotherName { get; private set; }
        public ulong MotherRG { get; private set; }
        public double InitialBalance { get; set; }

        public TeenageAccount() { }

        public TeenageAccount(string firstName, string lastName, string email, ulong rg, DateTime birthdayDate, char gender, string motherName, ulong motherRG, double intialBalance)
            : base(firstName, lastName, email, rg, birthdayDate, gender)
        {
            MotherName = motherName;
            MotherRG = motherRG;
            InitialBalance = intialBalance;
        }

        public void Deposit(double amount)
        {
            if (amount <= 0.0) throw new DomainException("The amount requested must be greater than zero.");
            InitialBalance += amount;
        }
        public void Withdraw(double amount)
        {
            if (amount > InitialBalance) throw new DomainException("The requested amount is greater than the current value in the account.");
            if (amount <= 0.0) throw new DomainException("The amount requested must be greater than zero.");
            InitialBalance -= amount;
        }

        // Entrando na conta...
        public void EnterTeenageAccount()
        {
            Console.Clear();
            Console.WriteLine("Name: " + FirstName + " " + LastName);
            Console.WriteLine("Account Type: Commom Account");
            Console.WriteLine("Initial Balance: " + InitialBalance);
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

                // savings
                case 3:
                    SavingsAccount sA = new SavingsAccount();
                    sA.EnterSavingsAccount();
                    break;

                // exit
                case 4:
                    Environment.Exit(0);
                    break;

                // a different number
                default:
                    Console.WriteLine("Error! Invalid Number.");
                    break;
            }
        }
    }
}