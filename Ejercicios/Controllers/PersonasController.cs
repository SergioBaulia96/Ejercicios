using Ejercicios.Models;
using Ejercicios.Data;

using Microsoft.AspNetCore.Mvc;

namespace Ejercicios.Controllers;

public class PersonasController : Controller
{
    private ApplicationDbContext _context;

    public PersonasController(ApplicationDbContext context)
    {
        _context = context;
    }

    public ActionResult Personas()
    {
        return View();
    }

    public JsonResult ListadoPersonas(int? id)
    {
    
        var tipoDePersonas = _context.Personas.ToList();

        //LUEGO PREGUNTAMOS SI EL USUARIO INGRESO UN ID
        //QUIERE DECIR QUE QUIERE UN EJERCICIO EN PARTICULAR
        if (id != null)
        {
            //FILTRAMOS EL LISTADO COMPLETO DE EJERCICIOS POR EL EJERCICIO QUE COINCIDA CON ESE ID
            tipoDePersonas = tipoDePersonas.Where(t => t.PersonaID == id).ToList();
        }

        return Json(tipoDePersonas);
    }

    public JsonResult GuardarLugares(int lugarID, string nombre)
    {

        string resultado = "";

        if (!String.IsNullOrEmpty(nombre))
        {
            nombre = nombre.ToUpper();
            
            if (lugarID == 0)
            {
                var existeLugar = _context.Lugares.Where(t => t.Nombre == nombre).Count();
                if (existeLugar == 0)
                {
                    //4- GUARDAR EL TIPO DE EJERCICIO
                    var lugar = new Lugar
                    {
                        Nombre = nombre
                    };
                    _context.Add(lugar);
                    _context.SaveChanges();
                }
                else
                {
                    resultado = "YA EXISTE UN REGISTRO EL MISMO NOMBRE";
                }
            }
            else
            {
                //QUIERE DECIR QUE VAMOS A EDITAR EL REGISTRO
                var lugarEditar = _context.Lugares.Where(t => t.LugarID == lugarID).SingleOrDefault();
                if (lugarEditar != null)
                {
                    //BUSCAMOS EN LA TABLA SI EXISTE UN REGISTRO CON EL MISMO NOMBRE PERO QUE EL ID SEA DISTINTO AL QUE ESTAMOS EDITANDO
                    var existeTipoEjercicio = _context.Lugares.Where(t => t.Nombre == nombre && t.LugarID != lugarID).Count();
                    if (existeTipoEjercicio == 0)
                    {
                        //QUIERE DECIR QUE EL ELEMENTO EXISTE Y ES CORRECTO ENTONCES CONTINUAMOS CON EL EDITAR
                        lugarEditar.Nombre = nombre;
                        _context.SaveChanges();
                    }
                    else
                    {
                        resultado = "YA EXISTE UN REGISTRO CON EL MISMO NOMBRE";
                    }
                }
            }
        }
        else
        {
            resultado = "DEBE INGRESAR UN NOMBRE.";
        }

        return Json(resultado);
    }

    public JsonResult EliminarLugares(int lugarID)
    {
        var lugar = _context.Lugares.Find(lugarID);
        _context.Remove(lugar);
        _context.SaveChanges();

        return Json(true);
    }

    
}