using System.ComponentModel.DataAnnotations;

namespace Ejercicios.Models;

public class Persona
{
    [Key]
    public int PersonaID { get; set; }
    public string? UsuarioID { get; set; }
    public string? NombreCompleto { get; set; }
    public DateOnly FechaNacimiento { get; set; }
    public Genero Genero { get; set; }
    public decimal Peso { get; set; }
    public decimal Altura { get; set; }
    public virtual ICollection<EjercicioFisico> EjercicioFisicos { get; set; }
    public virtual ICollection<Lugar> Lugares { get; set; }
}

public enum Genero
{
    Masculino = 1,
    Femenino,
    Otro
}