namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Common
{
    public class Result
    {
        public bool Status { get; }
        public string Error { get; }
        public string Message { get; }

        private Result(bool status, string error, string message)
        {
            Status = status;
            Error = error;
            Message = message;
        }

        public static Result Ok(string message)
            => new(true, null, message);

        public static Result Fail(string error, string message)
            => new(false, error, message);
    }
}
