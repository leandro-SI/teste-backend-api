using Gestao.Application.Dtos;
using Gestao.Application.Services.Interfaces;
using Gestao.Data.Dtos.Request;
using Gestao.Domain.Enums;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Gestao.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaService _tarefaService;

        public TarefaController(ITarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }

        /// <summary>
        /// Tipos de SItuações
        /// </summary>
        /// <param name="request">Situação: AGENDADA = 1 - ANDAMENTO = 2 - FINALIZADA = 3</param>
        /// <returns></returns>
        /// 
        [HttpPost]
        [ActionName("criar_tarefa")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CriarTarefa([FromBody] TarefaRequest request)
        {
            try
            {
                var response = await _tarefaService.Create(request);

                if (response == null) return NoContent();

                return Ok(response);
            }
            catch (Exception _error)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar salvar a tarefa. Erro: {_error.Message}"); ;
            }
        }

        [HttpPut]
        [ActionName("alterar_situacao")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AlterarSituacao(long idTarefa, TarefaEstadoEnum situacao)
        {
            try
            {
                var response = await _tarefaService.UpdateSituacao(idTarefa, situacao);

                if (response == null) return NoContent();

                return Ok(response);
            }
            catch (Exception _error)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar alterar a situação da tarefa. Erro: {_error.Message}"); ;
            }

        }

        [HttpGet]
        [ActionName("periodo_em_andamento")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPeriodoTempo(long idTarefa)
        {
            try
            {
                var response = await _tarefaService.GetPeriodoTempo(idTarefa);

                if (response == null) return NoContent();

                return Ok(response);
            }
            catch (Exception _error)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar retornar o período de tempo da tarefa. Erro: {_error.Message}"); ;
            }

        }

        [HttpPut]
        [ActionName("finalizar_tarefa")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> FinalizarTarefa(long idTarefa)
        {
            try
            {
                var response = await _tarefaService.FinalizarTarefa(idTarefa);

                if (!response) return BadRequest("Erro ao tentar finalizar tarefa");

                return Ok("Tarefa finalizada com sucesso.");
            }
            catch (Exception _error)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar finalizar a tarefa. Erro: {_error.Message}"); ;
            }

        }

        [HttpPost]
        [ActionName("anexar_arquivo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AnexarArquivo(IFormFile file, long idTarefa)
        {
            try
            {
                var response = await _tarefaService.AnexarArquivo(file, idTarefa);

                if (response == null) return BadRequest("Erro ao tentar anexar arquivo");

                return Ok(response);
            }
            catch (Exception _error)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar anexar arquivo. Erro: {_error.Message}"); ;
            }

        }



    }
}
