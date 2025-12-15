namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Exceptions
{
    public class NotFoundException :DomainException
    {
        public NotFoundException(string message) : base(message) { }
    }
}
