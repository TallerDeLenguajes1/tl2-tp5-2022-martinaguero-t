using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using NLog;
using TP4.Models;

namespace TP4.Repositories;
public interface IRepositorioCadetes
{
    void agregarCadete(Cadete cadete);
    void eliminarCadete(int idCadete);
    List<Cadete> obtenerCadetes();
    void modificarCadete(Cadete cadeteAModificar);
    Cadete? buscarCadetePorID(int idCadete);
}

public class RepositorioCadetes : IRepositorioCadetes
{
    private readonly string cadenaConexion;
    // readonly para que cadenaConexion sea inmutable
    private readonly IConfiguration _configuration;
    // para usar la cadena de conexión del JSON
    public RepositorioCadetes(IConfiguration configuration)
    {
        this._configuration = configuration;
        //this.cadenaConexion = "Data Source=DB/PedidosDB.db;Version=3;";
        this.cadenaConexion = this._configuration.GetConnectionString("SQLite");
        // solo puedo asignar un valor a un atributo readonly en el constructor o en la misma declaración
        // inyección de dependencia (cadenaConexion)
    }

    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    // Se elimina la lista de cadetes y la asignación de autonuméricos, trabajo con la DB.
    public List<Cadete> obtenerCadetes()
    {

        // Para trabajar con DB
        try
        {

            List<Cadete> cadetes = new List<Cadete>();

            string consulta = "SELECT * FROM Cadete";

            using (SQLiteConnection conexion = new SQLiteConnection(cadenaConexion))
            {

                conexion.Open();

                SQLiteCommand comando = new SQLiteCommand(consulta, conexion);

                using (SQLiteDataReader reader = comando.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        cadetes.Add(new Cadete(reader.GetInt32(0), reader[1].ToString(), reader[2].ToString(), reader[3].ToString()));
                    }

                }

                conexion.Close();
            }

            return cadetes;

        }
        catch (SQLiteException exDB)
        {
            logger.Debug("Hubo un error, no se pudo obtener información de los cadetes. Excepción: " + exDB.ToString());
            throw new Exception("Hubo un error, no se pudo obtener información de los cadetes.", exDB);
        }
        catch (Exception ex)
        {
            logger.Debug("Hubo un error al conectar a la base de datos (para lectura de datos de cadetes). Excepción: " + ex.ToString());
            throw new Exception("Hubo un error al conectar a la base de datos.", ex);
        }

    }

    public Cadete? buscarCadetePorID(int idCadete){

        try
        {
            Cadete? cadeteBuscado = null;

            string consulta = "SELECT * FROM Cadete WHERE id = @idCadete";

            using (SQLiteConnection conexion = new SQLiteConnection(cadenaConexion))
            {

                conexion.Open();

                SQLiteCommand comando = new SQLiteCommand(consulta, conexion);

                comando.Parameters.Add(new SQLiteParameter("@idCadete", idCadete));

                using (SQLiteDataReader reader = comando.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        cadeteBuscado = new Cadete(reader.GetInt32(0), reader[1].ToString(), reader[2].ToString(), reader[3].ToString());
                    }

                }

                conexion.Close();
            }

            return cadeteBuscado;

        }
        catch (SQLiteException exDB)
        {
            logger.Debug($"Hubo un error, no se pudo obtener información del cadete de ID {idCadete}. Excepción: " + exDB.ToString());
            throw new Exception($"Hubo un error, no se pudo obtener información de un cadete de ID {idCadete}.", exDB);
        }
        catch (Exception ex)
        {
            logger.Debug("Hubo un error al conectar a la base de datos (para lectura de datos de un cadete). Excepción: " + ex.ToString());
            throw new Exception("Hubo un error al conectar a la base de datos.", ex);
        }

    }
    public void agregarCadete(Cadete cadete)
    {
        try
        {

            string consulta = $"INSERT INTO Cadete (nombre, direccion, telefono) VALUES (@nombre,@direccion,@telefono)";

            using (SQLiteConnection conexion = new SQLiteConnection(cadenaConexion))
            {

                conexion.Open();

                SQLiteCommand comando = new SQLiteCommand(consulta, conexion);

                // Agregando parámetros a la consulta
                comando.Parameters.Add(new SQLiteParameter("@nombre", cadete.Nombre));
                comando.Parameters.Add(new SQLiteParameter("@direccion", cadete.Direccion));
                comando.Parameters.Add(new SQLiteParameter("@telefono", cadete.Telefono));

                comando.ExecuteNonQuery();

                conexion.Close();
            }

            logger.Info($"Se agregó el cadete de ID {cadete.ID}");

        }
        catch (SQLiteException exDB)
        {
            logger.Debug("Hubo un error, no se pudo cargar la información de un cadete. Excepción: " + exDB.ToString());
            throw new Exception("Hubo un error, no se pudo cargar la información del cadete.", exDB);
        }
        catch (Exception ex)
        {
            logger.Debug("Hubo un error al conectar a la base de datos (para cargar datos de un cadete). Excepción: " + ex.ToString());
            throw new Exception("Hubo un error al conectar a la base de datos.", ex);
        }

    }


    public void eliminarCadete(int idCadete)
    {
        try
        {

            string consulta = $"DELETE FROM Cadete WHERE id = @idCadete";

            using (SQLiteConnection conexion = new SQLiteConnection(cadenaConexion))
            {

                conexion.Open();

                SQLiteCommand comando = new SQLiteCommand(consulta, conexion);

                comando.Parameters.Add(new SQLiteParameter("@idCadete", idCadete));

                comando.ExecuteNonQuery();

                conexion.Close();
            }

            logger.Info($"Se eliminó el cadete de ID {idCadete}");

        }
        catch (SQLiteException exDB)
        {
            logger.Debug($"Hubo un error, no se pudo eliminar el registro de un cadete de ID {idCadete}. Excepción: " + exDB.ToString());
            throw new Exception("Hubo un error, no se pudo eliminar el registro del cadete.", exDB);
        }
        catch (Exception ex)
        {
            logger.Debug($"Hubo un error al conectar a la base de datos (para eliminar datos de un cadete de ID {idCadete}). Excepción: " + ex.ToString());
            throw new Exception("Hubo un error al conectar a la base de datos.", ex);
        }

    }

    public void modificarCadete(Cadete cadeteAModificar)
    {

        try
        {
            string consulta = $"UPDATE Cadete SET nombre = @nombre, direccion = @direccion, telefono = @telefono  WHERE id = @idCadete";

            using (SQLiteConnection conexion = new SQLiteConnection(cadenaConexion))
            {

                conexion.Open();

                SQLiteCommand comando = new SQLiteCommand(consulta, conexion);

                comando.Parameters.Add(new SQLiteParameter("@idCadete", cadeteAModificar.ID));
                comando.Parameters.Add(new SQLiteParameter("@nombre", cadeteAModificar.Nombre));
                comando.Parameters.Add(new SQLiteParameter("@direccion", cadeteAModificar.Direccion));
                comando.Parameters.Add(new SQLiteParameter("@telefono", cadeteAModificar.Telefono));

                comando.ExecuteNonQuery();

                conexion.Close();
            }

            logger.Info($"Se modificó la información del cadete de ID {cadeteAModificar.ID}");

        }
        catch (SQLiteException exDB)
        {
            logger.Debug($"Hubo un error, no se pudo actualizar la información de un cadete de ID {cadeteAModificar.ID}. Excepción: " + exDB.ToString());
            throw new Exception("Hubo un error, no se pudo actualizar el registro del cadete.", exDB);
        }
        catch (Exception ex)
        {
            logger.Debug($"Hubo un error al conectar a la base de datos (para actualizar datos de un cadete de ID {cadeteAModificar.ID}). Excepción: " + ex.ToString());
            throw new Exception("Hubo un error al conectar a la base de datos.", ex);
        }

    }
}