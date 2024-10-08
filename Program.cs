﻿using PhoenixBank.Entities.Accounts;
using PhoenixBank.Services.Interfaces;
using System.Globalization;

// Por enquanto, por não ter um banco de dados, o banco sempre vai entender que, ao ser aberto, vai criar uma nova conta
namespace PhoenixBank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Instanciando os valores iniciais

            VerificationAccount vAcc = new VerificationAccount(); // instanciando vazio p/ tratar possíveis erros na validação

            Console.WriteLine("Welcome to Phoenix Bank! Let's get started?");
            try
            {
                Console.Write("First Name: "); string firstName = Console.ReadLine();
                Console.Write("Last Name: "); string lastName = Console.ReadLine();
                vAcc.ValidateNames(firstName, lastName); // verificação do nome/sobrenome

                Console.Write("Gender (M or F): "); char gender = char.Parse(Console.ReadLine().ToUpper());
                vAcc.ValidateGender(gender); // diferente de M ou F

                Console.Write("Email: "); string email = Console.ReadLine();
                vAcc.ValidateEmail(email); // caso o domínio seja outro além da lista presente no método

                Console.Write("RG (just numbers): "); string rgInput = Console.ReadLine();
                vAcc.ValidateRG(rgInput); // caso o RG digitado contenha letras e/ou caracteres especiais (tudo que for diferente de números)
                ulong rg = ulong.Parse(rgInput); // após a verificãção, o RG digitado sairá de string e passará a ser do tipo ulong

                Console.Write("Birthday Date (dd/mm/yyyy): "); DateTime birthdayDate = DateTime.Parse(Console.ReadLine());
                int age = vAcc.ValidateBirthdayDate(birthdayDate);

                VerificationAccount acc = new VerificationAccount(firstName, lastName, email, rg, birthdayDate, gender);

                Console.WriteLine("Wait...");
                Thread.Sleep(2300);

                // Interface Principal (Menu)
                Console.Clear();
                Console.WriteLine("Name: " + acc.FirstName + " " + acc.LastName);
                Console.WriteLine("Email: " + acc.Email);
                Console.WriteLine();
                if (age < 18)
                {
                    // a única opção possível é uma conta p/ jovens (menores de 18 anos)
                    Console.Clear();
                    Console.Write("Mother Name: "); string motherName = Console.ReadLine();
                    Console.Write("Mother RG (just numbers): "); ulong motherRG = ulong.Parse(Console.ReadLine());
                    Console.Write("Initial Balance: "); double tInitialBalance = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                    TeenageAccount teenageAcc = new TeenageAccount(firstName, lastName, email, rg, birthdayDate, gender, motherName, motherRG, tInitialBalance);

                    Console.WriteLine("Wait...");
                    Thread.Sleep(2300);

                    teenageAcc.EnterTeenageAccount();
                }
                Console.WriteLine("What type of account would you like to open?");
                Console.WriteLine("1 - Commom Account");
                Console.WriteLine("2 - Enterprise Account");
                Console.WriteLine("3 - Entrepreneur Account");
                int op = int.Parse(Console.ReadLine());

                switch (op)
                {
                    case 1: // commom account
                        Console.Clear();
                        Console.Write("CPF (just numbers): "); ulong cpf = ulong.Parse(Console.ReadLine());
                        Console.Write("Initial Balance: "); double initialBalance = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                        CommomAccount commomAcc = new CommomAccount(firstName, lastName, email, rg, birthdayDate, gender, cpf, initialBalance);

                        Console.WriteLine("Wait...");
                        Thread.Sleep(2300);

                        commomAcc.EnterCommomAccount();
                        break;

                    case 2: // enterprise account
                        Console.Clear();
                        Console.Write("CNPJ (just numbers): "); ulong cnpj = ulong.Parse(Console.ReadLine());
                        Console.Write("Initial Balance: "); double eInitialBalance = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                        Console.Write("Do you like a credit? (1 - yes; 2 - no)"); int op2 = int.Parse(Console.ReadLine());

                        switch (op2)
                        {
                            case 1: // yes credit
                                Console.Write("Credit: "); double credit = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                                EnterpriseAccount enterpriseAcc = new EnterpriseAccount(firstName, lastName, email, rg, birthdayDate, gender, cnpj, eInitialBalance, 100000.00, credit);
                                
                                Console.WriteLine("Wait...");
                                Thread.Sleep(2300);
                                
                                enterpriseAcc.EnterEnterpriseAccount();
                                break;
                            
                            case 2: // no credit
                                EnterpriseAccount enterpriseAcc2 = new EnterpriseAccount(firstName, lastName, email, rg, birthdayDate, gender, cnpj, eInitialBalance, 100000.00, 0.0);

                                Console.WriteLine("Wait...");
                                Thread.Sleep(2300);

                                enterpriseAcc2.EnterEnterpriseAccount();
                                break;

                            default:
                                Console.WriteLine("Error! Invalid Number.");
                                break;
                        }
                        break;

                    case 3:
                        Console.Clear();
                        Console.Write("Company Name: "); string eName = Console.ReadLine();
                        Console.Write("CPF or CNPJ? (1 - CPF; 2 - CNPJ): "); int op3 = int.Parse(Console.ReadLine());
                        switch (op3)
                        {
                            case 1: // cpf
                                Console.Write("CPF (just numbers): "); ulong eCpf = ulong.Parse(Console.ReadLine());
                                Console.Write("Company Type: "); string ct1 = Console.ReadLine();
                                Console.Write("Initial Balance: "); double ib = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                                Console.Write("Do you like a credit? (1 - yes; 2 - no)"); int op4 = int.Parse(Console.ReadLine());

                                switch (op4) // yes credit
                                {
                                    case 1:
                                        Console.Write("Credit: "); double cr = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                                        EntrepreneursAccount entrepreneursCpfAcc = new EntrepreneursAccount(firstName, lastName, email, rg, birthdayDate, gender, eName, ct1, new IPersonIndividual(), ib, 250000.00, cr);

                                        Console.WriteLine("Wait...");
                                        Thread.Sleep(2300);

                                        entrepreneursCpfAcc.EnterEntrepreneursAccount();
                                        break;

                                    case 2: // no credit
                                        EntrepreneursAccount entrepreneursCpfAcc2 = new EntrepreneursAccount(firstName, lastName, email, rg, birthdayDate, gender, eName, ct1, new IPersonIndividual(), ib, 250000.00, 0.0);

                                        Console.WriteLine("Wait...");
                                        Thread.Sleep(2300);

                                        entrepreneursCpfAcc2.EnterEntrepreneursAccount();
                                        break;

                                    default:
                                        Console.WriteLine("Error! Invalid Number.");
                                        break;
                                }
                                break;

                            case 2: // cnpj
                                Console.Write("CNPJ (just numbers): "); ulong eCnpj = ulong.Parse(Console.ReadLine());
                                Console.Write("Company Type: "); string ct2 = Console.ReadLine();
                                Console.Write("Initial Balance: "); double ib2 = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                                Console.Write("Do you like a credit? (1 - yes; 2 - no)"); int op5 = int.Parse(Console.ReadLine());

                                switch (op5) // yes credit
                                {
                                    case 1:
                                        Console.Write("Credit: "); double cr2 = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                                        EntrepreneursAccount entrepreneursCnpjAcc = new EntrepreneursAccount(firstName, lastName, email, rg, birthdayDate, gender, eName, ct2, new IPersonIndividual(), ib2, 250000.00, cr2);

                                        entrepreneursCnpjAcc.EnterEntrepreneursAccount();
                                        break;

                                    case 2: // no credit
                                        EntrepreneursAccount entrepreneursCnpjAcc2 = new EntrepreneursAccount(firstName, lastName, email, rg, birthdayDate, gender, eName, ct2, new IPersonIndividual(), ib2, 250000.00, 0.0);

                                        entrepreneursCnpjAcc2.EnterEntrepreneursAccount();
                                        break;

                                    default:
                                        Console.WriteLine("Error! Invalid Number.");
                                        break;
                                }
                                break;
                        }
                        break;
                }
            }
            catch (FormatException ex) // caso, onde se pede números, o usuário escreva letras, palavras ou frases.
            {
                Console.WriteLine("Error! Type numbers, not letters.");
            }
            catch (Exception ex) // outros erros (lembrando que os principais já estão sendo tratados em suas respectivas classes)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}