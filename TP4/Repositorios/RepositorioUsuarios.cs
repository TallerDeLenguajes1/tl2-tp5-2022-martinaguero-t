using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using NLog;
using TP4.Models;

namespace TP4.Repositories;

public interface IRepositorioUsuarios
{
    //bool existeUsuario(string nombreUsuario, string contrasena);
}

public class RepositorioUsuarios : IRepositorioUsuarios
{
    private readonly string cadenaConexion;
    // readonly para que cadenaConexion sea inmutable
    private readonly IConfiguration _configuration;
    // para usar la cadena de conexión del JSON
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    public RepositorioUsuarios(IConfiguration configuration)
    {
        this._configuration = configuration;
        this.cadenaConexion = this._configuration.GetConnectionString("SQLite");
    }

    /*
    public bool existeUsuario(string nombreUsuario, string contrasena){

        // Que devuelva un usuario para tener todos los datos.
    }
    */
}
