using System.ComponentModel.DataAnnotations;

namespace TP4.ViewModels;

public class LoginUsuarioViewModel {
    private string username;
    private string contrasena;

    [Display(Name = "Nombre de usuario")]
    [Required(ErrorMessage = "Ingrese su nombre de usuario")]
    public string Username { get => username; set => username = value; }

    [Display(Name = "Contraseña")]
    [Required(ErrorMessage = "Ingrese su contraseña")]
    [DataType(DataType.Password)]
    public string Contrasena { get => contrasena; set => contrasena = value; }
}

