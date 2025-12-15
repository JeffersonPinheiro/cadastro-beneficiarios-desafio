namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Entities
{
    public class LogExclusaoBeneficiario
    {
        public int Id { get; set; }

        public int BeneficiarioId { get; set; }

        public string NomeBeneficiario { get; set; } = string.Empty;

        public int Prioridade { get; set; }

        public DateTime DataSolicitacao { get; set; }

        public DateTime DataExclusao { get; set; }

        public string ProcessadoPor { get; set; } = "Worker";
    }
}
