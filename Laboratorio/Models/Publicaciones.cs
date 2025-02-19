using System.ComponentModel.DataAnnotations;

namespace L01_2022BB650_2022LM653.Models
{
    public class Publicaciones
    {
        [Key]
        public int publicacionId { get; set; }
        public string titulo {  get; set; }
        public string descripcion { get; set; }
        public int? usuarioId { get; set; }
    }
}
