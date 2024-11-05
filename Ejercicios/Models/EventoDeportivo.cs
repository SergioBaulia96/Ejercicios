using System.ComponentModel.DataAnnotations;

namespace Ejercicios.Models;

public class EventoDeportivo
{
    [Key]
    public int EventoDeportivoID { get; set; }
    public string? Nombre { get; set; }
    public bool Eliminado { get; set; }

    ICollection<EjercicioFisico> EjercicioFisicos { get; set; }
}

public class VistaEventoDeportivo
{
    public int EventoDeportivoID { get; set; }
    public string? Nombre { get; set; }
    public List<VistaLugar> ListadoLugares { get; set; }
}