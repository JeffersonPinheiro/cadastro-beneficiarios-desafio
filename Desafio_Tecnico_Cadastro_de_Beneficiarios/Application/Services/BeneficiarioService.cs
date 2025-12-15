using AutoMapper;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Application.Dto;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Application.Dto.Beneficiario;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Application.Interfaces;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Entities;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Enum;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Exceptions;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Interfaces;
using Result = Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Common.Result;

namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Application.Services
{
    public class BeneficiarioService : IBeneficiarioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BeneficiarioService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDto<BeneficiarioDto>> BuscarBeneficiariosPorId(int id)
        {
            var beneficiario = await _unitOfWork.Beneficiarios.ObterPorIdAsync(id);

            if (beneficiario == null)
                throw new Domain.Exceptions.NotFoundException("Beneficiário não localizado");

            return new ResponseDto<BeneficiarioDto>
            {
                Dados = _mapper.Map<BeneficiarioDto>(beneficiario),
                Mensagem = "Beneficiário localizado com sucesso"
            };
        }

        public async Task<ResponseDto<BeneficiarioDto>> CriarBeneficiario(BeneficiarioDto dto)
        {
            if (await _unitOfWork.Beneficiarios.CpfExisteAsync(dto.Cpf))
                throw new ValidationException("CPF já cadastrado");

            if (!await _unitOfWork.Planos.PlanoExisteAsync(dto.PlanoId))
                throw new Domain.Exceptions.NotFoundException("Plano não encontrado");

            var beneficiario = _mapper.Map<Beneficiario>(dto);
            beneficiario.Status = (int)Status.ATIVO;

            await _unitOfWork.Beneficiarios.CriarAsync(beneficiario);
            await _unitOfWork.CommitAsync();

            return new ResponseDto<BeneficiarioDto>
            {
                Dados = _mapper.Map<BeneficiarioDto>(beneficiario),
                Mensagem = "Beneficiário criado com sucesso"
            };
        }

        public async Task<Result> SolicitarExclusaoAsync(int id, int prioridade)
        {
            if (prioridade < 1 || prioridade > 3)
                return Result.Fail("ValidationError", "Prioridade inválida.");

            var beneficiario = await _unitOfWork.Beneficiarios.ObterPorIdAsync(id);

            if (beneficiario == null)
                return Result.Fail("NotFound", "Beneficiário não encontrado.");

            if (beneficiario.PendenteExclusao)
                return Result.Fail("ValidationError", "Beneficiário já possui exclusão pendente.");

            beneficiario.PendenteExclusao = true;
            beneficiario.PrioridadeExclusao = prioridade;
            beneficiario.DataSolicitacaoExclusao = DateTime.UtcNow;

            _unitOfWork.Beneficiarios.Atualizar(beneficiario);
            await _unitOfWork.CommitAsync();

            return Result.Ok("Solicitação de exclusão registrada com sucesso.");
        }

        public async Task<ResponseDto<BeneficiarioDto>> EditarBeneficiarios(BeneficiarioEdicaoDto dto)
        {
            var beneficiarioBanco = await _unitOfWork.Beneficiarios.ObterPorIdAsync(dto.Id);

            if (beneficiarioBanco == null)
                throw new Domain.Exceptions.NotFoundException("Beneficiário não localizado");

            beneficiarioBanco.NomeCompleto = dto.NomeCompleto;
            beneficiarioBanco.Cpf = dto.Cpf;
            beneficiarioBanco.DataNascimento = dto.DataNascimento;
            beneficiarioBanco.Status = (int)dto.Status;

            _unitOfWork.Beneficiarios.Atualizar(beneficiarioBanco);
            await _unitOfWork.CommitAsync();

            return new ResponseDto<BeneficiarioDto>
            {
                Dados = _mapper.Map<BeneficiarioDto>(beneficiarioBanco),
                Mensagem = "Beneficiário editado com sucesso"
            };
        }

        public async Task<ResponseDto<List<BeneficiarioDto>>> ListarBeneficiarios()
        {
            var beneficiarios = await _unitOfWork.Beneficiarios.ObterTodosAsync();

            return new ResponseDto<List<BeneficiarioDto>>
            {
                Dados = _mapper.Map<List<BeneficiarioDto>>(beneficiarios),
                Mensagem = "Beneficiários listados com sucesso"
            };
        }
    }
}
