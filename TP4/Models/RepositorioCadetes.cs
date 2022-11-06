using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace TP4.Models
{
    public class RepositorioCadetes
    {
        private static int autonumerico = 0;
        private List<Cadete> cadetes = new List<Cadete>();
        public List<Cadete> getCadetes(){

            /*  Para trabajar con DB
            string cadenaConexion = @"Data Source=db\Cadeteria.db;Version=3;";

            using(SQLiteConnection conexion = new SQLiteConnection(cadenaConexion)){
                conexion.Open();

                conexion.Close();
            }
            */
            return cadetes; 

           
        }

        public void addCadete(Cadete cadete){
            asignarID(cadete);
            this.cadetes.Add(cadete);
        }

        public void eliminarCadete(int idCadete){
            Cadete? cadeteBuscado = cadetes.Find(cadete => cadete.ID == idCadete);
            if(cadeteBuscado != null) cadetes.Remove(cadeteBuscado);
        }

        public void modificarCadete(Cadete cadeteAModificar){

            Cadete? cadeteBuscado = cadetes.Find(cadete => cadete.ID == cadeteAModificar.ID);

            if(cadeteBuscado != null){
                cadeteBuscado.Nombre = cadeteAModificar.Nombre;
                cadeteBuscado.Telefono = cadeteAModificar.Telefono;
                cadeteBuscado.Direccion = cadeteAModificar.Direccion;
            }
            // Si bien se espera recibir un cadete ya existente, se controla que se trate de un cadete que ya esté en la lista (para el TP5)
        }   

        public void asignarID(Cadete cadete){
            autonumerico++;
            cadete.ID = autonumerico;
        }
    }
}