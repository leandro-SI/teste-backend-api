using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gestao.Domain.Models
{
    [Table("arquivos")]
    public class Arquivo
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("nome")]
        public string Nome { get; set; }
        [Column("extensao")]
        public string Extensao { get; set; }
        [Column("caminho")]
        public string Caminho { get; set; }

        [Column("tarefa_id")]
        public long TarefaId { get; set; }

        [ForeignKey(nameof(TarefaId))]
        public Tarefa Tarefa { get; set; }

    }
}
