namespace TP4.ViewModels;
using System.ComponentModel.DataAnnotations;
public class ListaCadeteViewModel {
    
    public List<CadeteViewModel> listaCadetesViewModel;

    public ListaCadeteViewModel(List<CadeteViewModel> listaCadetesViewModel){
        this.listaCadetesViewModel = listaCadetesViewModel;
    }

    public int contarCadetes(){
        return listaCadetesViewModel.Count();
    }

}

