using Ejercicios.Data;
using Ejercicios.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ejercicios.Controllers;

[Authorize(Roles = "ADMINISTRADOR")]

public class AdminController : Controller
{
    private ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

public IActionResult Index()
{
    return View();
}

    public JsonResult InformeCuentas(int? id)
    {
        List<VistaCuenta> vistaCuentas = new List<VistaCuenta>();

        var personas = _context.Personas.ToList();

        foreach (var persona in personas)
        {
            var rol = _context.UserRoles.Where(r => r.UserId == persona.UsuarioID).SingleOrDefault();
            var rolName = _context.Roles.Where(r => r.Id == rol.RoleId).SingleOrDefault();
            var email = _context.Users.Where(r => r.Id == persona.UsuarioID).SingleOrDefault();

            var vistaCuenta = new VistaCuenta
            {
                PersonaID = persona.PersonaID,
                NombreCompleto = persona.NombreCompleto,
                EmailPersona = email.Email,
                FechaNacimiento = persona.FechaNacimiento.ToString("dd/MM/yyyy"),
                Genero = Enum.GetName(typeof(Genero),persona.Genero),
                Peso = persona.Peso,
                Altura = persona.Altura,
                Rol = rolName.Name,

            };
            vistaCuentas.Add(vistaCuenta);
        }

        return Json(vistaCuentas);
    }
}