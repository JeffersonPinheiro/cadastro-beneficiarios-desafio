namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Entities
{
    public class Planos
    {
        public int Id { get; set; }
        public string Codigo_registro_ans { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public ICollection<Beneficiario> Beneficiarios { get; set; } = new List<Beneficiario>();
    }
}
