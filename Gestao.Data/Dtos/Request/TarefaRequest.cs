using Gestao.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gestao.Data.Dtos.Request
{
    public class TarefaRequest
    {
        [Required]
        public string Nome { get; set; }
        public DateTime? DataInicio { get; set; }
        [Required]
        public DateTime DataFim { get; set; }
        [Required]
        public TarefaEstadoEnum Situacao { get; set; }
        [Required]
        public long PessoaId { get; set; }
    }
}
