using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP4.Models;
using TP4.ViewModels;
using TP4.Repositories;
using AutoMapper;

namespace TP4.Controllers
{
    public class PedidosController : Controller
    {
        private readonly ILogger<PedidosController> _logger;
        private IMapper _mapper;
        private IRepositorioPedidos _repPedidos;
        private IRepositorioCadetes _repCadetes;
        private IRepositorioClientes _repClientes;

        public PedidosController(ILogger<PedidosController> logger, IMapper mapper, IRepositorioCadetes repCadetes, IRepositorioPedidos repPedidos, IRepositorioClientes repClientes)
        {
            _logger = logger;
            _mapper = mapper;
            _repCadetes = repCadetes;
            _repClientes = repClientes;
            _repPedidos = repPedidos;
        }

        public IActionResult listarPedidos(){
            try
            {
                if(HttpContext.Session.GetString("rolUsuario") == null) return RedirectToAction("Index","Home");

                var pedidos = _repPedidos.obtenerPedidos();
                var pedidosViewModel = _mapper.Map<List<PedidoViewModel>>(pedidos);
                return View(pedidosViewModel);
            }
            catch (System.Exception ex)
            {
                ViewBag.Error = ex.Message; 
                return View(new List<PedidoViewModel>());
            }
        }


        [HttpGet]   
        public IActionResult crearPedido(){ 
            
            try
            {
                string rolUsuario = HttpContext.Session.GetString("rolUsuario");
                if(rolUsuario == null || rolUsuario == "Encargado") return RedirectToAction("Index","Home");

                var cadetes = _repCadetes.obtenerCadetes();
                var clientes = _repClientes.obtenerClientes();

                if(cadetes.Any() && clientes.Any()){

                    PedidosCadetesClientesViewModel pedidosCadetesClientesViewModel = new PedidosCadetesClientesViewModel();
                    pedidosCadetesClientesViewModel.Cadetes = _mapper.Map<List<CadeteViewModel>>(cadetes);
                    pedidosCadetesClientesViewModel.Clientes = _mapper.Map<List<ClienteViewModel>>(clientes);
                    
                    return View(pedidosCadetesClientesViewModel);

                } else {
                    return RedirectToAction("listarPedidos");
                }
            }
            catch (System.Exception)
            {
                return RedirectToAction("listarPedidos"); 
            }
        }   

        [HttpPost]
        public IActionResult crearPedidoPost(PedidosCadetesClientesViewModel pedidosCadetesClientesViewModel){

            try
            {
                string rolUsuario = HttpContext.Session.GetString("rolUsuario");
                if(rolUsuario == null || rolUsuario == "Encargado") return RedirectToAction("Index","Home");

                if(ModelState.IsValid){
                    var pedido = _mapper.Map<Pedido>(pedidosCadetesClientesViewModel);
                    _repPedidos.agregarPedido(pedido);
                    return RedirectToAction("listarPedidos");
                } 

                return RedirectToAction("listarPedidos"); 
                // Si el modelo no es válido, redirige al listado de pedidos.
                
            }
            catch (System.Exception)
            {
                return RedirectToAction("listarPedidos"); 
            }

        }

        [HttpGet]
        public IActionResult eliminarPedido(int numPedido){

            try
            {
                string rolUsuario = HttpContext.Session.GetString("rolUsuario");
                if(rolUsuario == null || rolUsuario == "Encargado") return RedirectToAction("Index","Home");

                _repPedidos.eliminarPedido(numPedido);
                return RedirectToAction("listarPedidos");
            }
            catch (System.Exception)
            {
                return RedirectToAction("listarPedidos");
            }
        }

        public IActionResult modificarPedido(int numPedido){

            try
            {
                string rolUsuario = HttpContext.Session.GetString("rolUsuario");
                if(rolUsuario == null || rolUsuario == "Encargado") return RedirectToAction("Index","Home");

                var pedido = _repPedidos.buscarPedidoPorNumero(numPedido);

                if(pedido != null){

                    PedidosCadetesViewModel pedidosCadetesViewModel = _mapper.Map<PedidosCadetesViewModel>(pedido);
                    pedidosCadetesViewModel.Cadetes = _mapper.Map<List<CadeteViewModel>>(_repCadetes.obtenerCadetes());

                    return View(pedidosCadetesViewModel);

                } else{ 
                    return RedirectToAction("listarPedidos");
                }
            }
            catch (System.Exception)
            {
                return RedirectToAction("listarPedidos");
            }
        }

        [HttpPost]
        public IActionResult modificarPedidoPost(PedidosCadetesViewModel pedidosCadetesViewModel){
            
            try
            {
                string rolUsuario = HttpContext.Session.GetString("rolUsuario");
                if(rolUsuario == null || rolUsuario == "Encargado") return RedirectToAction("Index","Home");

                if(ModelState.IsValid){
                    var pedido = _mapper.Map<Pedido>(pedidosCadetesViewModel);
                    _repPedidos.modificarPedido(pedido);
                    return RedirectToAction("listarPedidos");
                }

                return RedirectToAction("listarPedidos"); 
                // Si el modelo no es válido, redirige al listado de pedidos.
            }
            catch (System.Exception)
            {
                return RedirectToAction("listarPedidos");
            }
        }

        [HttpGet]
        public IActionResult listarPedidosCliente(int idCliente, string nombreCliente){
            try
            {
                string rolUsuario = HttpContext.Session.GetString("rolUsuario");
                if(rolUsuario == null) return RedirectToAction("Index","Home");

                var pedidos = _repPedidos.obtenerPedidosCliente(idCliente);

                
                var pedidosViewModel = _mapper.Map<List<PedidoViewModel>>(pedidos);

                ListaPedidosClienteViewModel listarPedidosClienteViewModel = new ListaPedidosClienteViewModel(nombreCliente,pedidosViewModel);
                return View(listarPedidosClienteViewModel);
            }
            catch (System.Exception ex)
            {
                ViewBag.Error = ex.Message; 
                return View(new ListaPedidosClienteViewModel());
            }
        }

        [HttpGet]
        public IActionResult listarPedidosCadete(int idCadete, string nombreCadete){
            try
            {
                string rolUsuario = HttpContext.Session.GetString("rolUsuario");
                if(rolUsuario == null) return RedirectToAction("Index","Home");

                var pedidos = _repPedidos.obtenerPedidosCadete(idCadete);
                var pedidosViewModel = _mapper.Map<List<PedidoViewModel>>(pedidos);

                ListaPedidosCadeteViewModel listarPedidosCadeteViewModel = new ListaPedidosCadeteViewModel(nombreCadete,pedidosViewModel);

                return View(listarPedidosCadeteViewModel);
            }
            catch (System.Exception ex)
            {
                ViewBag.Error = ex.Message; 
                return View(new ListaPedidosCadeteViewModel());
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}