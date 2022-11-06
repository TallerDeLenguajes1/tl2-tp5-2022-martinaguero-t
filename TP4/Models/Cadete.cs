namespace TP4.Models
{
    public class Cadete {
        private int id;
        public string nombre;
        private string direccion;
        private string telefono;

        public int ID { get => id; set => id = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Nombre { get => nombre; set => nombre = value; }

        public Cadete(){
            this.id = 0;
            this.direccion = "";
            this.telefono = "";
            this.nombre = "";
        }
        
        public Cadete(string nombre, string direccion, string telefono){
            this.id = 0;
            this.nombre = nombre;
            this.direccion = direccion;
            this.telefono = telefono;
        }
        
    }

}