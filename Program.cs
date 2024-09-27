using PhoenixBank.Entities;
using PhoenixBank.Entities.Exceptions;

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
                VerificationAccount acc = new VerificationAccount(); // instanciando vazio p/ tratar possíveis erros na validação
                
                Console.WriteLine("Welcome to Phoenix Bank! Let's get started?");
                Console.Write("First Name: "); string firstName = Console.ReadLine();
                Console.Write("Last Name: "); string lastName = Console.ReadLine();
                acc.ValidateNames(firstName, lastName); // verificação do nome/sobrenome
                Console.Write("Gender (M or F): "); char gender = char.Parse(Console.ReadLine().ToUpper());
                acc.ValidateGender(gender); // diferente de M ou F
                Console.Write("Email: "); string email = Console.ReadLine();
                Console.Write("RG (just numbers): "); ulong rg = ulong.Parse(Console.ReadLine());
                Console.Write("Birthday Date (dd/mm/yyyy): "); DateTime birthdayDate = DateTime.Parse(Console.ReadLine());

                VerificationAccount acc1 = new VerificationAccount(firstName, lastName, email, rg, birthdayDate, gender);

                Console.WriteLine("Wait...");
                Thread.Sleep(2300);

                // Interface Principal (Menu)
                Console.Clear();
                Console.WriteLine("Name: " + acc.FirstName + " " + acc.LastName);
                Console.WriteLine("Email: " + acc.Email);
                Console.WriteLine();
                Console.WriteLine("What type of account would you like to open?");
                Console.WriteLine("1 - Commom Account");
                Console.WriteLine("2 - Enterprise Account");
                Console.WriteLine("3 - Teenage Account");
                int op = int.Parse(Console.ReadLine());

                switch (op)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("CPF (just numbers): "); ulong cpf = ulong.Parse(Console.ReadLine());
                        Console.WriteLine("Initial Balance: "); double commomInitialBalance = double.Parse(Console.ReadLine());
                        CommomAccount commomAcc = new CommomAccount(firstName, lastName, email, rg, birthdayDate, gender, cpf, commomInitialBalance);
                        Console.WriteLine("Wait...");
                        Thread.Sleep(2300);

                        commomAcc.EnterCommomAccount();
                        break;

                    case 2:
                        Console.Clear();
                        Console.WriteLine("CNPJ (just numbers): "); ulong cnpj = ulong.Parse(Console.ReadLine());
                        Console.WriteLine("Initial Balance: "); double enterpriseInitialBalance = double.Parse(Console.ReadLine());
                        Console.WriteLine("Credit: "); double credit = double.Parse(Console.ReadLine());
                        EnterpriseAccount enterpriseAcc = new EnterpriseAccount(firstName, lastName, email, rg, birthdayDate, gender, cnpj, enterpriseInitialBalance, 100000.00, credit);
                        Console.WriteLine("Wait...");
                        Thread.Sleep(2300);

                        enterpriseAcc.EnterEnterpriseAccount();
                        break;

                    case 3:
                        Console.Clear();
                        Console.WriteLine("Mother Name: "); string motherName = Console.ReadLine();
                        Console.WriteLine("Mother RG: "); ulong motherRG = ulong.Parse(Console.ReadLine());
                        Console.WriteLine("Initial Balance: "); double initialBalance = double.Parse(Console.ReadLine());
                        TeenageAccount teenageAcc = new TeenageAccount(firstName, lastName, email, rg, birthdayDate, gender, motherName, motherRG, initialBalance);
                        Console.WriteLine("Wait...");
                        Thread.Sleep(2300);

                        teenageAcc.EnterTeenageAccount();
                        break;
                }
            }
            catch (FormatException ex) // caso, onde se pede números, o usuário escreva letras, palavras ou frases.
            {
                Console.WriteLine("Error! Type numbers, not letters.");
            }
            catch (DomainException ex) // erros no nome
            {
                Console.WriteLine("Error in names: " + ex.Message);
            }
            catch (Exception ex) // erros no gênero
            {
                Console.WriteLine("Error in gender: " + ex.Message);
            }
        }
    }
}