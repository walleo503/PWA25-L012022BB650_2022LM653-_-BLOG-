﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2022BB650_2022LM653.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2022BB650_2022LM653.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalificacionesController : ControllerBase
    {
        private readonly DatosContext _CalificacionesContexto;

        public CalificacionesController(DatosContext CalificacionesContexto) { 
        
            _CalificacionesContexto = CalificacionesContexto;
        }

   

    }
}
