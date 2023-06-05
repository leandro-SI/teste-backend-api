using Gestao.Application.Dtos;
using Gestao.Application.Services.Interfaces;
using Gestao.Data.Dtos;
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

        public async Task<TarefaDTO> Create(TarefaDTO dto)
        {
            try
            {
                if (await _tarefaRepositorio.GetByNomeAsync(dto.Nome) != null)
                    throw new Exception("Já existe uma tarefa com esse nome.");

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
                    throw new Exception("Tarefa é possível alterar a situação de uma tarefa já Finalizada.");

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
                var response = await _tarefaRepositorio.FinalizarTarefa(idTarefa) .ConfigureAwait(false);

                return response;
            }
            catch (Exception _error)
            {
                throw new Exception(_error.Message);
            }
        }

        public async Task<ArquivoDTO> AnexarArquivo(IFormFile file, ArquivoDTO arquivo)
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

                var extensao = file.Name.Split('.');

                var newFile = new ArquivoDTO()
                {
                    Nome = arquivo.Nome != null ? arquivo.Nome : uniqueFileName,
                    Caminho = uploadsFolder,
                    Extensao = extensao[1]                  

                };

                var response = await _tarefaRepositorio.AnexarArquivo(arquivo);

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
