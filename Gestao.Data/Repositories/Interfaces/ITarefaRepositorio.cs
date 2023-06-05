using Gestao.Application.Dtos;
using Gestao.Data.Dtos;
using Gestao.Domain.Enums;
using Gestao.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gestao.Data.Repositories.Interfaces
{
    public interface ITarefaRepositorio
    {
        Task<TarefaDTO> CreateTarefa(TarefaDTO dto);
        Task<TarefaDTO> AtualizarSituacao(long idTarefa, TarefaEstadoEnum situacao);
        Task<PeriodoDTO> RetornoPeriodoTempo(long idTarefa);
        Task<bool> FinalizarTarefa(long idTarefa);
        Task<ArquivoDTO> AnexarArquivo(ArquivoDTO arquivo);
        Task<TarefaDTO> GetByNomeAsync(string nome);
        Task<TarefaDTO> GetByIdAsync(long id);
    }
}
