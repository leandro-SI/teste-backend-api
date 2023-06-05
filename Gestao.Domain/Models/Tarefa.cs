using Gestao.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gestao.Domain.Models
{
    public class Tarefa
    {
        [Key]
        public long Id { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public DateTime DuracaoEstimada { get; set; }

        [Required]
        public TarefaEstadoEnum Situacao { get; set; }

        [Required]
        public long PessoaId { get; set; }

        [ForeignKey(nameof(PessoaId))]
        public Pessoa Pessoa { get; set; }


        public Tarefa()
        {
            DataFim = null;

        }

        public void Concluir()
        {
            if (DataFim == null && Situacao != TarefaEstadoEnum.FINALIZADA)
            {
                this.DataFim = DateTime.Now;
                this.Situacao = TarefaEstadoEnum.FINALIZADA;
            }
            else
            {
                throw new Exception($"Tarefa já concluída em: {DataFim?.ToString("dd/MM/yyyy hh:mm")}");
            }
        }

    }
}
