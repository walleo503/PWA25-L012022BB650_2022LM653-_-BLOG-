using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2022BB650_2022LM653.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace L01_2022BB650_2022LM653.Properties
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly DatosContext _UsuariosContexto;

        public UsuariosController (DatosContext UsuariosContexto)
        {
            _UsuariosContexto = UsuariosContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Usuarios> ListaUsuarios = (from e in _UsuariosContexto.Usuarios
                                            select e).ToList();
            if (ListaUsuarios.Count() == 0)
            { 
                return NotFound();
            }
            return Ok(ListaUsuarios);
        }
    }
}
