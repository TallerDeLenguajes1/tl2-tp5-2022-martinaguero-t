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
    Usuario buscarUsuario(string nombreUsuario, string contrasena);
}

public class RepositorioUsuarios : IRepositorioUsuarios
{
    private readonly string cadenaConexion;
    // readonly para que cadenaConexion sea inmutable
    private readonly IConfiguration _configuration;
    // para usar la cadena de conexión del JSON
    private readonly ILogger<RepositorioUsuarios> _logger;
    public RepositorioUsuarios(IConfiguration configuration, ILogger<RepositorioUsuarios> logger)
    {
        this._configuration = configuration;
        this.cadenaConexion = this._configuration.GetConnectionString("SQLite");
        // inyección de dependencia (cadenaConexion)
        this._logger = logger;
        // inyección de dependencia (NLog Logger)
    }

    public Usuario buscarUsuario(string nombreUsuario, string contrasena){

        try
        {
            Usuario usuarioBuscado = new Usuario();
            // Notar que si no se encuentra un usuario coincidente, entonces el id se establece en -1.

            string consulta = "SELECT id, usuario, rol FROM Usuario WHERE usuario = @nombreUsuario AND contrasena = @contrasena";
            // No traigo la contraseña ya que no es necesario.
            
            using (SQLiteConnection conexion = new SQLiteConnection(cadenaConexion))
            {

                conexion.Open();

                SQLiteCommand comando = new SQLiteCommand(consulta, conexion);

                comando.Parameters.Add(new SQLiteParameter("@nombreUsuario", nombreUsuario));
                comando.Parameters.Add(new SQLiteParameter("@contrasena", contrasena));


                using (SQLiteDataReader reader = comando.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        // Se supone que no habrán dos usuarios con el mismo userame, así que esta consulta siempre debe devolver un registro.
                        usuarioBuscado = new Usuario(reader.GetInt32(0),reader[1].ToString(),reader[2].ToString());
                    }

                }

                conexion.Close();
            }

            return usuarioBuscado;

        }
        catch (SQLiteException exDB)
        {
            _logger.LogDebug($"Hubo un error, no se pudo validar la información ingresada de usuario. Excepción: " + exDB.ToString());
            throw new Exception($"Hubo un error, no se pudo validar la información ingresada de usuario.", exDB);
        }
        catch (Exception ex)
        {
            _logger.LogDebug("Hubo un error al conectar a la base de datos (para validar información de un usuario). Excepción: " + ex.ToString());
            throw new Exception("Hubo un error al conectar a la base de datos.", ex);
        }

    }
}
