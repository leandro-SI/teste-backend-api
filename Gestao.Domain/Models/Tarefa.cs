using Gestao.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gestao.Domain.Models
{
    [Table("tarefas")]
    public class Tarefa
    {
        [Key]
        public long Id { get; set; }

        [Column("nome")]
        public string Nome { get; set; }

        [Column("data_inicio")]
        public DateTime DataInicio { get; set; }

        [Column("data_fim")]
        public DateTime DataFim { get; set; }

        [Column("duracao_estimada")]
        public DateTime DuracaoEstimada { get; set; }

        [Column("situacao")]
        [Required]
        public TarefaEstadoEnum Situacao { get; set; }

        [Column("pessoa_id")]
        public long PessoaId { get; set; }

        [ForeignKey(nameof(PessoaId))]
        public Pessoa Pessoa { get; set; }


        public void FinalizarTarefa()
        {
            if (DataFim == null && Situacao != TarefaEstadoEnum.FINALIZADA)
            {
                this.DataFim = DateTime.Now;
                this.Situacao = TarefaEstadoEnum.FINALIZADA;
            }
            else
            {
                throw new Exception($"Tarefa já concluída em: {DataFim.ToString("dd/MM/yyyy hh:mm")}");
            }
        }

    }
}
