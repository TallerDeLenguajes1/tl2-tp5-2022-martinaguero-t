using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP4.Models;
using TP4.ViewModels;
using AutoMapper;

namespace TP4.Controllers;

public class ClientesController : Controller
{
    private readonly ILogger<ClientesController> _logger;
    private IMapper _mapper;
    private readonly IRepositorioClientes _repClientes;
    public ClientesController(ILogger<ClientesController> logger, IMapper mapper, IRepositorioClientes repClientes)
    {
        _logger = logger;
        _mapper = mapper;
        _repClientes = repClientes;
    }
    
    public IActionResult listarClientes(){

        try
        {
            var clientes = _repClientes.obtenerClientes();

            // mapear
            var clientesViewModel = _mapper.Map<List<ClienteViewModel>>(clientes);
            ListaClienteViewModel listaClientesViewModel = new ListaClienteViewModel(clientesViewModel);

            // tener la clase con la lista y los métodos
            return View(listaClientesViewModel);
        }
        catch (System.Exception ex)
        {
            ViewBag.Error = ex.Message; 
            return View(new ListaClienteViewModel(new List<ClienteViewModel>()));
        }
    }
    
    [HttpGet]   
    public IActionResult cargarCliente(){
        return View(new ClienteViewModel());
    }   

    [HttpPost]
    public IActionResult cargarClientePost(ClienteViewModel clienteViewModel){

        try{

            if(ModelState.IsValid){
                var cliente = _mapper.Map<Cliente>(clienteViewModel);
                _repClientes.agregarCliente(cliente);
                return RedirectToAction("listarClientes");
            } 

            return View("cargarCliente",clienteViewModel);
        }
        catch 
        {   
            return RedirectToAction("listarClientes");
        }

    }

    public IActionResult modificarCliente(int id){
        
        try
        {
            
            var clientes = _repClientes.obtenerClientes();
            Cliente? clienteBuscado = clientes.Find(cliente => cliente.ID == id);

            if(clienteBuscado != null){
                var clienteViewModel = _mapper.Map<ClienteViewModel>(clienteBuscado);
                return View(clienteViewModel);
            } else {
                return RedirectToAction("listarClientes");
            }

        }
        catch (System.Exception)
        {
            return RedirectToAction("listarClientes");
        }
    }

    [HttpPost]
    public IActionResult modificarClientePost(ClienteViewModel clienteViewModel){

        try
        {
            
            if(ModelState.IsValid){
                var cliente = _mapper.Map<Cliente>(clienteViewModel);
                _repClientes.modificarCliente(cliente);
                return RedirectToAction("listarClientes");
            } 
            
            return View("modificarCliente",clienteViewModel);
            // El estado del modelo recibido no es válido, redirige al formulario

        }
        catch (System.Exception)
        {
            return RedirectToAction("listarClientes");
        }

    }

    [HttpGet]
    public IActionResult eliminarCliente(int ID){

        try
        {
            _repClientes.eliminarCliente(ID);
            return RedirectToAction("listarClientes");
        }
        catch (System.Exception)
        {   
            return RedirectToAction("listarClientes");
        }
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
