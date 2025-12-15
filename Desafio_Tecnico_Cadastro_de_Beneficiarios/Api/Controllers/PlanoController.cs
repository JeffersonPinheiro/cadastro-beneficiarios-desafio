using Desafio_Tecnico_Cadastro_de_Beneficiarios.Application.Dto.Plano;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Application.Interfaces;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanoController : ControllerBase
    {
        private readonly IPlanosService _planoService;

        public PlanoController(IPlanosService planoService)
        {
            _planoService = planoService;
        }

        /// <summary>
        /// Busca plano pelo ID
        /// </summary>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPlanoPorId(int id)
        {
            var result = await _planoService.BuscarPlanoPorId(id);

            if (!result.Status)
                return NotFound(result);

            return Ok(result);
        }

        /// <summary>
        /// Criar Plano
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CriarPlano([FromBody] PlanoDto planoCriacaoDto)
        {
            var plano = await _planoService.CriarPlano(planoCriacaoDto);

            if (!plano.Status)
            {
                if (plano.Error == "ValidationError")
                    return BadRequest(plano);

                return StatusCode(StatusCodes.Status500InternalServerError, plano);
            }

            return CreatedAtAction(nameof(BuscarPlanoPorId), new { id = plano.Dados.Id }, plano);
        }

        /// <summary>
        /// Edita um plano existente
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditarPlano([FromBody] PlanoEdicaoDto planoEdicaoDto)
        {
            var plano = await _planoService.EditarPlano(planoEdicaoDto);

            if(!plano.Status)
                return BadRequest(plano);

            return Ok(plano);
        }

        /// <summary>
        /// Deleta um plano pelo ID
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletarPlano(int id)
        {
            var plano = await _planoService.DeletarPlano(id);

            if (!plano.Status)
                return NotFound(plano);

            return Ok(plano);
        }
    }
}
