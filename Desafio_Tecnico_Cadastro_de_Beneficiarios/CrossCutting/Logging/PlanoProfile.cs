using AutoMapper;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Application.Dto.Plano;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Entities;

namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.CrossCutting.Logging
{
    public class PlanoProfile : Profile
    {
        public PlanoProfile()
        {
            CreateMap<PlanoCriacaoDto, Planos>();
        }
    }
}
