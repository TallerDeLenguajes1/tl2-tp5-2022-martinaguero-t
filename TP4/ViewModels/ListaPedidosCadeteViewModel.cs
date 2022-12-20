namespace TP4.ViewModels;
using System.ComponentModel.DataAnnotations;

public class ListaPedidosCadeteViewModel {  
    public string nombreCadete;
    public List<PedidoViewModel> pedidosCadete;
    public ListaPedidosCadeteViewModel(string nombreCadete, List<PedidoViewModel> pedidosCadete)
    {
        this.nombreCadete = nombreCadete;
        this.pedidosCadete = pedidosCadete;
    }

    public ListaPedidosCadeteViewModel(){
        this.nombreCadete = "";
        this.pedidosCadete = new List<PedidoViewModel>();
    }

    public int cantidadPedidosEntregados(){
        return pedidosCadete.Count(pedido => pedido.EstaRealizado);
    }

    public double jornalAPagar(){
        return cantidadPedidosEntregados()*300;
    }
}

