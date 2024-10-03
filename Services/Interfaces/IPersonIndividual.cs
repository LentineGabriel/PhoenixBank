namespace PhoenixBank.Services.Interfaces
{
    internal class IPersonIndividual : IPerson, ILegal
    {
        public ulong CPF { get; set; }
        public ulong CNPJ { get; set; }
    }
}
