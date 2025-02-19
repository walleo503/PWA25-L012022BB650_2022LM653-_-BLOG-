using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2022BB650_2022LM653.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace L01_2022BB650_2022LM653.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentariosController : ControllerBase
    {
        private readonly DatosContext _context;

        public ComentariosController(DatosContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los comentarios.
        /// </summary>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            var comentarios = _context.Comentarios.ToList();
            if (comentarios.Count == 0)
            {
                return NotFound();
            }
            return Ok(comentarios);
        }

        /// <summary>
        /// Agrega un nuevo comentario.
        /// </summary>
        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarComentario([FromBody] Comentarios comentario)
        {
            try
            {
                _context.Comentarios.Add(comentario);
                _context.SaveChanges();
                return Ok(comentario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Actualiza un comentario existente.
        /// </summary>
        [HttpPut]
        [Route("Actualizar/{id}")]
        public IActionResult ActualizarComentario(int id, [FromBody] Comentarios comentarioModificar)
        {
            var comentarioActual = _context.Comentarios.Find(id);

            if (comentarioActual == null)
            {
                return NotFound();
            }

            comentarioActual.publicacionesId = comentarioModificar.publicacionesId;
            comentarioActual.comentario = comentarioModificar.comentario;
            comentarioActual.usuarioId = comentarioModificar.usuarioId;

            _context.Entry(comentarioActual).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(comentarioModificar);
        }

        /// <summary>
        /// Elimina un comentario por su ID.
        /// </summary>
        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IActionResult EliminarComentario(int id)
        {
            var comentario = _context.Comentarios.Find(id);
            if (comentario == null)
            {
                return NotFound();
            }

            _context.Comentarios.Remove(comentario);
            _context.SaveChanges();

            return Ok(comentario);
        }

        /// <summary>
        /// Obtiene comentarios filtrados por una publicacinn especifica.
        /// </summary>
        [HttpGet]
        [Route("FiltrarPorPublicacion/{publicacionId}")]
        public IActionResult ObtenerComentariosPorPublicacion(int publicacionId)
        {
            var comentario = _context.Comentarios
                .Where(c => c.publicacionesId == publicacionId)
                .ToList();

            if (comentario.Count == 0)
            {
                return NotFound();
            }

            return Ok(comentario);
        }
    }
}
