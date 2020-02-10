using System;
using System.Collections.Generic;

namespace asp.net.Models
{
    public class Alumno: ObjetoEscuelaBase
    {
        public List<Evaluación> Evaluaciones { get; set; } //= new List<Evaluación>();

        public string CursoId { get; set; }
        public Curso Curso { get; set; }
    }
}