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
}

