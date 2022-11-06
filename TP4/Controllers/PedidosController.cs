using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP4.Models;
using TP4.ViewModels;
using AutoMapper;

namespace TP4.Controllers
{
    public class PedidosController : Controller
    {
        private readonly ILogger<PedidosController> _logger;
        private IMapper _mapper;
        private static RepositorioPedidos repPedidos = new RepositorioPedidos();

        public PedidosController(ILogger<PedidosController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public IActionResult listarPedidos(){
            var pedidos = repPedidos.getPedidos();
            var pedidosViewModel = _mapper.Map<List<PedidoViewModel>>(pedidos);
            return View(pedidosViewModel);
        }


        [HttpGet]   
        public IActionResult crearPedido(){ 
            RepositorioCadetes infoCadetes = new RepositorioCadetes();
            if(infoCadetes.getCadetes().Any()){
                return View(new PedidoViewModel());
            } else {
                return RedirectToAction("listarPedidos");
            }
        }   

        [HttpPost]
        public IActionResult crearPedidoPost(PedidoViewModel pedidoViewModel){

            if(ModelState.IsValid){
                var pedido = _mapper.Map<Pedido>(pedidoViewModel);
                repPedidos.addPedido(pedido);
                return RedirectToAction("listarPedidos");
            } 

            return View("crearPedido", pedidoViewModel);

        }

        [HttpGet]
        public IActionResult eliminarPedido(int numPedido){
            repPedidos.eliminarPedido(numPedido);
            return RedirectToAction("listarPedidos");
        }

        public IActionResult modificarPedido(int numPedido){

            var pedidos = repPedidos.getPedidos();
            Pedido? pedido = pedidos.Find(pedido => pedido.Numero == numPedido);

            if(pedido != null){
                var pedidoViewModel = _mapper.Map<PedidoViewModel>(pedido);
                return View(pedidoViewModel);

            } else{ 
                return RedirectToAction("listarPedidos");
            }
        }

        [HttpPost]
        public IActionResult modificarPedidoPost(PedidoViewModel pedidoViewModel){

            if(ModelState.IsValid){
                var pedido = _mapper.Map<Pedido>(pedidoViewModel);
                repPedidos.modificarPedido(pedido);
                return RedirectToAction("listarPedidos");
            }

            return View("modificarPedido",pedidoViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}