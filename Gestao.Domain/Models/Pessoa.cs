using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gestao.Domain.Models
{
    [Table("pessoas")]
    public class Pessoa
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("nome")]
        public string Nome { get; set; }
    }
}
