using PhoenixBank.Entities.Exceptions;
using System.Text.RegularExpressions;

namespace PhoenixBank.Entities.Accounts
{
    internal class VerificationAccount
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ulong RG { get; private set; } // não pode ser alterado
        public DateTime BirthdayDate { get; private set; } // não pode ser alterado
        public char Gender { get; set; }

        public VerificationAccount() { }
        public VerificationAccount(string firstName, string lastName, string email, ulong rg, DateTime birthdayDate, char gender)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            RG = rg;
            BirthdayDate = birthdayDate;
            Gender = gender;
        }
        // tratando os nomes
        public void ValidateNames(string firstName, string lastName)
        {
            // Caso os campos estejam vazios
            if (firstName == null || lastName == null)
                throw new NameException("The field must be filled.");

            // Caso os nomes sejam preenchidos com números ou caracteres especiais
            if (!Regex.IsMatch(firstName, @"^[a-zA-Z\s]+$")) throw new NameException("Invalid Name! Only letters and spaces are allowed.");
            if (!Regex.IsMatch(lastName, @"^[a-zA-Z\s]+$")) throw new NameException("Invalid Name! Only letters and spaces are allowed.");
        }

        // tratando o gênero
        public void ValidateGender(char gender)
        {
            if (char.IsWhiteSpace(gender)) Console.WriteLine("Gender cannot be empty.");

            // Se o gênero não for 'M' ou 'F'
            if (gender != 'M' && gender != 'F')
                throw new GenderException("Invalid Gender! Only 'M' or 'F' are accepted.");
        }

        // tratando o email
        public void ValidateEmail(string email)
        {
            // email não pode ser vazio ou conter espaços em branco
            if (string.IsNullOrWhiteSpace(email)) throw new EmailException("Email cannot be empty or with white spaces.");

            // email precisa ter o sufixo @
            if (!email.Contains("@")) throw new EmailException("Invalid email! It must contain '@'");

            // email precisa terminar com '.com'
            if (!email.EndsWith(".com")) throw new EmailException("Invalid email! It must end with '.com'");

            // uma lista com alguns domínios válidos, qualquer domínio fora desta lista lançará um erro
            List<string> domains = new List<string> { "gmail.com", "yahoo.com", "hotmail.com", "outlook.com" };
            bool validDomain = false;

            foreach (string d in domains)
            {
                if (email.EndsWith("@" + d))
                {
                    validDomain = true;
                    break;
                }
            }
            if (validDomain) Console.WriteLine("O e-mail é válido. Prossiga!");
            else
            {
                throw new EmailException("O e-mail não pertence a um domínio válido.");
                Environment.Exit(0);
            }
        }

        // tratando o RG
        public void ValidateRG(string rg)
        {
            // Vai verificar se o campo RG está vazio ou nulo; ou se há algum caractere diferente de números na hora de passar p/ ulong
            if (string.IsNullOrEmpty(rg) || !ulong.TryParse(rg, out _)) throw new RGException("Invalid RG! Please enter only numbers.");
        }

        // tratando a data de aniversário
        public int ValidateBirthdayDate(DateTime birthdayDate)
        {
            int age;
            DateTime today = DateTime.Today;
            
            // caso o ano de nascimento (inserido pelo usuário) seja maior que o ano atual (de acordo com a aplicação rodando)
            if(birthdayDate.Year > today.Year) throw new BirthdayException("The year of your birth is greater than the current year");
            else
            {
                age = today.Year - birthdayDate.Year;
            }
            return age;
        }
    }
}