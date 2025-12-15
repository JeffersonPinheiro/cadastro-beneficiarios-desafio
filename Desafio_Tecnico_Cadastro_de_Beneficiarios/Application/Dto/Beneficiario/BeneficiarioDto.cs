using Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Entities;

namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Application.Dto.Beneficiario
{
    public class BeneficiarioDto
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
        public int Status { get; set; }
        public int PlanoId { get; set; }
        public Planos Plano { get; set; } = null!;
    }
}
