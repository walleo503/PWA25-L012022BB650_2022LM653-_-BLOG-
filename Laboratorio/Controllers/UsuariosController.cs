using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2022BB650_2022LM653.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace L01_2022BB650_2022LM653.Properties
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController1 : ControllerBase
    {
        private readonly DatosContext _context;

        public UsuariosController1(DatosContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los usuarios.
        /// </summary>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            //var usuarios = _context.Usuarios.Include(u => u.Rol).ToList();
            List<Usuarios> listadoUsuarios = (from e in _context.Usuarios
                                              select e).ToList();

            if (listadoUsuarios.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoUsuarios);
        }

        /// <summary>
        /// Agrega un nuevo usuario.
        /// </summary>
        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarUsuario([FromBody] Usuarios usuario)
        {
            try
            {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Actualiza un usuario existente.
        /// </summary>
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarUsuario(int id, [FromBody] Usuarios usuarioModificar)
        {
            var usuarioActual = _context.Usuarios.FirstOrDefault(u => u.UsuarioId == id);

            if (usuarioActual == null)
            {
                return NotFound();
            }

            usuarioActual.NombreUsuario = usuarioModificar.NombreUsuario;
            usuarioActual.Clave = usuarioModificar.Clave;
            usuarioActual.Nombre = usuarioModificar.Nombre;
            usuarioActual.Apellido = usuarioModificar.Apellido;
            usuarioActual.RolId = usuarioModificar.RolId;

            _context.Entry(usuarioActual).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(usuarioModificar);
        }

        /// <summary>
        /// Elimina un usuario por su ID.
        /// </summary>
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarUsuario(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.UsuarioId == id);

            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();

            return Ok(usuario);
        }

        /// <summary>
        /// Obtiene usuarios filtrados por nombre y apellido.
        /// </summary>
        [HttpGet]
        [Route("filtrar/nombre-apellido")]
        public IActionResult FiltrarPorNombreApellido([FromQuery] string nombre, [FromQuery] string apellido)
        {
            var usuarios = _context.Usuarios
                .Where(u => u.Nombre.Contains(nombre) || u.Apellido.Contains(apellido))
                .ToList();

            if (usuarios.Count == 0)
            {
                return NotFound();
            }

            return Ok(usuarios);
        }

        /// <summary>
        /// Obtiene usuarios filtrados por rol.
        /// </summary>
        [HttpGet]
        [Route("filtrar/rol/{rolId}")]
        public IActionResult FiltrarPorRol(int rolId)
        {
            var usuarios = _context.Usuarios
                .Where(u => u.RolId == rolId)
                .Include(u => u.Rol)
                .ToList();

            if (usuarios.Count == 0)
            {
                return NotFound();
            }

            return Ok(usuarios);
        }
    }
}