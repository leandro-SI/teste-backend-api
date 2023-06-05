using Gestao.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gestao.Data.Context
{
    public class GestaoContext : DbContext
    {
        public GestaoContext(DbContextOptions<GestaoContext> options) : base(options)
        {
        }

        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Arquivo> Arquivos { get; set; }
    }
}
