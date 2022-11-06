using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP4.Models
{
    public class Cliente {
        private int ID;
        private string nombre;
        private string direccion;
        private string telefono;
        private string datosReferenciaDireccion;
        public Cliente(int ID, string nombre, string direccion, string telefono, string datosReferenciaDireccion){
            this.ID = ID;
            this.nombre = nombre;
            this.direccion = direccion;
            this.telefono = telefono;
            this.datosReferenciaDireccion = datosReferenciaDireccion;
        }

    }
}