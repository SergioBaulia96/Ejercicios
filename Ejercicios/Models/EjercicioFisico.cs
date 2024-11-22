using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Ejercicios.Models;

namespace Ejercicios.Models
{

    public class EjercicioFisico
    {
        [Key]
        public int EjercicioFisicoID { get; set; }
        public int TipoEjercicioID { get; set; }
        public int LugarID { get; set; }
        public int? PersonaID { get; set; }
        public int EventoDeportivoID { get; set;}
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }

        [NotMapped]

        public TimeSpan IntervaloEjercicio { get {return Fin - Inicio;}}
        public EstadoEmocional EstadoEmocionalInicio { get; set; }
        public EstadoEmocional EstadoEmocionalFin { get; set; }
        public string? Observaciones { get; set; }
        public virtual TipoEjercicio TipoEjercicios{ get; set; }
        public virtual Lugar Lugar {get; set;}
        public virtual EventoDeportivo EventosDeportivos { get; set; }
        public virtual Persona Personas { get; set; }
    }

    public class VistaEjercicioFisico
    {
        public int EjercicioFisicoID {get; set; }
        public int TipoEjercicioID { get; set; }
        public int LugarID { get; set; }
        public int PersonaID { get; set; }
        public int EventoDeportivoID { get; set; }
        public string? NombreEvento { get; set; }
        public string? Descripcion { get; set; }
        public string? NombreLugar { get; set; }
        public string? InicioString { get; set; }
        public string? FinString { get; set; }
        public decimal IntervaloEjercicio { get; set; }
        public string? EstadoEmocionalInicio {get; set; }
        public string? EstadoEmocionalFin {get; set; }
        public string? Observaciones {get; set; }
        public decimal PersonaPeso { get; set; }
        public string? MetEjercicio { get; set; }
        public decimal ClQuemadas { get; set; }
    }

    public enum EstadoEmocional{
        Feliz = 1,
        Triste,
        Enojado,
        Ansioso,
        Estresado,
        Relajado,
        Aburrido,
        Emocionado,
        Agobiado,
        Confundido,
        Optimista,
        Pesimista,
        Motivado,
        Cansado,
        Eufórico,
        Agitado,
        Satisfecho,
        Desanimado
    }

}

