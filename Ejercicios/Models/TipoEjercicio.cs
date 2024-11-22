using System.ComponentModel.DataAnnotations;

namespace Ejercicios.Models
{

public class TipoEjercicio
{
    [Key]
    public int TipoEjercicioID { get; set;}
    public string? Descripcion { get; set;}
    public decimal Met { get; set;}
    public bool Eliminado { get; set;}
    public virtual ICollection<EjercicioFisico> EjercicioFisicos { get; set;}
}

public class VistaTipoEjercicio
{
    public int TipoEjercicioID { get; set; }
    public string? Descripcion { get; set;}
    public decimal Met { get; set;}
    public List<VistaTipoLugar> ListadoLugares { get; set; }
}

public class VistaTipoEjercicios
{
    public int TipoEjercicioID { get; set; }
    public string? Descripcion { get; set;}
    public decimal Met { get; set;}
    public List<VistaEjercicioFisico> ListadoEjercicios { get; set; }
}
}