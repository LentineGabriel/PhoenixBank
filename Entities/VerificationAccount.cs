using PhoenixBank.Entities.Exceptions;
using System.Text.RegularExpressions;

namespace PhoenixBank.Entities
{
    internal class VerificationAccount
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ulong RG { get; set; }
        public DateTime BirthdayDate { get; set; }
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
                throw new DomainException("The field must be filled.");

            // Caso os nomes sejam preenchidos com números ou caracteres especiais
            if (!Regex.IsMatch(firstName, @"^[a-zA-Z\s]+$")) throw new DomainException("Invalid Name! Only letters and spaces are allowed.");
            if (!Regex.IsMatch(lastName, @"^[a-zA-Z\s]+$")) throw new DomainException("Invalid Name! Only letters and spaces are allowed.");
        }

        // tratando o gênero
        public void ValidateGender(char gender)
        {
            // Se o gênero não for 'M' ou 'F'
            if (gender != 'M' && gender != 'F')
                throw new DomainException("Invalid Gender! Only 'M' or 'F' are accepted.");
        }

        // tratando o email
        public void ValidateEmail(string email)
        {
            // uma lista com alguns domínios válidos, qualquer domínio fora desta lista lançará um erro
            List<string> domains = new List<string> { "gmail.com", "yahoo.com", "hotmail.com", "outlook.com" };
            bool validDomain = false;

            foreach (string d in domains)
            {
                if(email.EndsWith("@" + d))
                {
                    validDomain = true;
                    break;
                }
            }
            if(validDomain) Console.WriteLine("O e-mail pertence a um domínio válido. Prossiga!");
            else
            {
                throw new Exception("O e-mail não pertence a um domínio válido.");
                System.Environment.Exit(0);
            }
        }

        // tratando o RG
        public void ValidateRG(string rg)
        {
            // Vai verificar se o campo RG está vazio ou nulo; ou se há algum caractere diferente de números na hora de passar p/ ulong
            if (string.IsNullOrEmpty(rg) || !ulong.TryParse(rg, out _)) throw new Exception("Invalid RG! Please enter only numbers.");
        }

        // tratando a data de aniversário
        public int ValidateBirthdayDate(DateTime birthdayDate)
        {
            int age;
            DateTime today = DateTime.Today;

            age = today.Year - birthdayDate.Year;
            return age;
        }
    }
}