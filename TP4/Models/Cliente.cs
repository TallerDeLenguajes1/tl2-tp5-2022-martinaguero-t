using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP4.Models
{
    public class Cliente {
        private int id;
        private string nombre;
        private string direccion;
        private string telefono;
        private string? datosReferenciaDireccion;
        public Cliente(int ID, string nombre, string direccion, string telefono, string datosReferenciaDireccion){
            this.id = ID;
            this.nombre = nombre;
            this.direccion = direccion;
            this.telefono = telefono;
            this.datosReferenciaDireccion = datosReferenciaDireccion;
        }

        public int ID { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string? DatosReferenciaDireccion { get => datosReferenciaDireccion; set => datosReferenciaDireccion = value; }
    }
}