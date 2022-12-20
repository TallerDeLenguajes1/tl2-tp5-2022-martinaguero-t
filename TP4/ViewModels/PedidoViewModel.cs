namespace TP4.ViewModels;
using System.ComponentModel.DataAnnotations;
public class PedidoViewModel {
    private int numero;
    public string? observaciones;
    private bool estaRealizado;
    private int idCadete;
    private int idCliente;

    public int Numero { get => numero; set => numero = value; }
    
    [Required(ErrorMessage = "Seleccione un estado válido.")]
    public bool EstaRealizado { get => estaRealizado; set => estaRealizado = value; }

    [Display(Name = "Observaciones del pedido")]
    public string? Observaciones{ get => observaciones; set => observaciones = value; }

    [Display(Name = "Cadete")]
    [Required(ErrorMessage = "Seleccione un cadete.")] 
    public int IdCadete { get => idCadete; set => idCadete = value; }

    [Display(Name = "ID del Cliente")]
    [Required(ErrorMessage = "Seleccione un cliente.")] 
    public int IdCliente { get => idCliente; set => idCliente = value; }

}

