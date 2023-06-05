using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gestao.Domain.Models
{
    public class Pessoa
    {
        [Key]
        public long Id { get; set; }
        public string Nome { get; set; }
    }
}
