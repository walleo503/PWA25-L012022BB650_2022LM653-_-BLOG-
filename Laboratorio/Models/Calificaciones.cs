using System.ComponentModel.DataAnnotations;

namespace L01_2022BB650_2022LM653.Models
{
    public class Calificaciones
    {
        [Key]   
        public int calificacionesId { get; set; }
         public int? publicacionesId { get; set; }
        public int? usuariId { get; set; }
         public int calificaciones {  get; set; }
    }
}
