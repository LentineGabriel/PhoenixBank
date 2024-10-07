﻿using PhoenixBank.Entities.Exceptions;
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
            Console.WriteLine("Account Type: Teenage Account");
            Console.WriteLine("Initial Balance: " + InitialBalance);
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1 - Deposit");
            Console.WriteLine("2 - Withdraw");
            Console.WriteLine("3 - Savings");
            Console.WriteLine("4 - Exit");
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
                    Savings();
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
        public void Savings()
        {
            Console.Clear();
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1 - Deposit"); // por enquanto, só vai dar p/ depositar
            Console.WriteLine("2 - Exit");
            int op = int.Parse(Console.ReadLine());

            switch (op)
            {
                case 1:
                    SavingsAccount sAccD = new SavingsAccount();

                    Console.Write("Enter the amount you wish to deposit: "); double sAmount = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                    double res = sAccD.SavingsDeposit(InitialBalance, sAmount);
                    Console.WriteLine("New Teenage Balance: " + (InitialBalance - res));
                    Console.WriteLine("Savings Balance: " + res);
                    break;

                case 2:
                    System.Environment.Exit(0);
                    break;
            }
        }
    }
}