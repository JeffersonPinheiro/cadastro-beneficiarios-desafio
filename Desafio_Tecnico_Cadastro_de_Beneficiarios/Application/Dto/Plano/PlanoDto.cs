using Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Entities;

namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Application.Dto.Plano
{
    public class PlanoDto
    {
        public int Id { get; set; }
        public string Codigo_registro_ans { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public ICollection<Domain.Entities.Beneficiario> Beneficiarios { get; set; } = new List<Domain.Entities.Beneficiario>();

        public static implicit operator PlanoDto?(Planos? v)
        {
            throw new NotImplementedException();
        }
    }
}
