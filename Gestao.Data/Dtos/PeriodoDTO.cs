using System;
using System.Collections.Generic;
using System.Text;

namespace Gestao.Data.Dtos
{
    public class PeriodoDTO
    {
        public long Idtarefa { get; set; }
        public string NomeTarefa { get; set; }
        public TimeSpan Periodo { get; set; }
    }
}
