namespace TP4.ViewModels;
using System.ComponentModel.DataAnnotations;
public class CadeteViewModel {
    private int id;
    public string nombre;
    private string direccion;
    private string telefono;
    
    public int ID { get => id; set => id = value; }

    [Display(Name = "Dirección del cadete")]
    [Required]
    public string Direccion { get => direccion; set => direccion = value; }

    [Display(Name = "Teléfono del cadete")]
    [Required] [Phone]
    public string Telefono { get => telefono; set => telefono = value; }

    [Display(Name = "Nombre del cadete")]
    [Required]
    public string Nombre { get => nombre; set => nombre = value; }

}

