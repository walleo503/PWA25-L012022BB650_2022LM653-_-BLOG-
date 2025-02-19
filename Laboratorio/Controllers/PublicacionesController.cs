
using Microsoft.AspNetCore.Mvc;
using L01_2022BB650_2022LM653.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2022BB650_2022LM653.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacionesController : ControllerBase
    {
        private readonly DatosContext _PublicacionesContexto;
        public PublicacionesController (DatosContext PublicacionesContexto)
        {
            _PublicacionesContexto = PublicacionesContexto;
        }

        //Enpoint que retorna el listado de todas las publicaciones existentes
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Publicaciones> listadoPublicaciones = (from e in _PublicacionesContexto.Publicaciones
                                        select e).ToList();

            if (listadoPublicaciones.Count() == 0)
            {
                return NotFound();
            }

            return Ok(listadoPublicaciones);
        }

        
        //  Enpoint para agregar una nueva publicacion.
        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarPublicacion([FromBody] Publicaciones Publicaciones)
        {
            try
            {
                _PublicacionesContexto.Publicaciones.Add(Publicaciones);
                _PublicacionesContexto.SaveChanges();
                return Ok(Publicaciones);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        //Enpoint para modificar publicacion.
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarPublicacion(int id, [FromBody] Publicaciones PublicacionModificado)
        {
            //Escojer un registro existente de la base de datos.
            Publicaciones? PublicacionActual = (from e in _PublicacionesContexto.Publicaciones
                                  where e.publicacionId == id
                                  select e).FirstOrDefault();
            //Verificacion de la existencia del registro.
            if (PublicacionActual == null)
            { return NotFound(); }

            //Campos modificables
            PublicacionActual.titulo = PublicacionModificado.titulo;
            PublicacionActual.descripcion = PublicacionModificado.descripcion;

            //Marcado de registro modificado.
            _PublicacionesContexto.Entry(PublicacionActual).State = EntityState.Modified;
            _PublicacionesContexto.SaveChanges();
            return Ok(PublicacionModificado);
        }


        /// Buscador de publicaciones por el  Id de usuario.
        /// <param name="usuarioId"></param>
        [HttpGet]
        [Route("BuscarPorUsuarioId/{usuarioId}")]
        public IActionResult BuscarPublicacionesPorUsuarioId(int usuarioId)
        {
            // Verificación de usuario válido
            var usuario = _PublicacionesContexto.Usuarios
                            .Where(u => u.usuarioId== usuarioId)
                            .Select(u => new { u.usuarioId, u.nombreUsuario })
                            .FirstOrDefault();

            if (usuario == null)
            {
                return NotFound($"No se encontró un usuario con el ID {usuarioId}.");
            }

            // Obtener publicaciones del usuario filtrando por ID
            var publicacionesEncontradas = _PublicacionesContexto.Publicaciones
                                        .Where(p => p.usuarioId == usuarioId)
                                        .Select(p => new
                                        {
                                            p.publicacionId,
                                            p.titulo,
                                            p.descripcion,
                                            NombreUsuario = usuario.nombreUsuario
                                        })
                                        .ToList();

            // Validar si se encontraron publicaciones
            if (!publicacionesEncontradas.Any())
            {
                return NotFound($"No se encontraron publicaciones para el usuario '{usuario.nombreUsuario}'.");
            }

            return Ok(publicacionesEncontradas);
        }


    }

}
