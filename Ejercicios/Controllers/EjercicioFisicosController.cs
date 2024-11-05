using Ejercicios.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ejercicios.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Ejercicios.Controllers;
[Authorize]

public class EjercicioFisicosController : Controller
{
    private ApplicationDbContext _context;
    public EjercicioFisicosController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(int? PersonaID)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        PersonaID = _context.Personas.Where(t => t.UsuarioID == userId).Select(t => t.PersonaID).SingleOrDefault();
        ViewBag.PersonaID = PersonaID;
        // Crear una lista de SelectListItem que incluya el elemento adicional
        var selectListItems = new List<SelectListItem>
        {
            new SelectListItem { Value = "0", Text = "[SELECCIONE...]" }
        };

        // Obtener todas las opciones del enum
        var enumValues = Enum.GetValues(typeof(EstadoEmocional)).Cast<EstadoEmocional>();

        // Convertir las opciones del enum en SelectListItem
        selectListItems.AddRange(enumValues.Select(e => new SelectListItem
        {
            Value = e.GetHashCode().ToString(),
            Text = e.ToString().ToUpper()
        }));

        // Pasar la lista de opciones al modelo de la vista
        ViewBag.EstadoEmocionalInicio = selectListItems.OrderBy(t => t.Text).ToList();
        ViewBag.EstadoEmocionalFin = selectListItems.OrderBy(t => t.Text).ToList();

        var tipoEjercicios = _context.TipoEjercicios.ToList();
        tipoEjercicios.Add(new TipoEjercicio { TipoEjercicioID = 0, Descripcion = "[SELECCIONE...]" });
        ViewBag.TipoEjercicioID = new SelectList(tipoEjercicios.OrderBy(c => c.Descripcion), "TipoEjercicioID", "Descripcion");

        var lugares = _context.Lugares.ToList();
        lugares.Add(new Lugar { LugarID = 0, Nombre = "[SELECCIONE...]" });
        ViewBag.LugarID = new SelectList(lugares.OrderBy(c => c.Nombre), "LugarID", "Nombre");

        var eventosDeportivos = _context.EventosDeportivos.ToList();
        eventosDeportivos.Add(new EventoDeportivo { EventoDeportivoID = 0, Nombre = "[SELECCIONE...]" });
        ViewBag.EventoDeportivoID = new SelectList(eventosDeportivos.OrderBy(c => c.Nombre), "EventoDeportivoID", "Nombre");

        return View();
    }
    public JsonResult ListadoEjerciciosFisicos(int? id)
    {
        List<VistaEjercicioFisico> ejerciciosFisicosMostrar = new List<VistaEjercicioFisico>();

        var ejerciciosFisicos = _context.EjerciciosFisicos.ToList();

        if (id != null)
        {
            ejerciciosFisicos = ejerciciosFisicos.Where(t => t.EjercicioFisicoID == id).ToList();
        }

        var tiposEjercicios = _context.TipoEjercicios.ToList();
        var lugares = _context.Lugares.ToList();
        var eventosDeportivos = _context.EventosDeportivos.ToList();

        foreach (var ejercicioFisicos in ejerciciosFisicos)
        {
            var tipoEjercicio = tiposEjercicios.Where(t => t.TipoEjercicioID == ejercicioFisicos.TipoEjercicioID).Single();
            var lugar = lugares.Where(t => t.LugarID == ejercicioFisicos.LugarID).Single();
            var eventoDeportivo = eventosDeportivos.Where(t => t.EventoDeportivoID == ejercicioFisicos.EventoDeportivoID).Single();

            var ejercicioFisicosMostrar = new VistaEjercicioFisico
            {
                EjercicioFisicoID = ejercicioFisicos.EjercicioFisicoID,
                TipoEjercicioID = ejercicioFisicos.TipoEjercicioID,
                LugarID = ejercicioFisicos.LugarID,
                EventoDeportivoID = ejercicioFisicos.EventoDeportivoID,
                NombreEvento = eventoDeportivo.Nombre,
                Descripcion = tipoEjercicio.Descripcion,
                NombreLugar = lugar.Nombre,
                InicioString = ejercicioFisicos.Inicio.ToString("dd/MM/yyyy HH:mm"),
                FinString = ejercicioFisicos.Fin.ToString("dd/MM/yyyy HH:mm"),
                EstadoEmocionalInicio = Enum.GetName(typeof(EstadoEmocional), ejercicioFisicos.EstadoEmocionalInicio),
                EstadoEmocionalFin = Enum.GetName(typeof(EstadoEmocional), ejercicioFisicos.EstadoEmocionalFin),
                Observaciones = ejercicioFisicos.Observaciones
            };
            ejerciciosFisicosMostrar.Add(ejercicioFisicosMostrar);
        }


        return Json(ejerciciosFisicosMostrar);
    }

    public JsonResult TraerEjerciciosFisicosAlModal(int? EjercicioFisicoID)
    {
        var ejerciciosFisicosporId = _context.EjerciciosFisicos.ToList();
        if (EjercicioFisicoID != null)
        {
            ejerciciosFisicosporId = ejerciciosFisicosporId.Where(e => e.EjercicioFisicoID == EjercicioFisicoID).ToList();
        }

        return Json(ejerciciosFisicosporId.ToList());
    }

    public JsonResult GuardarEjercicioFisico(
        int EjercicioFisicoID,
        int TipoEjercicioID,
        int LugarID,
        int EventoDeportivoID,
        DateTime Inicio,
        DateTime Fin,
        EstadoEmocional EstadoEmocionalInicio,
        EstadoEmocional EstadoEmocionalFin,
        string? Observaciones)
    {
        string resultado = "";
        if (EjercicioFisicoID == 0)
        {
            if (TipoEjercicioID > 0 && LugarID > 0 && EventoDeportivoID > 0)
            {
                var EjercicioFisico = new EjercicioFisico
                {
                    TipoEjercicioID = TipoEjercicioID,
                    LugarID = LugarID,
                    EventoDeportivoID = EventoDeportivoID,
                    Inicio = Inicio,
                    Fin = Fin,
                    EstadoEmocionalInicio = EstadoEmocionalInicio,
                    EstadoEmocionalFin = EstadoEmocionalFin,
                    Observaciones = Observaciones
                };
                _context.Add(EjercicioFisico);
                _context.SaveChanges();
            }
        }
        else
        {
            var editarEjercicioFisico = _context.EjerciciosFisicos.Where(e => e.EjercicioFisicoID == EjercicioFisicoID).SingleOrDefault();
            if (editarEjercicioFisico != null)
            {
                editarEjercicioFisico.TipoEjercicioID = TipoEjercicioID;
                editarEjercicioFisico.LugarID = LugarID;
                editarEjercicioFisico.EventoDeportivoID = EventoDeportivoID;
                editarEjercicioFisico.Inicio = Inicio;
                editarEjercicioFisico.Fin = Fin;
                editarEjercicioFisico.EstadoEmocionalInicio = EstadoEmocionalInicio;
                editarEjercicioFisico.EstadoEmocionalFin = EstadoEmocionalFin;
                editarEjercicioFisico.Observaciones = Observaciones;

                _context.SaveChanges();
            }
        }
        return Json(resultado);
    }

    public JsonResult EliminarEjercicioFisico(int EjercicioFisicoID)
    {
        var EjercicioFisico = _context.EjerciciosFisicos.Find(EjercicioFisicoID);
        _context.Remove(EjercicioFisico);
        _context.SaveChanges();

        return Json(true);
    }


    public IActionResult Informe()
    {
        var buscarTipoEjercicio = _context.TipoEjercicios.ToList();

        buscarTipoEjercicio.Add(new TipoEjercicio {TipoEjercicioID = 0, Descripcion = "[Ejercicios...]"});
        ViewBag.TipoEjercicioBuscar = new SelectList(buscarTipoEjercicio.OrderBy(e => e.Descripcion), "TipoEjercicioID", "Descripcion");
    return View();
    }

    public JsonResult ListadoInforme(DateTime? BuscarInicio, DateTime? BuscarFin, int? TipoEjercicioBuscar)
    {
        List<VistaTipoEjercicio> tiposEjerciciosMostrar = new List<VistaTipoEjercicio>();

        var ejerciciosFisicos = _context.EjerciciosFisicos.Include(t => t.TipoEjercicios).Include(l => l.Lugar).ToList();

        
        if (BuscarInicio != null && BuscarFin != null)
        {
            ejerciciosFisicos= ejerciciosFisicos.Where(e => e.Inicio >= BuscarInicio && e.Inicio <= BuscarFin).ToList();
        }
        
        if (TipoEjercicioBuscar != 0)
        {
            ejerciciosFisicos = ejerciciosFisicos.Where(e => e.TipoEjercicioID == TipoEjercicioBuscar).ToList();
        }

        foreach ( var e in ejerciciosFisicos)
        {
            var tipoEjercicioMostrar = tiposEjerciciosMostrar.Where(t => t.TipoEjercicioID == e.TipoEjercicioID).SingleOrDefault();
            if (tipoEjercicioMostrar == null)
            {
                tipoEjercicioMostrar = new VistaTipoEjercicio
                {
                    TipoEjercicioID = e.TipoEjercicioID,
                    Descripcion = e.TipoEjercicios.Descripcion,
                    ListadoLugares = new List<VistaTipoLugar>()
                };
                tiposEjerciciosMostrar.Add(tipoEjercicioMostrar);
            }

            var lugarMostrar = tipoEjercicioMostrar.ListadoLugares.Where(l => l.LugarID == e.LugarID).SingleOrDefault();
            if (lugarMostrar == null)
            {
                lugarMostrar = new VistaTipoLugar
                {
                    LugarID = e.LugarID,
                    NombreLugar = e.Lugar.Nombre,
                    ListadoEjercicios = new List<VistaEjercicioFisico>()
                };
                tipoEjercicioMostrar.ListadoLugares.Add(lugarMostrar);
            }

            var ejercicioFisicoMostrar = lugarMostrar.ListadoEjercicios.Where(f => f.EjercicioFisicoID == e.EjercicioFisicoID).SingleOrDefault();
            if (ejercicioFisicoMostrar == null)
            {
                ejercicioFisicoMostrar = new VistaEjercicioFisico
                {
                    EjercicioFisicoID = e.EjercicioFisicoID,
                    InicioString = e.Inicio.ToString("dd/MM/yyyy HH:mm"),
                    FinString = e.Fin.ToString("dd/MM/yyyy HH:mm"),
                    IntervaloEjercicio = Convert.ToDecimal(e.IntervaloEjercicio.TotalMinutes),
                    EstadoEmocionalInicio = Enum.GetName(typeof(EstadoEmocional), e.EstadoEmocionalInicio),
                    EstadoEmocionalFin = Enum.GetName(typeof(EstadoEmocional), e.EstadoEmocionalFin),
                    Observaciones = e.Observaciones
                };
                lugarMostrar.ListadoEjercicios.Add(ejercicioFisicoMostrar);
            }
        }
        
        return Json(tiposEjerciciosMostrar);
    }

    public IActionResult InformeLugares()
    {
        return View();
    }


        public JsonResult ListadoInformes(int? id, DateTime? BuscarInicio, DateTime? BuscarFin)
    {
        List<VistaEjercicioFisico> ejerciciosFisicosMostrar = new List<VistaEjercicioFisico>();
        
        // Obtener la lista de ejercicios físicos
        var ejercicioFisicos = _context.EjerciciosFisicos.AsQueryable();

        // Aplicar filtro por ID si se proporciona
        if (id != null)
        {
            ejercicioFisicos = ejercicioFisicos.Where(t => t.EjercicioFisicoID == id);
        }

        // Aplicar filtro por rango de fechas si se proporcionan
        if (BuscarInicio != null && BuscarFin != null)
        {
            ejercicioFisicos = ejercicioFisicos.Where(e => e.Inicio >= BuscarInicio && e.Inicio <= BuscarFin);
        }

        // Ordenar los ejercicios por la fecha de inicio
        ejercicioFisicos = ejercicioFisicos.OrderBy(e => e.Inicio);

        // Obtener la lista de tipos de ejercicios
        var tipoLugares = _context.Lugares.ToList();
        var tipoEjercicios = _context.TipoEjercicios.ToList();

        // Construir la lista de VistaEjercicioFisico para mostrar
        foreach (var ejercicioFisico in ejercicioFisicos)
        {
            var tipoLugar = tipoLugares.Single(t => t.LugarID == ejercicioFisico.LugarID);
            var tipoEjercicio = tipoEjercicios.Single(t => t.TipoEjercicioID == ejercicioFisico.TipoEjercicioID);

            var ejercicioFisicosMostrar = new VistaEjercicioFisico
            {
                EjercicioFisicoID = ejercicioFisico.EjercicioFisicoID,
                TipoEjercicioID = ejercicioFisico.TipoEjercicioID,
                LugarID =   ejercicioFisico.LugarID,
                NombreLugar = tipoLugar.Nombre,
                Descripcion = tipoEjercicio.Descripcion,
                InicioString = ejercicioFisico.Inicio.ToString("dd/MM/yyyy HH:mm"),
                FinString = ejercicioFisico.Fin.ToString("dd/MM/yyyy HH:mm"),
                IntervaloEjercicio = Convert.ToDecimal(ejercicioFisico.IntervaloEjercicio.TotalMinutes),
                Observaciones = ejercicioFisico.Observaciones
            };
            ejerciciosFisicosMostrar.Add(ejercicioFisicosMostrar);
        }
        return Json(ejerciciosFisicosMostrar);
    }


    public IActionResult InformeEventos()
    {
        return View();
    }

    public JsonResult ListadosInformeEventos(DateTime? BuscarInicioEvento, DateTime? BuscarFinEvento)
    {
        List<VistaEventoDeportivo> eventosDeportivosMostrar = new List<VistaEventoDeportivo>();
        var ejerciciosFisicos = _context.EjerciciosFisicos.Include(t => t.TipoEjercicios).Include(l => l.Lugar).Include(d => d.EventosDeportivos).ToList();

        if (BuscarInicioEvento != null && BuscarFinEvento != null)
        {
            ejerciciosFisicos= ejerciciosFisicos.Where(e => e.Inicio >= BuscarInicioEvento && e.Inicio <= BuscarFinEvento).ToList();
        }

        foreach ( var e in ejerciciosFisicos)
        {
            var eventoDeportivoMostrar = eventosDeportivosMostrar.Where(d => d.EventoDeportivoID == e.EventoDeportivoID).SingleOrDefault();
            if (eventoDeportivoMostrar == null)
            {
                eventoDeportivoMostrar = new VistaEventoDeportivo
                {
                    EventoDeportivoID = e.EventoDeportivoID,
                    Nombre = e.EventosDeportivos.Nombre,
                    ListadoLugares = new List<VistaLugar>()
                };
                eventosDeportivosMostrar.Add(eventoDeportivoMostrar);
            }

            var lugarMostrar = eventoDeportivoMostrar.ListadoLugares.Where(l => l.LugarID == e.LugarID).SingleOrDefault();
            if (lugarMostrar == null)
            {
                lugarMostrar = new VistaLugar
                {
                    LugarID = e.LugarID,
                    NombreLugar = e.Lugar.Nombre,
                    ListadoTipoEjercicios = new List<VistaTipoEjercicios>()
                };
                eventoDeportivoMostrar.ListadoLugares.Add(lugarMostrar);
            }

            var tipoEjercicioMostrar = lugarMostrar.ListadoTipoEjercicios.Where(t => t.TipoEjercicioID == e.TipoEjercicioID).SingleOrDefault();
            if (tipoEjercicioMostrar == null)
            {
                tipoEjercicioMostrar = new VistaTipoEjercicios
                {
                    TipoEjercicioID = e.TipoEjercicioID,
                    Descripcion = e.TipoEjercicios.Descripcion,
                    ListadoEjercicios = new List<VistaEjercicioFisico>()
                };
                lugarMostrar.ListadoTipoEjercicios.Add(tipoEjercicioMostrar);
            }

            var ejercicioFisicoMostrar = tipoEjercicioMostrar.ListadoEjercicios.Where(f => f.EjercicioFisicoID == e.EjercicioFisicoID).SingleOrDefault();
            if (ejercicioFisicoMostrar == null)
            {
                ejercicioFisicoMostrar = new VistaEjercicioFisico
                {
                    EjercicioFisicoID = e.EjercicioFisicoID,
                    InicioString = e.Inicio.ToString("dd/MM/yyyy HH:mm"),
                    FinString = e.Fin.ToString("dd/MM/yyyy HH:mm"),
                    IntervaloEjercicio = Convert.ToDecimal(e.IntervaloEjercicio.TotalMinutes),
                    EstadoEmocionalInicio = Enum.GetName(typeof(EstadoEmocional), e.EstadoEmocionalInicio),
                    EstadoEmocionalFin = Enum.GetName(typeof(EstadoEmocional), e.EstadoEmocionalFin),
                    Observaciones = e.Observaciones
                };
                tipoEjercicioMostrar.ListadoEjercicios.Add(ejercicioFisicoMostrar);
            }
        }
        
        return Json(eventosDeportivosMostrar);
    }



}


