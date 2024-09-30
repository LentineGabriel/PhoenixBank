using PhoenixBank.Entities;
using PhoenixBank.Entities.Exceptions;
using System.Globalization;

// Por enquanto, por não ter um banco de dados, o banco sempre vai entender que, ao ser aberto, vai criar uma nova conta
namespace PhoenixBank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Instanciando os valores iniciais
            try
            {
                VerificationAccount vAcc = new VerificationAccount(); // instanciando vazio p/ tratar possíveis erros na validação

                Console.WriteLine("Welcome to Phoenix Bank! Let's get started?");

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
                Console.WriteLine($"Você tem {age} anos.");

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
                    Console.Write("Mother RG: "); ulong motherRG = ulong.Parse(Console.ReadLine());
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
                    case 1:
                        Console.Clear();
                        Console.Write("CPF (just numbers): "); ulong cpf = ulong.Parse(Console.ReadLine());
                        Console.Write("Initial Balance: "); double initialBalance = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                        CommomAccount commomAcc = new CommomAccount(firstName, lastName, email, rg, birthdayDate, gender, cpf, initialBalance);
                        
                        Console.WriteLine("Wait...");
                        Thread.Sleep(2300);

                        commomAcc.EnterCommomAccount();
                        break;

                    case 2:
                        Console.Clear();
                        Console.WriteLine("CNPJ (just numbers): "); ulong cnpj = ulong.Parse(Console.ReadLine());
                        Console.WriteLine("Initial Balance: "); double eInitialBalance = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                        Console.WriteLine("Credit: "); double credit = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                        EnterpriseAccount enterpriseAcc = new EnterpriseAccount(firstName, lastName, email, rg, birthdayDate, gender, cnpj, eInitialBalance, 100000.00, credit);
                        
                        Console.WriteLine("Wait...");
                        Thread.Sleep(2300);

                        enterpriseAcc.EnterEnterpriseAccount();
                        break;

                    case 3:
                        break;
                }
            }
            catch (FormatException ex) // caso, onde se pede números, o usuário escreva letras, palavras ou frases.
            {
                Console.WriteLine("Error! Type numbers, not letters.");
            }
            catch (Exception ex) // outros erros
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}