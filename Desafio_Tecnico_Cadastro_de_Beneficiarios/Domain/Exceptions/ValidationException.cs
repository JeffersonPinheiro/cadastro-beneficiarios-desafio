namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Exceptions
{
    public class ValidationException : DomainException
    {
        public ValidationException(string message) : base(message) { }
    }
}
