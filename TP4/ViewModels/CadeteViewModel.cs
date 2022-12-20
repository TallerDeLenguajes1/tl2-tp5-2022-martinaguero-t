namespace TP4.ViewModels;
using System.ComponentModel.DataAnnotations;
public class CadeteViewModel {
    private int id;
    public string nombre;
    private string direccion;
    private string telefono;
    
    public int ID { get => id; set => id = value; }

    [Display(Name = "Dirección del cadete")]
    [Required(ErrorMessage = "Ingrese una dirección.")]
    public string Direccion { get => direccion; set => direccion = value; }

    [Display(Name = "Teléfono del cadete")]
    [Required(ErrorMessage = "Ingrese un número de teléfono.")] [Phone]
    public string Telefono { get => telefono; set => telefono = value; }

    [Display(Name = "Nombre del cadete")]
    [RegularExpression(@"\b([A-ZÀ-ÿ][-,a-z. ']+[ ]*)+", ErrorMessage = "Ingrese un nombre válido")]
    [Required(ErrorMessage = "Ingrese un nombre.")]
    public string Nombre { get => nombre; set => nombre = value; }

}

