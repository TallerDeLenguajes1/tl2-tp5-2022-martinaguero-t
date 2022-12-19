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
                var cadetes = _repCadetes.obtenerCadetes();
                var clientes = _repClientes.obtenerClientes();

                if(cadetes.Any() && clientes.Any()){

                    PedidosCadetesClientesViewModel pedidosCadetesClientes = new PedidosCadetesClientesViewModel();
                    pedidosCadetesClientes.Cadetes = _mapper.Map<List<CadeteViewModel>>(cadetes);
                    pedidosCadetesClientes.Clientes = _mapper.Map<List<ClienteViewModel>>(clientes);
                    
                    return View(pedidosCadetesClientes);

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
        public IActionResult crearPedidoPost(PedidoViewModel pedidoViewModel){

            try
            {
                if(ModelState.IsValid){
                    var pedido = _mapper.Map<Pedido>(pedidoViewModel);
                    _repPedidos.agregarPedido(pedido);
                    return RedirectToAction("listarPedidos");
                } 

                return View("crearPedido", pedidoViewModel);
                
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
                var pedido = _repPedidos.buscarPedidoPorNumero(numPedido);

                if(pedido != null){

                    PedidosCadetesViewModel pedidosCadetes = _mapper.Map<PedidosCadetesViewModel>(pedido);
                    pedidosCadetes.Cadetes = _mapper.Map<List<CadeteViewModel>>(_repCadetes.obtenerCadetes());

                    return View(pedidosCadetes);

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
        public IActionResult modificarPedidoPost(PedidoViewModel pedidoViewModel){
            
            try
            {
                if(ModelState.IsValid){
                    var pedido = _mapper.Map<Pedido>(pedidoViewModel);
                    _repPedidos.modificarPedido(pedido);
                    return RedirectToAction("listarPedidos");
                }

                return View("modificarPedido",pedidoViewModel);
            }
            catch (System.Exception)
            {
                return RedirectToAction("listarPedidos");
            }
        }

        [HttpGet]
        public IActionResult listarPedidosCliente(int idCliente){
            try
            {
                var pedidos = _repPedidos.obtenerPedidosCliente(idCliente);
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
        public IActionResult listarPedidosCadete(int idCadete){
            try
            {
                var pedidos = _repPedidos.obtenerPedidosCadete(idCadete);
                var pedidosViewModel = _mapper.Map<List<PedidoViewModel>>(pedidos);
                return View(pedidosViewModel);
            }
            catch (System.Exception ex)
            {
                ViewBag.Error = ex.Message; 
                return View(new List<PedidoViewModel>());
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}