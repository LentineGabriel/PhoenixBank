using PhoenixBank.Entities.Exceptions;

namespace PhoenixBank.Entities
{
    internal class VerificationAccount
    {
        // PROPERTIES
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ulong RG { get; set; }
        public DateTime BirthdayDate { get; set; }
        public char Gender { get; set; }

        // CONSTRUCTORS
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
    }
}
