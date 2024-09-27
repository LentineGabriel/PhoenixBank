namespace PhoenixBank.Entities
{
    // Esta conta só vai estar disponível p/ aqueles cuja idade for igual ou maior que 18
    internal class CommomAccount : VerificationAccount
    {
        // NEW PROPERTIES
        public ulong CPF { get; set; }
        public double InitialBalance { get; set; }
        public CommomAccount() { }

        // NEW CONSTRUCTORS
        public CommomAccount(string firstName, string lastName, string email, ulong rg, DateTime birthdayDate, char gender, ulong cpf, double initialBalance)
            : base(firstName, lastName, email, rg, birthdayDate, gender)
        {
            InitialBalance = initialBalance;
            CPF = cpf;
        }

        public void Deposit(double amount)
        {
            InitialBalance += amount;
        }
        public void Withdraw(double amount)
        {
            InitialBalance -= amount;
        }

        // Entrando na conta...
        public void EnterCommomAccount()
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
                    Console.Write("Enter the amount you wish to deposit: "); double amount = double.Parse(Console.ReadLine());
                    Deposit(amount);
                    Console.WriteLine("New Balance: " + InitialBalance);
                    break;

                // withdraw
                case 2:
                    Console.Write("Enter the amount you wish to withdraw: "); double amount2 = double.Parse(Console.ReadLine());
                    Withdraw(amount2);
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