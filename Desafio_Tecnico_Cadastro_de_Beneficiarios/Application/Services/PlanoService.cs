using System.Numerics;
using AutoMapper;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Application.Dto;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Application.Dto.Plano;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Application.Interfaces;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Entities;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Exceptions;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Interfaces;

namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Application.Services
{
    public class PlanoService : IPlanosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PlanoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDto<PlanoDto>> BuscarPlanoPorId(int id)
        {
            var plano = await _unitOfWork.Planos.ObterPorIdAsync(id);

            if (plano == null)
                throw new Domain.Exceptions.NotFoundException("Plano não localizado");

            return new ResponseDto<PlanoDto>
            {
                Dados = _mapper.Map<PlanoDto>(plano),
                Mensagem = "Plano localizado com sucesso"
            };
        }

        public async Task<ResponseDto<PlanoDto>> CriarPlano(PlanoDto dto)
        {
            if (await _unitOfWork.Planos.PlanoExisteAsync(dto.Id))
                throw new ValidationException("Já existe um plano com esse nome");

            var plano = _mapper.Map<Planos>(dto);

            _unitOfWork.Planos.CriarPlano(plano);
            await _unitOfWork.CommitAsync();

            return new ResponseDto<PlanoDto>
            {
                Dados = _mapper.Map<PlanoDto>(plano),
                Mensagem = "Plano criado com sucesso"
            };
        }

        public async Task<ResponseDto<PlanoDto>> EditarPlano(PlanoEdicaoDto dto)
        {
            var planoBanco = await _unitOfWork.Planos.ObterPorIdAsync(dto.Id);

            if (planoBanco == null)
                throw new Domain.Exceptions.NotFoundException("Plano não localizado");

            planoBanco.Nome = dto.Nome;
            planoBanco.Codigo_registro_ans = dto.Codigo_registro_ans;

            _unitOfWork.Planos.Atualizar(planoBanco);
            await _unitOfWork.CommitAsync();

            return new ResponseDto<PlanoDto>
            {
                Dados = _mapper.Map<PlanoDto>(planoBanco),
                Mensagem = "Plano atualizado com sucesso"
            };
        }

        public async Task<ResponseDto<PlanoDto>> DeletarPlano(int id)
        {
            var plano = await _unitOfWork.Planos.ObterPorIdAsync(id);

            if (plano == null)
                throw new Domain.Exceptions.NotFoundException("Plano não localizado");

            _unitOfWork.Planos.Remover(plano);
            await _unitOfWork.CommitAsync();

            return new ResponseDto<PlanoDto>
            {
                Dados = _mapper.Map<PlanoDto>(plano),
                Mensagem = "Plano removido com sucesso"
            };
        }
    }
}
