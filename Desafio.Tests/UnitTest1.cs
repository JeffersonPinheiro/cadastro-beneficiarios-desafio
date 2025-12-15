using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Application.Dto.Beneficiario;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Application.Services;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Infrastructure.Persistence;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Enum;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Entities;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Interfaces;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Infrastructure.UnitOfWork;
using Moq;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Infrastructure.Repositories;


namespace Desafio.Tests
{
    public class BeneficiarioTests : IDisposable 
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BeneficiarioTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
            _context = new AppDbContext(options);

            var beneficiarioRepository = new BeneficiariosRepository(_context);
            var planosRepository = new PlanosRepository(_context);
            var logExclusao = new LogExclusaoBeneficiarioRepository(_context);

            _unitOfWork = new UnitOfWork(
                _context,
                beneficiarioRepository,
                planosRepository,
                logExclusao
            );

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BeneficiarioEdicaoDto, Beneficiario>();
                cfg.CreateMap<Beneficiario, BeneficiarioDto>();
            });

            _mapper = config.CreateMapper();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        private BeneficiarioService CriarService() => new BeneficiarioService(_unitOfWork, _mapper);

        private async Task<BeneficiarioService> CriarServiceComBeneficiarioExistente(string cpf)
        {
            _context.Beneficiarios.Add(new Beneficiario
            {
                NomeCompleto = "Beneficiario Existente",
                Cpf = cpf,
                PlanoId = 1,
                DataNascimento = new DateTime(2000, 1, 1),
                Status = (int)Status.ATIVO
            });
            await _context.SaveChangesAsync();

            return CriarService();
        }

        private async Task<BeneficiarioService> CriarServiceComListaDeBeneficiarios()
        {
            _context.Beneficiarios.AddRange(new List<Beneficiario>
    {
            new Beneficiario { NomeCompleto = "Lucas de Jesus Marinho", Cpf = "11111111111", PlanoId = 1, Status = (int)Status.ATIVO, DataNascimento= new DateTime(2000, 1, 1)},
            new Beneficiario { NomeCompleto = "Ana Alice de Jesus da Silva", Cpf = "22222222222", PlanoId = 2, Status = (int)Status.INATIVO, DataNascimento= new DateTime(2000, 2, 2) },
            new Beneficiario { NomeCompleto = "Maria de Jesus da Silva", Cpf = "33333333333", PlanoId = 1, Status = (int)Status.ATIVO, DataNascimento= new DateTime (2000, 3, 3) }
            });
            await _context.SaveChangesAsync();

            return CriarService();
        }

        private async Task<BeneficiarioService> CriarServiceComBeneficiarioAtivo()
        {
            _context.Beneficiarios.Add(new Beneficiario
            {
                NomeCompleto = "Lucas Ativo",
                Cpf = "55555555555",
                PlanoId = 1,
                DataNascimento = new DateTime(2000, 1, 1),
                Status = (int)Status.ATIVO
            });
            await _context.SaveChangesAsync();

            return CriarService();
        }

        [Fact]
        public async Task AtualizarStatus_Beneficiario_DeveAlterarParaInativo()
        {
            var service = await CriarServiceComBeneficiarioAtivo();
            var beneficiario = _context.Beneficiarios.First();

            var dtoEdicao = new BeneficiarioEdicaoDto
            {
                Id = beneficiario.Id,
                NomeCompleto = beneficiario.NomeCompleto,
                Cpf = beneficiario.Cpf,
                DataNascimento = beneficiario.DataNascimento,
                Status = Status.INATIVO
            };

            var result = await service.EditarBeneficiarios(dtoEdicao);

            result.Status.Should().BeTrue();
            result.Dados.Status.Should().Be((int)Status.INATIVO);
        }

        [Fact]
        public async Task ListarBeneficiarios_ComFiltros_DeveRetornarSomenteCorretos()
        {
            var service = await CriarServiceComListaDeBeneficiarios();

            var todos = (await service.ListarBeneficiarios()).Dados;

            var filtrados = todos.Where(b => b.Status == (int)Status.ATIVO && b.PlanoId == 1).ToList();
            filtrados.Should().OnlyContain(b => b.Status == (int)Status.ATIVO && b.PlanoId == 1);
        }
    }
}
