// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace AppPruebaMVC.Data.Models
{
    public partial class Diagnostico
    {
        public string TipoDiagnostico { get; set; }
        public int Codigo { get; set; }
        public int CodResultado { get; set; }
        public int CodEnfermedad { get; set; }

        public virtual Enfermedad CodEnfermedadNavigation { get; set; }
        public virtual Resultado CodResultadoNavigation { get; set; }
    }
}