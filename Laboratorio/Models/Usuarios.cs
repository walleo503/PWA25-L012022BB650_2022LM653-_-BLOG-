using L01_2022BB650_2022LM653.Models;
using System.ComponentModel.DataAnnotations;

namespace Laboratorio.Models
{

    public class Usuarios
    {
        [Key]
        public int usuarioId { get; set; }
        public int rolId { get; set; }
        public string nombreUsuario { get; set; }
        public string clave { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }

        public Roles Rol { get; set;  }
    }
}
