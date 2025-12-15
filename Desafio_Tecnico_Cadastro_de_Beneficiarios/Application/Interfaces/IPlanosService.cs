using Desafio_Tecnico_Cadastro_de_Beneficiarios.Application.Dto;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Application.Dto.Plano;

namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Application.Interfaces
{
    public interface IPlanosService
    {
        Task<ResponseDto<PlanoDto>> BuscarPlanoPorId(int id);
        Task<ResponseDto<PlanoDto>> CriarPlano(PlanoDto planoCriacaoDto);
        Task<ResponseDto<PlanoDto>> EditarPlano(PlanoEdicaoDto planoEdicaoDto);
        Task<ResponseDto<PlanoDto>> DeletarPlano(int id);
    }
}
