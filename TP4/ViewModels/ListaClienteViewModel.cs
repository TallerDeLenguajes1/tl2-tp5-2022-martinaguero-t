namespace TP4.ViewModels;
public class ListaClienteViewModel {
    
    public List<ClienteViewModel> listaClientesViewModel;

    public ListaClienteViewModel(List<ClienteViewModel> listaClientesViewModel){
        this.listaClientesViewModel = listaClientesViewModel;
    }

    public int contarCadetes(){
        return listaClientesViewModel.Count();
    }

}

