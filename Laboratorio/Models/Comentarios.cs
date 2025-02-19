using System.ComponentModel.DataAnnotations;

namespace L01_2022BB650_2022LM653.Models
{
    public class Comentarios
    {
        [Key]
        public int comentarioId { get; set; }
        public int? publicacionesId { get; set; }
        public string comentario {  get; set; }
        public int? usuarioId { get; set; }
    }
}
