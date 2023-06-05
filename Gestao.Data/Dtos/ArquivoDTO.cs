using Gestao.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gestao.Application.Dtos
{
    public class ArquivoDTO
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Extensao { get; set; }
        public string Caminho { get; set; }
        public long TarefaId { get; set; }

    }
}
