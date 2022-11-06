namespace TP4.ViewModels;
using System.ComponentModel.DataAnnotations;
public class PedidoViewModel {
    private int numero;
    public string observaciones;
    private string estaRealizado;
    private string idCadete;
    
    public int Numero { get => numero; set => numero = value; }

    public string EstaRealizado { get => estaRealizado; set => estaRealizado = value; }

    [Display(Name = "Observaciones del pedido")]
    [Required]
    public string  Observaciones{ get => observaciones; set => observaciones = value; }

    [Display(Name = "Cadete")]
    [Required] 
    public string IdCadete { get => idCadete; set => idCadete = value; }


}

