using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using NLog;

namespace TP4.Models
{
    public interface IRepositorioClientes
    {
        void agregarCliente(Cliente cliente);
        void eliminarCliente(int idCliente);
        List<Cliente> obtenerClientes();
        void modificarCliente(Cliente clienteAModificar);
    }

    public class RepositorioClientes : IRepositorioClientes
    {
        private readonly string cadenaConexion;
        // readonly para que cadenaConexion sea inmutable
        private readonly IConfiguration _configuration;
        // para usar la cadena de conexión del JSON
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public RepositorioClientes(IConfiguration configuration)
        {
            this._configuration = configuration;
            //this.cadenaConexion = "Data Source=DB/PedidosDB.db;Version=3;";
            this.cadenaConexion = this._configuration.GetConnectionString("SQLite");
            // solo puedo asignar un valor a un atributo readonly en el constructor o en la misma declaración
            // inyección de dependencia (cadenaConexion)
        }

        public List<Cliente> obtenerClientes()
        {

            // Para trabajar con DB
            try
            {

                List<Cliente> clientes = new List<Cliente>();

                string consulta = "SELECT * FROM Cliente";

                using (SQLiteConnection conexion = new SQLiteConnection(cadenaConexion))
                {

                    conexion.Open();

                    SQLiteCommand comando = new SQLiteCommand(consulta, conexion);

                    using (SQLiteDataReader reader = comando.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            clientes.Add(new Cliente(reader.GetInt32(0),reader[1].ToString(),reader[2].ToString(),reader[3].ToString(),reader[4].ToString()));
                        }

                    }

                    conexion.Close();
                }

                return clientes;

            }
            catch (SQLiteException exDB)
            {
                logger.Debug("Hubo un error, no se pudo obtener información de los clientes. Excepción: " + exDB.ToString());
                throw new Exception("Hubo un error, no se pudo obtener información de los c.", exDB);
            }
            catch (Exception ex)
            {
                logger.Debug("Hubo un error al conectar a la base de datos (para lectura de datos de clientes). Excepción: " + ex.ToString());
                throw new Exception("Hubo un error al conectar a la base de datos.", ex);
            }
            finally
            {
                // Cerrar la conexion??
            }


        }

        public void agregarCliente(Cliente cliente)
        {
            try
            {

                string consulta = $"INSERT INTO Cliente (nombre, direccion, telefono, referencia_direccion) VALUES (@nombre,@direccion,@telefono,@refdir)";

                using (SQLiteConnection conexion = new SQLiteConnection(cadenaConexion))
                {

                    conexion.Open();

                    SQLiteCommand comando = new SQLiteCommand(consulta, conexion);

                    // Agregando parámetros a la consulta
                    comando.Parameters.Add(new SQLiteParameter("@nombre", cliente.Nombre));
                    comando.Parameters.Add(new SQLiteParameter("@direccion", cliente.Direccion));
                    comando.Parameters.Add(new SQLiteParameter("@telefono", cliente.Telefono));
                    comando.Parameters.Add(new SQLiteParameter("@refdir", cliente.DatosReferenciaDireccion));


                    comando.ExecuteNonQuery();

                    conexion.Close();
                }

                logger.Info($"Se agregó el cliente de ID {cliente.ID}");

            }
            catch (SQLiteException exDB)
            {
                logger.Debug("Hubo un error, no se pudo cargar la información de un cliente. Excepción: " + exDB.ToString());
                throw new Exception("Hubo un error, no se pudo cargar la información del cliente.", exDB);
            }
            catch (Exception ex)
            {
                logger.Debug("Hubo un error al conectar a la base de datos (para cargar datos de un cliente). Excepción: " + ex.ToString());
                throw new Exception("Hubo un error al conectar a la base de datos.", ex);
            }
            finally
            {
                // Cerrar la conexion??
            }
        }


        public void eliminarCliente(int idCliente)
        {
            try
            {

                string consulta = $"DELETE FROM Cliente WHERE id = @idCliente";

                using (SQLiteConnection conexion = new SQLiteConnection(cadenaConexion))
                {

                    conexion.Open();

                    SQLiteCommand comando = new SQLiteCommand(consulta, conexion);

                    comando.Parameters.Add(new SQLiteParameter("@idCliente", idCliente));

                    comando.ExecuteNonQuery();

                    conexion.Close();
                }

                logger.Info($"Se eliminó el cliente de ID {idCliente}");

            }
            catch (SQLiteException exDB)
            {
                logger.Debug($"Hubo un error, no se pudo eliminar el registro de un cliente de ID {idCliente}. Excepción: " + exDB.ToString());
                throw new Exception("Hubo un error, no se pudo eliminar el registro del cliente.", exDB);
            }
            catch (Exception ex)
            {
                logger.Debug($"Hubo un error al conectar a la base de datos (para eliminar datos de un cliente de ID {idCliente}). Excepción: " + ex.ToString());
                throw new Exception("Hubo un error al conectar a la base de datos.", ex);
            }
            finally
            {
                // Cerrar la conexion??
            }
        }

        public void modificarCliente(Cliente clienteAModificar)
        {

            try
            {
                string consulta = $"UPDATE Cliente SET nombre = @nombre, direccion = @direccion, telefono = @telefono, referencia_direccion = @refdir  WHERE id = @idCliente";

                using (SQLiteConnection conexion = new SQLiteConnection(cadenaConexion))
                {

                    conexion.Open();

                    SQLiteCommand comando = new SQLiteCommand(consulta, conexion);

                    comando.Parameters.Add(new SQLiteParameter("@idCliente", clienteAModificar.ID));
                    comando.Parameters.Add(new SQLiteParameter("@nombre", clienteAModificar.Nombre));
                    comando.Parameters.Add(new SQLiteParameter("@direccion", clienteAModificar.Direccion));
                    comando.Parameters.Add(new SQLiteParameter("@telefono", clienteAModificar.Telefono));
                    comando.Parameters.Add(new SQLiteParameter("@refdir", clienteAModificar.DatosReferenciaDireccion));

                    comando.ExecuteNonQuery();

                    conexion.Close();
                }

                logger.Info($"Se modificó la información del cliente de ID {clienteAModificar.ID}");

            }
            catch (SQLiteException exDB)
            {
                logger.Debug($"Hubo un error, no se pudo actualizar la información de un cliente de ID {clienteAModificar.ID}. Excepción: " + exDB.ToString());
                throw new Exception("Hubo un error, no se pudo actualizar el registro del cliente.", exDB);
            }
            catch (Exception ex)
            {
                logger.Debug($"Hubo un error al conectar a la base de datos (para actualizar datos de un cliente de ID {clienteAModificar.ID}). Excepción: " + ex.ToString());
                throw new Exception("Hubo un error al conectar a la base de datos.", ex);
            }
            finally
            {
                // Cerrar la conexion??
            }
        }



    }
}