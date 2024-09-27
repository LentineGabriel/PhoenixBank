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
        public void ValidateNames(string firstName, string lastName)
        {
            // Caso os campos estejam vazios
            if (string.IsNullOrEmpty(firstName)) throw new DomainException("The field must be filled.");
            if (string.IsNullOrEmpty(lastName)) throw new DomainException("The field must be filled.");
            if (firstName == null || lastName == null) throw new DomainException("The field must be filled.");

            // Caso os campos sejam preenchidos com números ou caracteres especiais
            if (!Regex.IsMatch(firstName, @"^[a-zA-Z\s]+$")) throw new DomainException("Invalid Name! Only letters and spaces are allowed.");
            if (!Regex.IsMatch(lastName, @"^[a-zA-Z\s]+$")) throw new DomainException("Invalid Name! Only letters and spaces are allowed.");
        }
        public void ValidateGender(char gender)
        {
            // Se o gênero não for 'M' ou 'F'
            if (gender != 'M' && gender != 'F') throw new DomainException("Invalid Gender! Only 'M' or 'F' are accepted.");
        }
    }
}
