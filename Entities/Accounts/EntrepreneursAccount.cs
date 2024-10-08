﻿using PhoenixBank.Entities.Exceptions;
using PhoenixBank.Services.Interfaces;
using System.Globalization;

namespace PhoenixBank.Entities.Accounts
{
    internal class EntrepreneursAccount : VerificationAccount
    {
        public string CompanyName { get; set; }
        public string BusinessType { get; set; }
        public double InitialBalance { get; set; }
        public double WithdrawLimit { get; private set; }
        public double Credit { get; set; }

        private IPersonIndividual _pIndividual;

        public EntrepreneursAccount() { }

        public EntrepreneursAccount(string firstName, string lastName, string email, ulong rg, DateTime birthdayDate, char gender, string companyName, string businessType, IPersonIndividual pIndividual, double initialBalance, double withdrawLimit, double credit)
            : base(firstName, lastName, email, rg, birthdayDate, gender)
        {
            CompanyName = companyName;
            BusinessType = businessType;
            _pIndividual = pIndividual;
            Credit = credit;
            InitialBalance = initialBalance + credit;
            WithdrawLimit = 250000.00;
        }

        // caso a pessoa escolha o CPF como entrada
        public class Individual : EntrepreneursAccount, IPerson
        {
            public ulong CPF { get; set; }

            public Individual(string companyName, string businessType, ulong cpf)
            {
                CompanyName = companyName;
                BusinessType = businessType;
                CPF = _pIndividual.CPF;
            }
        }

        // caso a pessoa escolha o CNPJ como entrada
        public class Legal : EntrepreneursAccount, ILegal
        {
            public ulong CNPJ { get; set; }

            public Legal(string companyName, string businessType, ulong cnpj)
            {
                CompanyName = companyName;
                BusinessType = businessType;
                CNPJ = _pIndividual.CNPJ;
            }
        }

        public void Deposit(double amount)
        {
            if (amount <= 0.0) throw new DomainException("The amount requested must be greater than zero.");
            InitialBalance += amount;
        }
        public void Withdraw(double amount)
        {
            if (amount > WithdrawLimit) throw new DomainException("The amount to be withdrawn is greater than the withdraw limit.");
            if (amount > InitialBalance) throw new DomainException("The requested amount is greater than the current value in the account.");
            if (amount <= 0.0) throw new DomainException("The amount requested must be greater than zero.");
            InitialBalance -= amount * 0.03;
        }

        // Entrando na conta...
        public void EnterEntrepreneursAccount()
        {
            Console.Clear();
            Console.WriteLine("Name: " + FirstName + " " + LastName);
            Console.WriteLine("Account Type: Entrepreneur Account");
            Console.WriteLine("Initial Balance: " + InitialBalance);
            Console.WriteLine("Withdraw Limit: " + WithdrawLimit);

            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1 - Deposit");
            Console.WriteLine("2 - Withdraw");
            Console.WriteLine("3 - Savings Account");
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
                    Console.WriteLine("New Entrepreneurs Balance: " + (InitialBalance - res));
                    Console.WriteLine("Savings Balance: " + res);
                    break;

                case 2:
                    System.Environment.Exit(0);
                    break;
            }
        }
    }
}