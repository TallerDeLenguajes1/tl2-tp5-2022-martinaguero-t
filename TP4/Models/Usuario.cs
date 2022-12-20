namespace TP4.Models
{
    public class Usuario {
        
        private int id;
        private string username;
        private string rol;     

        // No se almacena la contraseña ni el nombre ya que no es necesario.
        public Usuario(){
            id = -1; // id inválido, no existe usuario
            username = "";
            rol = "";
        }

        public Usuario(int id, string username, string rol)
        {
            this.id = id;
            this.username = username;
            this.rol = rol;
        }

        public int Id { get => id; set => id = value; }
        public string Rol { get => rol; set => rol = value; }
        public string Username { get => username; set => username = value; }
    }

}