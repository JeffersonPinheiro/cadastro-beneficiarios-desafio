using System.Numerics;

namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Entities
{
    public class Beneficiario
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
        public int Status { get; set; }
        public int PlanoId { get; set; }
        public Planos Plano { get; set; } = null!;

        public bool PendenteExclusao { get; set; }
        public int? PrioridadeExclusao { get; set; }
        public DateTime? DataSolicitacaoExclusao { get; set; }
    }
}
