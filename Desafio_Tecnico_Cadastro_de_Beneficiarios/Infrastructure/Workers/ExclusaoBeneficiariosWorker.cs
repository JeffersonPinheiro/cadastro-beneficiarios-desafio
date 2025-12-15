using Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Entities;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Interfaces;

namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Infrastructure.Workers
{
    public class ExclusaoBeneficiariosWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<ExclusaoBeneficiariosWorker> _logger;

        public ExclusaoBeneficiariosWorker(
            IServiceScopeFactory scopeFactory,
            ILogger<ExclusaoBeneficiariosWorker> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker de exclusão de beneficiários iniciado.");

            while (!stoppingToken.IsCancellationRequested)
            {
                await ProcessarExclusoes(stoppingToken);
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private async Task ProcessarExclusoes(CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();

            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var beneficiariosPendentes =
                await unitOfWork.Beneficiarios.ObterPendentesExclusaoAsync();

            if (!beneficiariosPendentes.Any())
                return;

            var ordenados = beneficiariosPendentes
                .OrderBy(b => b.PrioridadeExclusao)
                .ThenBy(b => b.DataSolicitacaoExclusao)
                .ToList();

            foreach (var beneficiario in ordenados)
            {
                _logger.LogInformation(
                    "Processando exclusão do beneficiário {Id} - Prioridade {Prioridade}",
                    beneficiario.Id,
                    beneficiario.PrioridadeExclusao
                );

                var log = new LogExclusaoBeneficiario
                {
                    BeneficiarioId = beneficiario.Id,
                    NomeBeneficiario = beneficiario.NomeCompleto,
                    Prioridade = beneficiario.PrioridadeExclusao!.Value,
                    DataSolicitacao = beneficiario.DataSolicitacaoExclusao!.Value,
                    DataExclusao = DateTime.UtcNow
                };

                await unitOfWork.LogsExclusao.AdicionarAsync(log);

                unitOfWork.Beneficiarios.Remover(beneficiario);
            }

            await unitOfWork.CommitAsync();

            _logger.LogInformation(
                "Exclusão concluída para {Total} beneficiários.",
                ordenados.Count
            );
        }
    }

}
