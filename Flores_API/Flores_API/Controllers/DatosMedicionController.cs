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
    public class DatosMedicionController:Controller
	{
        private readonly FloresAPIContext _context;

        public DatosMedicionController(FloresAPIContext context)
        {
            _context = context;
        }
        #region GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Response>>> GetDatosDeMedicion()
        {
            Response response = new Response();
            response.data = await _context.DatosMedicion.ToListAsync();
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

        //Obtenemos el dato de medicion por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Response>>> GetDatoDeMedicion(int id)
        {
            Response response = new Response();
            response.data = await _context.DatosMedicion.FindAsync(id);
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

        //Extraemos el dato de medicion por coincidencias en su nombre
        [HttpGet("Nombre/{cs}")]
        public async Task<ActionResult<IEnumerable<Response>>> GetDatoMedicionN(string cs)
        {
            Response response = new Response();
            response.data = await _context.DatosMedicion.Where(a => a.NombreDato.Contains(cs)).ToListAsync();
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
        #endregion

        #region POST
        [HttpPost("Create")]
        public async Task<IActionResult> Create(DatosMedicion datoMedicion)
        {
            Response response = new Response();
            if (ModelState.IsValid)
            {
                _context.Add(datoMedicion);
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
            return Ok(response);
        }
        #endregion
    }
}

