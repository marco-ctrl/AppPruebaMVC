// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace AppPruebaMVC.Data.Models
{
    public partial class CitaMedica
    {
        public CitaMedica()
        {
            Admicions = new HashSet<Admicion>();
            Resultados = new HashSet<Resultado>();
        }

        public bool Estado { get; set; }
        public DateTime FechaHora { get; set; }
        public int Codigo { get; set; }
        public int CodUsuario { get; set; }
        public int CodDoctor { get; set; }
        public int CodPaciente { get; set; }

        public virtual Doctor CodDoctorNavigation { get; set; }
        public virtual Paciente CodPacienteNavigation { get; set; }
        public virtual Usuario CodUsuarioNavigation { get; set; }
        public virtual ICollection<Admicion> Admicions { get; set; }
        public virtual ICollection<Resultado> Resultados { get; set; }
    }
}