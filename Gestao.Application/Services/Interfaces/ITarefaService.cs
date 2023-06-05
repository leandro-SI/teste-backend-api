using Gestao.Application.Dtos;
using Gestao.Data.Dtos;
using Gestao.Data.Dtos.Request;
using Gestao.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gestao.Application.Services.Interfaces
{
    public interface ITarefaService
    {
        Task<TarefaDTO> Create(TarefaRequest request);
        Task<TarefaDTO> UpdateSituacao(long idTarefa, TarefaEstadoEnum situacao);
        Task<PeriodoDTO> GetPeriodoTempo(long idTarefa);
        Task<bool> FinalizarTarefa(long idTarefa);
        Task<ArquivoDTO> AnexarArquivo(IFormFile file, long idTarefa);
    }
}
