namespace TP4.ViewModels;
using System.ComponentModel.DataAnnotations;
public class ClienteViewModel {
    private int id;
    private string nombre;
    private string direccion;
    private string telefono;
    private string? datosReferenciaDireccion;
    
    public int ID { get => id; set => id = value; }

    [Display(Name = "Dirección del cliente")]
    [Required(ErrorMessage = "Ingrese una dirección.")]
    public string Direccion { get => direccion; set => direccion = value; }

    [Display(Name = "Teléfono del cliente")]
    [Required(ErrorMessage = "Ingrese un número de teléfono.")] [Phone]
    public string Telefono { get => telefono; set => telefono = value; }

    [Display(Name = "Nombre del cliente")]
    [RegularExpression(@"\b([A-ZÀ-ÿ][-,a-z. ']+[ ]*)+", ErrorMessage = "Ingrese un nombre válido.")]
    [Required(ErrorMessage = "Ingrese un nombre.")]
    public string Nombre { get => nombre; set => nombre = value; }

    [Display(Name = "Datos de referencia de la dirección del cliente")]
    public string? DatosReferenciaDireccion { get => datosReferenciaDireccion; set => datosReferenciaDireccion = value; }
}

