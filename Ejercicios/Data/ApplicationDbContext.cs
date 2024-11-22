using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ejercicios.Models;
using System.Security.Claims;

namespace Ejercicios.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
        public DbSet<TipoEjercicio> TipoEjercicios { get; set; }
        public DbSet<EjercicioFisico> EjerciciosFisicos { get; set; }
        public DbSet<Lugar> Lugares { get; set; }
        public DbSet<EventoDeportivo> EventosDeportivos {get; set;}
        public DbSet<Persona> Personas { get; set; }

    internal object GetUserId(ClaimsPrincipal user)
    {
        throw new NotImplementedException();
    }
}
