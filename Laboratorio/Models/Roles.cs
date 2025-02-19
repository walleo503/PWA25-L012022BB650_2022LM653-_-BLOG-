using System.ComponentModel.DataAnnotations;

namespace L01_2022BB650_2022LM653.Models
{
    public class Roles
    {
        [Key]
        public int rolId { get; set; }
        public string rol {  get; set; }

    }
}
