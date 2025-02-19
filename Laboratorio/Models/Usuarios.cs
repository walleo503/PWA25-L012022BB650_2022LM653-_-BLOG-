using System.ComponentModel.DataAnnotations;

namespace L01_2022BB650_2022LM653.Models
{

    public class Usuarios
    {
        public int UsuarioId { get; set; }
        public int RolId { get; set; }
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public Roles Rol { get; set; }
    }
}
