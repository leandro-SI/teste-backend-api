using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gestao.Domain.Models
{
    public class Arquivo
    {
        [Key]
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Extensao { get; set; }
        public string Caminho { get; set; }

        [Required]
        public long TarefaId { get; set; }

        [ForeignKey(nameof(TarefaId))]
        public Tarefa Tarefa { get; set; }

    }
}
