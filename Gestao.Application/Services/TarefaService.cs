using Gestao.Application.Dtos;
using Gestao.Application.Services.Interfaces;
using Gestao.Data.Dtos;
using Gestao.Data.Dtos.Request;
using Gestao.Data.Repositories.Interfaces;
using Gestao.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Gestao.Application.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly ITarefaRepositorio _tarefaRepositorio;

        public TarefaService(ITarefaRepositorio tarefaRepositorio)
        {
            _tarefaRepositorio = tarefaRepositorio;
        }

        public async Task<TarefaDTO> Create(TarefaRequest request)
        {
            try
            {
                if (await _tarefaRepositorio.GetByNomeAsync(request.Nome) != null)
                    throw new Exception("Já existe uma tarefa com esse nome.");

                if (request.DataInicio > request.DataFim)
                    throw new Exception("Data de início maior que a data final.");

                var dto = new TarefaDTO()
                {
                    Nome = request.Nome,
                    DataInicio = request.DataInicio,
                    DataFim = request.DataFim,
                    Situacao = request.Situacao,
                    PessoaId = request.PessoaId,
                    DuracaoEstimada = request.DataFim != null && request.DataInicio != null ? (request.DataFim - request.DataInicio) : TimeSpan.Zero,

                };

                return await _tarefaRepositorio.CreateTarefa(dto);
                
            }
            catch (Exception _error)
            {
                throw new Exception(_error.Message);
            }
        }

        public async Task<TarefaDTO> UpdateSituacao(long idTarefa, TarefaEstadoEnum situacao)
        {
            try
            {
                var tarefa = await _tarefaRepositorio.GetByIdAsync(idTarefa);

                if (tarefa == null)
                    throw new Exception("Tarefa não encontrada.");

                if (tarefa.Situacao == TarefaEstadoEnum.FINALIZADA)
                    throw new Exception("Não é possível alterar a situação de uma tarefa já Finalizada.");

                if (situacao == TarefaEstadoEnum.ANDAMENTO)
                {
                    tarefa.DataInicio = DateTime.Now;
                    tarefa.DuracaoEstimada = tarefa.DataFim - tarefa.DataInicio;
                }                    

                tarefa.Situacao = situacao;

                return await _tarefaRepositorio.AtualizarSituacao(idTarefa, situacao);

            }
            catch (Exception _error)
            {
                throw new Exception(_error.Message);
            }
        }

        public async Task<PeriodoDTO> GetPeriodoTempo(long idTarefa)
        {
            try
            {
                var response = await _tarefaRepositorio.RetornoPeriodoTempo(idTarefa).ConfigureAwait(false);

                return response;
            }
            catch (Exception _error)
            {
                throw new Exception(_error.Message);
            }
        }

        public async Task<bool> FinalizarTarefa(long idTarefa)
        {
            try
            {
                var response = await _tarefaRepositorio.FinalizarTarefa(idTarefa).ConfigureAwait(false);

                return response;
            }
            catch (Exception _error)
            {
                throw new Exception(_error.Message);
            }
        }

        public async Task<ArquivoDTO> AnexarArquivo(IFormFile file, long idTarefa)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return null;
                }

                string uniqueFileName = GetUniqueFileName(file.FileName);
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var extensao = uniqueFileName.Split(".");

                var newFile = new ArquivoDTO()
                {
                    TarefaId = idTarefa,
                    Nome = uniqueFileName,
                    Caminho = uploadsFolder,
                    Extensao = extensao[1]                  

                };

                var response = await _tarefaRepositorio.AnexarArquivo(newFile);

                return response;

            }
            catch (Exception _error)
            {
                throw new Exception(_error.Message);
            }
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.ChangeExtension(Guid.NewGuid().ToString(), Path.GetExtension(fileName));
        }


    }
}
