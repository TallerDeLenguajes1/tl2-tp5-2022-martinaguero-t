using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP4.Models;
using TP4.ViewModels;
using AutoMapper;

namespace TP4.Controllers;

public class CadetesController : Controller
{
    private readonly ILogger<CadetesController> _logger;
    private IMapper _mapper;
    private readonly IRepositorioCadetes _repCadetes;
    public CadetesController(ILogger<CadetesController> logger, IMapper mapper, IRepositorioCadetes repCadetes)
    {
        _logger = logger;
        _mapper = mapper;
        _repCadetes = repCadetes;
    }
    
    public IActionResult listarCadetes(){

        try
        {
            // método que traiga cadetes
            var cadetes = _repCadetes.obtenerCadetes();

            // mapear
            var cadetesViewModel = _mapper.Map<List<CadeteViewModel>>(cadetes);
            ListaCadeteViewModel listaCadetesViewModel = new ListaCadeteViewModel(cadetesViewModel);

            // tener la clase con la lista y los métodos
            return View(listaCadetesViewModel);
        }
        catch (System.Exception ex)
        {
            // Modificar listaCadetesViewModel para que tenga un atributo que corresponda al error?
            ViewBag.Error = ex.Message; 
            return View(new ListaCadeteViewModel(new List<CadeteViewModel>()));
        }
    }
    
    [HttpGet]   
    public IActionResult cargarCadete(){
        return View(new CadeteViewModel());
    }   

    [HttpPost]
    public IActionResult cargarCadetePost(CadeteViewModel cadeteViewModel){

        try{

            if(ModelState.IsValid){
                var cadete = _mapper.Map<Cadete>(cadeteViewModel);
                _repCadetes.agregarCadete(cadete);
                return RedirectToAction("listarCadetes");
            } 

            return View("cargarCadete",cadeteViewModel);
        }
        catch 
        {   
            // En caso de no poder cargar un cadete redirigo al listado de cadetes.
            return RedirectToAction("listarCadetes");
        }

    }

    public IActionResult modificarCadete(int id){
        
        try
        {
            
            var cadetes = _repCadetes.obtenerCadetes();
            Cadete? cadeteBuscado = cadetes.Find(cadete => cadete.ID == id);

            if(cadeteBuscado != null){
                var cadeteViewModel = _mapper.Map<CadeteViewModel>(cadeteBuscado);
                return View(cadeteViewModel);
            } else {
                return RedirectToAction("listarCadetes");
                // No se encontró el cadete buscado
            }

        }
        catch (System.Exception)
        {
            // Hubo un error al conectar con la base de datos/ obtener cadetes. Redirijo al listado.
            return RedirectToAction("listarCadetes");
        }
    }

    [HttpPost]
    public IActionResult modificarCadetePost(CadeteViewModel cadeteViewModel){

        try
        {
            
            if(ModelState.IsValid){
                var cadete = _mapper.Map<Cadete>(cadeteViewModel);
                _repCadetes.modificarCadete(cadete);
                return RedirectToAction("listarCadetes");
            } 
            
            return View("modificarCadete",cadeteViewModel);
            // El estado del modelo recibido no es válido, redirige al formulario

        }
        catch (System.Exception)
        {
            // Hubo un error y no se pudo modificar la información del cadete.
            return RedirectToAction("listarCadetes");
        }

    }

    [HttpGet]
    public IActionResult eliminarCadete(int ID){

        try
        {
            _repCadetes.eliminarCadete(ID);
            return RedirectToAction("listarCadetes");
        }
        catch (System.Exception)
        {   
            // Hubo un error y no se pudo eliminar el cadete.
            return RedirectToAction("listarCadetes");
        }
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
