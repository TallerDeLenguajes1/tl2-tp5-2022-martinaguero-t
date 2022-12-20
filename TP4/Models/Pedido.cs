using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP4.Models
{
    public class Pedido {
        private int numero;
        private string? observaciones;
        private bool estaRealizado;
        private int idCadete;
        private string? nombreCadete;
        private int idCliente;
        private string? nombreCliente;

        public Pedido(){
            this.Numero = 0;
            this.EstaRealizado = false;
            this.IdCadete = 1;
            this.IdCliente = 1;
            this.NombreCadete = "";
            this.NombreCliente = "";
        }

        public Pedido(int numero, string observaciones, bool estaRealizado, int idCadete, int idCliente, string nombreCadete = "", string nombreCliente = ""){
            this.Numero = numero;
            this.Observaciones = observaciones;
            this.EstaRealizado = estaRealizado;
            this.IdCadete = idCadete;
            this.IdCliente = idCliente;
            this.NombreCadete = nombreCadete;
            this.NombreCliente = nombreCliente;
        }

        public int Numero { get => numero; set => numero = value; }
        public string? Observaciones { get => observaciones; set => observaciones = value; }
        public int IdCadete { get => idCadete; set => idCadete = value; }
        public int IdCliente { get => idCliente; set => idCliente = value; }
        public bool EstaRealizado { get => estaRealizado; set => estaRealizado = value; }
        public string NombreCadete { get => nombreCadete; set => nombreCadete = value; }
        public string NombreCliente { get => nombreCliente; set => nombreCliente = value; }
    }
}