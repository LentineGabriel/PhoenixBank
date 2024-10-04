namespace PhoenixBank.Entities.Exceptions
{
    internal class EmailException : ApplicationException
    {
        public EmailException(string message) : base(message) { }
    }
}
