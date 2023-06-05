using AutoMapper;
using Gestao.Application.Dtos;
using Gestao.Data.Context;
using Gestao.Data.Dtos;
using Gestao.Data.Repositories.Interfaces;
using Gestao.Domain.Enums;
using Gestao.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestao.Data.Repositories
{
    public class TarefaRepositorio : ITarefaRepositorio
    {
        private readonly GestaoContext _context = null;
        private readonly IMapper _mapper = null;

        public TarefaRepositorio(GestaoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TarefaDTO> CreateTarefa(TarefaDTO dto)
        {
            Tarefa tarefa = _mapper.Map<Tarefa>(dto);

            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();

            return _mapper.Map<TarefaDTO>(tarefa);
        }

        public async Task<TarefaDTO> AtualizarSituacao(long idTarefa, TarefaEstadoEnum situacao)
        {
            var tarefa = await _context.Tarefas.FirstOrDefaultAsync(t => t.Id == idTarefa);

            if (tarefa == null) return null;

            tarefa.Situacao = situacao;
            _context.Tarefas.Update(tarefa);
            await _context.SaveChangesAsync();

            return _mapper.Map<TarefaDTO>(tarefa);
        }

        public async Task<PeriodoDTO> RetornoPeriodoTempo(long idTarefa)
        {
            var tarefa = await _context.Tarefas.FirstOrDefaultAsync(t => t.Id == idTarefa);

            if (tarefa == null) return null;

            if (tarefa.DataInicio == null) throw new Exception("Tarefa ainda não foi iniciada.");

            TimeSpan periodoResponse = TimeSpan.Zero;

            if (tarefa.DataInicio.HasValue) periodoResponse = tarefa.DataFim - tarefa.DataInicio.Value;

            PeriodoDTO periodo = new PeriodoDTO
            {
                Idtarefa = tarefa.Id,
                NomeTarefa = tarefa.Nome,
                Dias = periodoResponse.Days,
                Horas = periodoResponse.Hours,
                Minutos = periodoResponse.Minutes
            };

            return periodo;
        }

        public async Task<bool> FinalizarTarefa(long idTarefa)
        {
            var tarefa = await _context.Tarefas.FirstOrDefaultAsync(t => t.Id == idTarefa);

            if (tarefa == null) return false;

            tarefa.Situacao = TarefaEstadoEnum.FINALIZADA;
            _context.Update(tarefa);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ArquivoDTO> AnexarArquivo(ArquivoDTO dto)
        {
            Arquivo arquivo = _mapper.Map<Arquivo>(dto);

            _context.Arquivos.Add(arquivo);
            await _context.SaveChangesAsync();

            return _mapper.Map<ArquivoDTO>(arquivo);
        }

        public async Task<TarefaDTO> GetByNomeAsync(string nome)
        {
            IQueryable<Tarefa> query = _context.Tarefas;

            query = query.AsNoTracking()
                        .OrderBy(t => t.Nome)
                        .Where(t => t.Nome.Equals(nome));

            return  _mapper.Map<TarefaDTO>(await query.FirstOrDefaultAsync());
        }

        public async Task<TarefaDTO> GetByIdAsync(long id)
        {
            IQueryable<Tarefa> query = _context.Tarefas;

            query = query.AsNoTracking()
                        .OrderBy(t => t.Id)
                        .Where(t => t.Id == id);

            return _mapper.Map<TarefaDTO>(await query.FirstOrDefaultAsync());
        }
    }
}
