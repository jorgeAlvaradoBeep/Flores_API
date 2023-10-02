using System;
using Flores_API.Data;
using Flores_API.Models;
using Flores_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flores_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicionesController:Controller
	{
        private readonly FloresAPIContext _context;

        public MedicionesController(FloresAPIContext context)
        {
            _context = context;
        }
        #region GET
        //Obtenemos la flor por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Response>>> GetMedicion(int id)
        {
            Response response = new Response();
            response.data = await _context.Mediciones.FindAsync(id);
            if (response.data == null)
            {
                response.succes = false;
                response.statusCode = 200;
                response.message = "Error al extraer datos";
            }
            else
            {
                response.succes = true;
                response.statusCode = 200;
            }

            return Ok(response);
        }
        //Obtenemos la flor por ID
        [HttpGet("Flor/{id}")]
        public async Task<ActionResult<IEnumerable<Response>>> GetFlorMedicion(int id)
        {
            Response response = new Response();
            response.data = await _context.Mediciones.Where(e => e.IdFlor.Equals(id)).ToListAsync();
            if (response.data == null)
            {
                response.succes = false;
                response.statusCode = 200;
                response.message = "Error al extraer datos";
            }
            else
            {
                response.succes = true;
                response.statusCode = 200;
            }

            return Ok(response);
        }

        //Verificamos que exista conexión con la API desde el ESP32
        [HttpGet("Status/")]
        public async Task<ActionResult<IEnumerable<Response>>> CheckForConnection(int id)
        {
            return Ok();
        }
        #endregion

        #region POST
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Mediciones medicion)
        {
            Response response = new Response();
            if (ModelState.IsValid)
            {

                _context.Add(medicion);
                var correct = await _context.SaveChangesAsync();
                response.data = correct;
                if (response.data == null)
                {
                    response.succes = false;
                    response.statusCode = 200;
                    response.message = "Error al insertar";
                }
                else
                {
                    response.succes = true;
                    response.statusCode = 200;
                }
                return Ok(response);
            }
            response.succes = false;
            response.message = "Los datos recibidos no son correctos";
            response.statusCode = 400;
            return BadRequest(response);
        }
        #endregion
    }
}

