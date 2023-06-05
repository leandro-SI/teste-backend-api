using Gestao.Domain.Enums;
using Gestao.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gestao.Application.Dtos
{
    public class TarefaDTO
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public TimeSpan DuracaoEstimada { get; set; }
        public TarefaEstadoEnum Situacao { get; set; }
        public long PessoaId { get; set; }
    }
}
