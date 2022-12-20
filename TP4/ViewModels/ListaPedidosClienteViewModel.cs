namespace TP4.ViewModels;
using System.ComponentModel.DataAnnotations;

public class ListaPedidosClienteViewModel {  
    public string nombreCliente;
    public List<PedidoViewModel> pedidosCliente;

    public ListaPedidosClienteViewModel(string nombreCliente, List<PedidoViewModel> pedidosCliente)
    {
        this.nombreCliente = nombreCliente;
        this.pedidosCliente = pedidosCliente;
    }

    public ListaPedidosClienteViewModel(){
        this.nombreCliente = "";
        this.pedidosCliente = new List<PedidoViewModel>();
    }

    public int cantidadPedidosSinRecibir(){
        return pedidosCliente.Count(pedido => !pedido.EstaRealizado);
    }
}

