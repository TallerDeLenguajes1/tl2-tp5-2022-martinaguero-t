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

    private static RepositorioCadetes repCadetes = new RepositorioCadetes();
    public CadetesController(ILogger<CadetesController> logger, IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
    }
    
    public IActionResult listarCadetes(){

        try
        {
            // método que traiga cadetes
            var cadetes = repCadetes.getCadetes();
            // mapear
            var cadetesViewModel = _mapper.Map<List<CadeteViewModel>>(cadetes);
            ListaCadeteViewModel listaCadetesViewModel = new ListaCadeteViewModel(cadetesViewModel);

            // tener la clase con la lista y los métodos
            return View(listaCadetesViewModel);
        }
        catch (System.Exception)
        {
            // Modificar listaCadetesViewModel para que tenga un atributo que corresponda al error?
            return View();
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
                repCadetes.addCadete(cadete);
                return RedirectToAction("listarCadetes");
            } 

            return View("cargarCadete",cadeteViewModel);
        }
        catch 
        {
            // ¿Qué hacer?
            return View("listarCadetes");
        }

    }

    public IActionResult modificarCadete(int id){
        
        var cadetes = repCadetes.getCadetes();
        Cadete? cadeteBuscado = cadetes.Find(cadete => cadete.ID == id);

        if(cadeteBuscado != null){
            var cadeteViewModel = _mapper.Map<CadeteViewModel>(cadeteBuscado);
            return View(cadeteViewModel);
        } else {
            return RedirectToAction("listarCadetes");
        }
    }

    [HttpPost]
    public IActionResult modificarCadetePost(CadeteViewModel cadeteViewModel){

        if(ModelState.IsValid){
            var cadete = _mapper.Map<Cadete>(cadeteViewModel);
            repCadetes.modificarCadete(cadete);
            return RedirectToAction("listarCadetes");
        } 
        
        return View("modificarCadete",cadeteViewModel);

    }

    [HttpGet]
    public IActionResult eliminarCadete(int ID){

        try
        {
            repCadetes.eliminarCadete(ID);
            return RedirectToAction("listarCadetes");
        }
        catch (System.Exception)
        {
            // Qué hacer acá?
            return RedirectToAction("listarCadetes");
        }
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
