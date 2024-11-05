
using System.ComponentModel.DataAnnotations;

namespace Ejercicios.Models;

public class Lugar 
{
    [Key]
    public int LugarID { get; set; }
    public int? PersonaID { get; set; }
    public string? Nombre { get; set;}
    public virtual ICollection<EjercicioFisico> EjercicioFisicos { get; set;}
    public virtual Persona Personas { get; set;}
}

public class VistaTipoLugar
{   
    public int LugarID { get; set; }
    public string? NombreLugar { get; set; }
    public List<VistaEjercicioFisico> ListadoEjercicios { get; set; }
}

public class VistaLugar
{
    public int LugarID { get; set; }
    public string? NombreLugar { get; set; }
    public List<VistaTipoEjercicios> ListadoTipoEjercicios { get; set; }
}