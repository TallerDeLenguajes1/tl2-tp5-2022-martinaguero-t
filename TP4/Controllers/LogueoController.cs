using System.Net;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP4.Models;
using TP4.ViewModels;
using TP4.Repositories;

// Para AutoMapper
using AutoMapper;
// Para session
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

namespace TP4.Controllers;

public class LogueoController : Controller
{
    private readonly ILogger<LogueoController> _logger;
    private readonly IRepositorioUsuarios _repUsuarios;

    public LogueoController(ILogger<LogueoController> logger, IRepositorioUsuarios repUsuarios)
    {
        _logger = logger;
        _repUsuarios = repUsuarios;
    }

    public IActionResult iniciarSesion()
    {
        // Si el usuario ya está logueado, no se lo deja acceder al formulario de logueo.
        if(HttpContext.Session.GetString("nombreUsuario") == null){
            return View(new LoginUsuarioViewModel());
        } else {
            return RedirectToAction("Index","Home");
        }
    }

    [HttpPost]
    public IActionResult iniciarSesionPost(LoginUsuarioViewModel loginUsuarioViewModel){

        try
        {
            if(ModelState.IsValid){
                Usuario usuario = _repUsuarios.buscarUsuario(loginUsuarioViewModel.Username,loginUsuarioViewModel.Contrasena);

                if(usuario.Id != -1){
                    HttpContext.Session.SetString("nombreUsuario",usuario.Username);
                    HttpContext.Session.SetString("rolUsuario",usuario.Rol);
                    return RedirectToAction("Index","Home");
                } else {
                    ViewBag.NoEncontrado = "No se encontró el usuario ingresado";
                    return View("iniciarSesion");
                    // No se encontró el usuario.
                }

            } 

            return RedirectToAction("Home","Index");
            // Si no es válido el estado del modelo, se redirige al inicio.
        }
        catch (System.Exception)
        {
            return RedirectToAction("Home","Index");
        }

       
    }

    public IActionResult cerrarSesion(){
        HttpContext.Session.Clear();
        return RedirectToAction("Index","Home");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}
