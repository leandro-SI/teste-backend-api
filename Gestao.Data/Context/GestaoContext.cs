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
        public GestaoContext()
        {
            
        }

        public GestaoContext(DbContextOptions<GestaoContext> options) : base(options)
        {
        }

        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Arquivo> Arquivos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoa>().HasData(new Pessoa
            {
                Id = 1,
                Nome = "João Silva"
            });

            modelBuilder.Entity<Pessoa>().HasData(new Pessoa
            {
                Id = 2,
                Nome = "Ana Silva"
            });
        }
    }
}
