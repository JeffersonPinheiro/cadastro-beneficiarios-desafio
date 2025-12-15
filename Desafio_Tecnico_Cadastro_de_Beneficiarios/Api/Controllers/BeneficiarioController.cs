using Desafio_Tecnico_Cadastro_de_Beneficiarios.Application.Dto.Beneficiario;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Sprache;

namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiarioController : ControllerBase
    {
        private readonly IBeneficiarioService _beneficiarioService;

        public BeneficiarioController(IBeneficiarioService beneficiarioService)
        {
            _beneficiarioService = beneficiarioService;
        }

        /// <summary>
        /// Lista todos os beneficiários
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarBeneficiarios()
        {
            var beneficiario = await _beneficiarioService.ListarBeneficiarios();

            if (!beneficiario.Status)
                return BadRequest(beneficiario);

            return Ok(beneficiario);
        }

        /// <summary>
        /// Retorna um beneficiário pelo ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Detalhe(int id)
        {
            var beneficiario = await _beneficiarioService.BuscarBeneficiariosPorId(id);

            if (!beneficiario.Status)
                return NotFound(beneficiario);

            return Ok(beneficiario);
        }

        /// <summary>
        /// Edita um beneficiário existente
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditarBeneficiario([FromBody] BeneficiarioEdicaoDto dto)
        {
            var beneficiario = await _beneficiarioService.EditarBeneficiarios(dto);

            if (!beneficiario.Status)
                return BadRequest(beneficiario);

            return Ok(beneficiario);
        }

        /// <summary>
        /// Deleta um beneficiário pelo ID
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SolicitarExclusao(int id,[FromBody] BeneficiarioExclusaoDto dto)
        {
            var resultado = await _beneficiarioService.SolicitarExclusaoAsync(id, dto.Prioridade);

            if (!resultado.Status)
            {
                if (resultado.Error == "NotFound")
                    return NotFound(resultado);

                if (resultado.Error == "ValidationError")
                    return BadRequest(resultado);
            }

            return Ok(resultado);
        }

        //<summary>
        //Cria um novo beneficiário
        //</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CriarBeneficiario([FromBody] BeneficiarioDto dto)
        {
            var result = await _beneficiarioService.CriarBeneficiario(dto);

            return CreatedAtAction(
                nameof(Detalhe),
                new { id = result.Dados.Id },
                result
            );
        }
    }
}
