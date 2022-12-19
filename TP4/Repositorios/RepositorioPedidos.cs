using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using NLog;
using TP4.Models;

namespace TP4.Repositories;
public interface IRepositorioPedidos
{
    void agregarPedido(Pedido pedido);
    void eliminarPedido(int numPedido);
    List<Pedido> obtenerPedidos();
    void modificarPedido(Pedido pedidoAModificar);
    List<Pedido> obtenerPedidosCadete(int idCadete);
    List<Pedido> obtenerPedidosCliente(int idCliente);
    Pedido? buscarPedidoPorNumero(int numPedido);

}

public class RepositorioPedidos : IRepositorioPedidos
{
    private readonly string cadenaConexion;
    // readonly para que cadenaConexion sea inmutable
    private readonly IConfiguration _configuration;
    // para usar la cadena de conexión del JSON
    private readonly ILogger<RepositorioPedidos> _logger;
    public RepositorioPedidos(IConfiguration configuration, ILogger<RepositorioPedidos> logger)
    {
        this._configuration = configuration;
        this.cadenaConexion = this._configuration.GetConnectionString("SQLite");
        // inyección de dependencia (cadenaConexion)
        this._logger = logger;
        // inyección de dependencia (NLog Logger)
    }

    public List<Pedido> obtenerPedidos()
    {

        try
        {
            List<Pedido> pedidos = new List<Pedido>();

            string consulta = "SELECT * FROM Pedido";

            using (SQLiteConnection conexion = new SQLiteConnection(cadenaConexion))
            {

                conexion.Open();

                SQLiteCommand comando = new SQLiteCommand(consulta, conexion);

                using (SQLiteDataReader reader = comando.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        pedidos.Add(
                        new Pedido(reader.GetInt32(0), reader[1].ToString(), reader.GetBoolean(2), reader.GetInt32(3), reader.GetInt32(4)));
                    }

                }

                conexion.Close();
            }

            return pedidos;

        }
        catch (SQLiteException exDB)
        {
            _logger.LogDebug("Hubo un error, no se pudo obtener información de los pedidos. Excepción: " + exDB.ToString());
            throw new Exception("Hubo un error, no se pudo obtener información de los pedidos.", exDB);
        }
        catch (Exception ex)
        {
            _logger.LogDebug("Hubo un error al conectar a la base de datos (para lectura de datos de pedidos). Excepción: " + ex.ToString());
            throw new Exception("Hubo un error al conectar a la base de datos.", ex);
        }

    }
    
    public Pedido? buscarPedidoPorNumero(int numPedido){

        try
        {
            Pedido? pedidoBuscado = null;

            string consulta = "SELECT * FROM Pedido WHERE numero = @numPedido";

            using (SQLiteConnection conexion = new SQLiteConnection(cadenaConexion))
            {

                conexion.Open();

                SQLiteCommand comando = new SQLiteCommand(consulta, conexion);

                comando.Parameters.Add(new SQLiteParameter("@numPedido", numPedido));

                using (SQLiteDataReader reader = comando.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        pedidoBuscado = new Pedido(reader.GetInt32(0), reader[1].ToString(), reader.GetBoolean(2), reader.GetInt32(3), reader.GetInt32(4));
                    }

                }

                conexion.Close();
            }

            return pedidoBuscado;

        }
        catch (SQLiteException exDB)
        {
            _logger.LogDebug($"Hubo un error, no se pudo obtener información del pedido de número {numPedido}. Excepción: " + exDB.ToString());
            throw new Exception($"Hubo un error, no se pudo obtener información de un pedido de número {numPedido}.", exDB);
        }
        catch (Exception ex)
        {
            _logger.LogDebug("Hubo un error al conectar a la base de datos (para lectura de datos de un pedido). Excepción: " + ex.ToString());
            throw new Exception("Hubo un error al conectar a la base de datos.", ex);
        }

    }

    public List<Pedido> obtenerPedidosCliente(int idCliente)
    {

        try
        {
            List<Pedido> pedidos = new List<Pedido>();

            string consulta = "SELECT * FROM Pedido WHERE id_cliente = @idCliente";

            using (SQLiteConnection conexion = new SQLiteConnection(cadenaConexion))
            {

                conexion.Open();

                SQLiteCommand comando = new SQLiteCommand(consulta, conexion);
                comando.Parameters.Add(new SQLiteParameter("@idCliente", idCliente));

                using (SQLiteDataReader reader = comando.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        pedidos.Add(
                        new Pedido(reader.GetInt32(0), reader[1].ToString(), reader.GetBoolean(2), reader.GetInt32(3), reader.GetInt32(4))
                        );
                    }

                }

                conexion.Close();
            }

            return pedidos;

        }
        catch (SQLiteException exDB)
        {
            _logger.LogDebug($"Hubo un error, no se pudo obtener información de los pedidos del cliente de ID {idCliente}. Excepción: " + exDB.ToString());
            throw new Exception($"Hubo un error, no se pudo obtener información de los pedidos del cliente de ID {idCliente}.", exDB);
        }
        catch (Exception ex)
        {
            _logger.LogDebug($"Hubo un error al conectar a la base de datos (para lectura de datos de pedidos del cliente de ID {idCliente}). Excepción: " + ex.ToString());
            throw new Exception("Hubo un error al conectar a la base de datos.", ex);
        }

    }

    public List<Pedido> obtenerPedidosCadete(int idCadete)
    {

        try
        {
            List<Pedido> pedidos = new List<Pedido>();

            string consulta = "SELECT * FROM Pedido WHERE id_cadete = @idCadete";

            using (SQLiteConnection conexion = new SQLiteConnection(cadenaConexion))
            {

                conexion.Open();

                SQLiteCommand comando = new SQLiteCommand(consulta, conexion);
                comando.Parameters.Add(new SQLiteParameter("@idCadete", idCadete));

                using (SQLiteDataReader reader = comando.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        pedidos.Add(
                        new Pedido(reader.GetInt32(0), reader[1].ToString(), reader.GetBoolean(2), reader.GetInt32(3), reader.GetInt32(4))
                        );
                    }

                }

                conexion.Close();
            }

            return pedidos;

        }
        catch (SQLiteException exDB)
        {
            _logger.LogDebug($"Hubo un error, no se pudo obtener información de los pedidos del cadete de ID {idCadete}. Excepción: " + exDB.ToString());
            throw new Exception($"Hubo un error, no se pudo obtener información de los pedidos del cadete de ID {idCadete}.", exDB);
        }
        catch (Exception ex)
        {
            _logger.LogDebug($"Hubo un error al conectar a la base de datos (para lectura de datos de pedidos del cadete de ID {idCadete}). Excepción: " + ex.ToString());
            throw new Exception("Hubo un error al conectar a la base de datos.", ex);
        }

    }

    public void agregarPedido(Pedido pedido)
    {
        try
        {

            string consulta = $"INSERT INTO Pedido (observaciones, esta_realizado, id_cadete, id_cliente) VALUES (@obs, @estado, @idCadete, @idCliente)";

            using (SQLiteConnection conexion = new SQLiteConnection(cadenaConexion))
            {

                conexion.Open();

                SQLiteCommand comando = new SQLiteCommand(consulta, conexion);

                // Agregando parámetros a la consulta
                comando.Parameters.Add(new SQLiteParameter("@obs", pedido.Observaciones));
                comando.Parameters.Add(new SQLiteParameter("@estado", pedido.EstaRealizado));
                comando.Parameters.Add(new SQLiteParameter("@idCadete", pedido.IdCadete));
                comando.Parameters.Add(new SQLiteParameter("@idCliente", pedido.IdCliente));

                comando.ExecuteNonQuery();

                conexion.Close();
            }

            _logger.LogInformation($"Se agregó un pedido.ID del cliente: {pedido.IdCliente}");

        }
        catch (SQLiteException exDB)
        {
            _logger.LogDebug("Hubo un error, no se pudo cargar la información de un pedido. Excepción: " + exDB.ToString());
            throw new Exception("Hubo un error, no se pudo cargar la información del pedido.", exDB);
        }
        catch (Exception ex)
        {
            _logger.LogDebug("Hubo un error al conectar a la base de datos (para cargar datos de un pedido). Excepción: " + ex.ToString());
            throw new Exception("Hubo un error al conectar a la base de datos.", ex);
        }
        
    }


    public void eliminarPedido(int numPedido)
    {
        try
        {

            string consulta = $"DELETE FROM Pedido WHERE numero = @numPedido";

            using (SQLiteConnection conexion = new SQLiteConnection(cadenaConexion))
            {
                conexion.Open();

                SQLiteCommand comando = new SQLiteCommand(consulta, conexion);

                comando.Parameters.Add(new SQLiteParameter("@numPedido", numPedido));

                comando.ExecuteNonQuery();

                conexion.Close();
            }

            _logger.LogInformation($"Se eliminó el pedido de número {numPedido}");

        }
        catch (SQLiteException exDB)
        {
            _logger.LogDebug($"Hubo un error, no se pudo eliminar el registro de un pedido de número {numPedido}. Excepción: " + exDB.ToString());
            throw new Exception("Hubo un error, no se pudo eliminar el registro del pedido.", exDB);
        }
        catch (Exception ex)
        {
            _logger.LogDebug($"Hubo un error al conectar a la base de datos (para eliminar datos de un pedido de numero {numPedido}). Excepción: " + ex.ToString());
            throw new Exception("Hubo un error al conectar a la base de datos.", ex);
        }

    }

    public void modificarPedido(Pedido pedidoAModificar)
    {

        try
        {
            string consulta = $"UPDATE Pedido SET observaciones = @obs, esta_realizado = @estado, id_cadete = @idCadete, id_cliente = @idCliente WHERE numero = @numero";

            using (SQLiteConnection conexion = new SQLiteConnection(cadenaConexion))
            {

                conexion.Open();

                SQLiteCommand comando = new SQLiteCommand(consulta, conexion);

                comando.Parameters.Add(new SQLiteParameter("@numero", pedidoAModificar.Numero));
                comando.Parameters.Add(new SQLiteParameter("@obs", pedidoAModificar.Observaciones));
                comando.Parameters.Add(new SQLiteParameter("@estado", pedidoAModificar.EstaRealizado));
                comando.Parameters.Add(new SQLiteParameter("@idCadete", pedidoAModificar.IdCadete));
                comando.Parameters.Add(new SQLiteParameter("@idCliente", pedidoAModificar.IdCliente));

                comando.ExecuteNonQuery();

                conexion.Close();
            }

            _logger.LogInformation($"Se modificó la información del pedido de numero {pedidoAModificar.Numero}");

        }
        catch (SQLiteException exDB)
        {
            _logger.LogDebug($"Hubo un error, no se pudo actualizar la información de un pedido de número {pedidoAModificar.Numero}. Excepción: " + exDB.ToString());
            throw new Exception("Hubo un error, no se pudo actualizar el registro del pedido.", exDB);
        }
        catch (Exception ex)
        {
            _logger.LogDebug($"Hubo un error al conectar a la base de datos (para actualizar datos de un pedido de número {pedidoAModificar.Numero}). Excepción: " + ex.ToString());
            throw new Exception("Hubo un error al conectar a la base de datos.", ex);
        }

    }

}