using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2022BB650_2022LM653.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2022BB650_2022LM653.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacionesController : ControllerBase
    {
        private readonly DatosContext _PublicacionContexto;
        public PublicacionesController (DatosContext PublicacionesContexto)
        {
            _PublicacionContexto = PublicacionesContexto;
        }

        //Enpoint que retorna el listado de todas las publicaciones existentes
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Publicaciones> listadoPublicaciones = (from e in _PublicacionContexto.Publicaciones
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
                _PublicacionContexto.Publicaciones.Add(Publicaciones);
                _PublicacionContexto.SaveChanges();
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
            Publicaciones? PublicacionActual = (from e in _PublicacionContexto.Publicaciones
                                  where e.publicacionId == id
                                  select e).FirstOrDefault();
            //Verificacion de la existencia del registro.
            if (PublicacionActual == null)
            { return NotFound(); }

            //Campos modificables
            PublicacionActual.titulo = PublicacionModificado.titulo;
            PublicacionActual.descripcion = PublicacionModificado.descripcion;

            //Marcado de registro modificado.
            _PublicacionContexto.Entry(PublicacionActual).State = EntityState.Modified;
            _PublicacionContexto.SaveChanges();
            return Ok(PublicacionModificado);
        }


        /// <summary>
        /// Buscador de publicaciones por el  ID de usuario.
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("BuscarPorUsuarioId/{usuarioId}")]
        public IActionResult BuscarPublicacionesPorUsuarioId(int usuarioId)
        {
            // Verificación de usuario válido
            var usuario = _PublicacionContexto.Usuarios
                            .Where(u => u.usuarioId== usuarioId)
                            .Select(u => new { u.usuarioId, u.nombreUsuario })
                            .FirstOrDefault();

            if (usuario == null)
            {
                return NotFound($"No se encontró un usuario con el ID {usuarioId}.");
            }

            // Obtener publicaciones del usuario filtrando por ID
            var publicacionesEncontradas = _PublicacionContexto.Publicaciones
                                        .Where(p => p.usuarioId == usuarioId)
                                        .Select(p => new
                                        {
                                            p.publicacionId,
                                            p.titulo,
                                            p.descripcion,
                                            UsuarioId = usuario.usuarioId,
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
