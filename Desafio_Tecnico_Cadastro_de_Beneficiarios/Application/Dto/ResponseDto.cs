namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Application.Dto
{
    public class ResponseDto<T>
    {
        public T Dados { get; set; }
        public string Error { get; set; }
        public string Mensagem { get; set; }
        public bool Status { get; set; } = true;
        public List<ValidacaoDto> Details { get; set; } = new List<ValidacaoDto>();
    }
}
