using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace TP4.Models
{
    public class RepositorioPedidos
    {
        private static int autonumerico = 0;
        private List<Pedido> pedidos = new List<Pedido>();
        public List<Pedido> getPedidos(){
            return pedidos; 
        }

        public void addPedido(Pedido pedido){
            asignarNumero(pedido);
            this.pedidos.Add(pedido);
        }

        public void eliminarPedido(int numPedido){
            Pedido? pedidoBuscado = pedidos.Find(Pedido => Pedido.Numero == numPedido);
            if(pedidoBuscado != null) pedidos.Remove(pedidoBuscado);
        }

        public void modificarPedido(Pedido pedidoAModificar){

            Pedido? pedidoBuscado = pedidos.Find(pedido => pedido.Numero == pedidoAModificar.Numero);

            if(pedidoBuscado != null){
                pedidoBuscado.Observaciones = pedidoAModificar.Observaciones;
                pedidoBuscado.EstaRealizado = pedidoAModificar.EstaRealizado;
                pedidoBuscado.IdCadete = pedidoAModificar.IdCadete;
                pedidoBuscado.IdCliente = pedidoAModificar.IdCliente;
            }
    
        }   

        public void asignarNumero(Pedido pedido){
            autonumerico++;
            pedido.Numero = autonumerico;
        }
    }
}