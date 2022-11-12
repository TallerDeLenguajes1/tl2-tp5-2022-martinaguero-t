namespace TP4.ViewModels;
using System.ComponentModel.DataAnnotations;
public class PedidosCadetesViewModel {  

    private List<CadeteViewModel> cadetes;
    private int numero;
    public string? observaciones;
    private bool estaRealizado;
    private int idCadete;
    private int idCliente;

    public List<CadeteViewModel> Cadetes {get => cadetes; set => cadetes = value; }
    
    public int Numero { get => numero; set => numero = value; }

    [Required]
    public bool EstaRealizado { get => estaRealizado; set => estaRealizado = value; }

    [Display(Name = "Observaciones del pedido")]
    public string? Observaciones{ get => observaciones; set => observaciones = value; }

    [Display(Name = "Cadete")]
    [Required] 
    public int IdCadete { get => idCadete; set => idCadete = value; }

    [Display(Name = "Cliente")]
    [Required] 
    public int IdCliente { get => idCliente; set => idCliente = value; }
}

