using Ejercicios.Data;
using Ejercicios.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ejercicios.Controllers;

public class EventosDeportivosController : Controller
{
    private ApplicationDbContext _context;

    public EventosDeportivosController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public JsonResult ListadoEventosDeportivos(int? EventoDeportivoID)
    {
        var listadoEventosDeportivos = _context.EventosDeportivos.ToList();
            listadoEventosDeportivos = _context.EventosDeportivos.OrderBy(l => l.Nombre).ToList();

        if(EventoDeportivoID != null)
        {
            listadoEventosDeportivos = _context.EventosDeportivos.Where(l => l.EventoDeportivoID == EventoDeportivoID).ToList();
        }
        return Json(listadoEventosDeportivos);
    }

    public JsonResult GuardarEventoDeportivo (int EventoDeportivoID, string Nombre)
    {
        string resultado = "";

        Nombre = Nombre.ToUpper();

        if(EventoDeportivoID == 0)
        {
            var nuevoEventoDeportivo = new EventoDeportivo
            {
                Nombre = Nombre,
            };
            _context.Add(nuevoEventoDeportivo);
            _context.SaveChanges();
            resultado = "Evento Guardado";
        }
        else
        {
            var editarEventoDeportivo = _context.EventosDeportivos.Where(e => e.EventoDeportivoID == EventoDeportivoID).SingleOrDefault();
            
            if(editarEventoDeportivo != null)
            {
                editarEventoDeportivo.Nombre = Nombre;
                _context.SaveChanges();
                resultado = "Evento Editado";
            }
        }
        return Json(resultado);
    }

    public JsonResult EliminarEventoDeportivo(int EventoDeportivoID )
{
    var eventoDeportivo = _context.EventosDeportivos.Find(EventoDeportivoID);

    // Verificar si la localidad está siendo utilizada por algún empleado
    var ejerciciosAsociados = _context.EjerciciosFisicos.Any(e => e.EventoDeportivoID == EventoDeportivoID);

    if (ejerciciosAsociados)
    {
        return Json("No se puede eliminar el evento. Está asociada a uno o más ejercicios.");
    }
    else
    {
        if (eventoDeportivo != null)
        {
            _context.EventosDeportivos.Remove(eventoDeportivo);
            _context.SaveChanges();
            return Json("Evento eliminada correctamente.");
        }
        return Json("No se encontró el evento.");
    }
}
}
