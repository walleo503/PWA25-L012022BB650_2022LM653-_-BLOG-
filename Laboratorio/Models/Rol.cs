namespace Laboratorio.Models
{
    public class Rol
    {
        public int RolId { get; set; }
        public string Nombre { get; set; }

        public ICollection<Usuarios> Usuarios { get; set; } = new List<Usuarios>();
    }
}
