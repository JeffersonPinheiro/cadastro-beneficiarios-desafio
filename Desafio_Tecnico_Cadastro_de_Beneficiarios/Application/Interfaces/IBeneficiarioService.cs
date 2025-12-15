using Desafio_Tecnico_Cadastro_de_Beneficiarios.Application.Dto;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Application.Dto.Beneficiario;
using Result = Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Common.Result;

namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Application.Interfaces
{
    public interface IBeneficiarioService
    {
        Task<ResponseDto<List<BeneficiarioDto>>> ListarBeneficiarios();
        Task<ResponseDto<BeneficiarioDto>> BuscarBeneficiariosPorId(int id);
        Task<ResponseDto<BeneficiarioDto>> EditarBeneficiarios(BeneficiarioEdicaoDto beneficiarioEdicaoDto);
        Task<Result> SolicitarExclusaoAsync(int id, int prioridade);
        Task<ResponseDto<BeneficiarioDto>> CriarBeneficiario(BeneficiarioDto dto);
    }
}
