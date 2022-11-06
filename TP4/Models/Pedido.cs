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
        private int idCliente;

        public Pedido(){
            this.Numero = 0;
            this.EstaRealizado = false;
            this.IdCadete = 1;
            this.IdCliente = 1;
        }
        public Pedido(int numero, string observaciones, bool estaRealizado, int idCadete, int idCliente){
            this.Numero = numero;
            this.Observaciones = observaciones;
            this.EstaRealizado = estaRealizado;
            this.IdCadete = idCadete;
            this.IdCliente = idCliente;
        }

        public int Numero { get => numero; set => numero = value; }
        public string? Observaciones { get => observaciones; set => observaciones = value; }
        public int IdCadete { get => idCadete; set => idCadete = value; }
        public int IdCliente { get => idCliente; set => idCliente = value; }
        public bool EstaRealizado { get => estaRealizado; set => estaRealizado = value; }

    }
}