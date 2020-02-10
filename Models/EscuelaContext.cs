using System;
using System.Collections.Generic;
using System.Linq;
using asp.net.Models;
using Microsoft.EntityFrameworkCore;

namespace asp.net.Models
{
    public class EscuelaContext : DbContext
    {
        public DbSet<Escuela> Escuelas { get; set; }
        public DbSet<Asignatura> Asignaturas { get; set; }
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Evaluación> Evaluaciones { get; set; }


        public EscuelaContext(DbContextOptions<EscuelaContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            var escuela = new Escuela();

            escuela.AñoDeCreación = 2005;
            escuela.Id = Guid.NewGuid().ToString();
            escuela.Nombre = "UMG";

            escuela.Dirección = "Direccion de la escuela";
            escuela.Ciudad = "Guatemala";
            escuela.Pais = "Guatemala";
            escuela.TipoEscuela = TiposEscuela.Secundaria;

            //Cargar cursos de la escuela

            var cursos = CargarCursos(escuela);

            //Por cada cursos cargar asignaturas 

            var asignaturas = CargarAsignaturas(cursos);

            //Por cada cursos cargar alumnos 

            var alumnos = CargarAlumnos(cursos);

            //x cada alumno cargar evaluaciones

            // modelBuilder.Entity<Asignatura>().HasData(
            //                 new Asignatura
            //                 {
            //                     Nombre = "Matemáticas",
            //                     // CursoId = curso.id,
            //                     Id = Guid.NewGuid().ToString()
            //                 },
            //                 new Asignatura
            //                 {
            //                     Nombre = "Educación Física",
            //                     Id = Guid.NewGuid().ToString()
            //                 },
            //                 new Asignatura
            //                 {
            //                     Nombre = "Castellano",
            //                     Id = Guid.NewGuid().ToString()
            //                 },
            //                 new Asignatura
            //                 {
            //                     Nombre = "Programacion",
            //                     Id = Guid.NewGuid().ToString()
            //                 },
            //                 new Asignatura
            //                 {
            //                     Nombre = "Ciencias Naturales",
            //                     Id = Guid.NewGuid().ToString()

            //                 }
            // );

            // modelBuilder.Entity<Alumno>().HasData(GenerarAlumnosAlAzar().ToArray());

            modelBuilder.Entity<Escuela>().HasData(escuela);
            modelBuilder.Entity<Curso>().HasData(cursos.ToArray());
            modelBuilder.Entity<Asignatura>().HasData(asignaturas.ToArray());
            modelBuilder.Entity<Alumno>().HasData(alumnos.ToArray());
        }

        private List<Alumno> CargarAlumnos(List<Curso> cursos){
            var listaAlumnos = new List<Alumno>();

            Random rnd = new Random();
            foreach(var curso in cursos){
                int cantRandom = rnd.Next(5, 20);
                var tmpList = GenerarAlumnosAlAzar(cantRandom, curso);
                listaAlumnos.AddRange(tmpList);
            }
            return listaAlumnos;
        }

        private static List<Asignatura> CargarAsignaturas(List<Curso> cursos)
        {
            var listaCompleta = new List<Asignatura> ();
            foreach (var curso in cursos)
            {
                var tmpList = new List<Asignatura>{
                    new Asignatura{ Id = Guid.NewGuid().ToString(), CursoId = curso.Id, Nombre= "Matematicas"},
                    new Asignatura{ Id = Guid.NewGuid().ToString(), CursoId = curso.Id, Nombre= "Fisica"},
                    new Asignatura{ Id = Guid.NewGuid().ToString(), CursoId = curso.Id, Nombre= "Castellano"},
                    new Asignatura{ Id = Guid.NewGuid().ToString(), CursoId = curso.Id, Nombre= "Ciencias Naturales"},
                    new Asignatura{ Id = Guid.NewGuid().ToString(), CursoId = curso.Id, Nombre= "Programacion"}
                };
                listaCompleta.AddRange(tmpList);
                // curso.Asignaturas = tmpList;
            }
            return listaCompleta;
        }

        private static List<Curso> CargarCursos(Escuela escuela)
        {
            return new List<Curso>(){
                new Curso(){ Id = Guid.NewGuid().ToString(), EscuelaId = escuela.Id, Nombre = "201", Jornada = TiposJornada.Mañana},
                new Curso(){ Id = Guid.NewGuid().ToString(), EscuelaId = escuela.Id, Nombre = "301", Jornada = TiposJornada.Mañana},
                new Curso(){ Id = Guid.NewGuid().ToString(), EscuelaId = escuela.Id, Nombre = "401", Jornada = TiposJornada.Tarde},
                new Curso(){ Id = Guid.NewGuid().ToString(), EscuelaId = escuela.Id, Nombre = "501", Jornada = TiposJornada.Tarde},
            };
        }

        private List<Alumno> GenerarAlumnosAlAzar(int cantidad, Curso curso)
        {
            string[] nombre1 = { "Alba", "Felipa", "Eusebio", "Farid", "Donald", "Alvaro", "Nicolás" };
            string[] apellido1 = { "Ruiz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera" };
            string[] nombre2 = { "Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes", "Teodoro" };

            var listaAlumnos = from n1 in nombre1
                               from n2 in nombre2
                               from a1 in apellido1
                               select new Alumno
                               {
                                   CursoId = curso.Id,
                                   Nombre = $"{n1} {n2} {a1}",
                                   Id = Guid.NewGuid().ToString()
                               };

            return listaAlumnos.OrderBy((al) => al.Id).Take(cantidad).ToList();
        }

    }
}