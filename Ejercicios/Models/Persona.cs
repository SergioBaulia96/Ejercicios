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

public class VistaCuenta
{
    public int PersonaID { get; set; }
    public string? NombreCompleto { get; set; }
    public string? FechaNacimiento { get; set; }
    public string? Genero { get; set; }
    public decimal Peso { get; set; }
    public decimal Altura { get; set; }
    public string? Edad { get; set; }
    public string? Imc { get; set; }
    public string? Tmb { get; set; }
    public string? EmailPersona { get; set; }
    public string? Rol { get; set; }
}