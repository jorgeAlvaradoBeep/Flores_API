using System;
using Flores_API.AuxiliarModels;
using Flores_API.Data;
using Flores_API.Models;
using Flores_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flores_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FloresController:Controller
	{
        private readonly FloresAPIContext _context;

        public FloresController(FloresAPIContext context)
        {
            _context = context;
        }

        #region GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Response>>> GetFlores()
        {
            Response response = new Response();
            response.data = await _context.Flores.ToListAsync();
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
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Response>>> Getflor(int id)
        {
            Response response = new Response();
            response.data = await _context.Flores.FindAsync(id);
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

        //Extraemos la flor por coincidencias en su nombre
        [HttpGet("Nombre/{cs}")]
        public async Task<ActionResult<IEnumerable<Response>>> GetFloresN(string cs)
        {
            Response response = new Response();
            response.data = await _context.Flores.Where(a => a.Nombre.Contains(cs)).ToListAsync();
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

        //Extraemos la flor por coincidencias en su tipo de flor
        [HttpGet("Tipo/{id}")]
        public async Task<ActionResult<IEnumerable<Response>>> GetFloresTipo(int id)
        {
            Response response = new Response();
            response.data = await _context.Flores.Where(a => a.IdTipoFlor.Equals(id)).ToListAsync();
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
        public async Task<IActionResult> Create([FromBody] Flores flor)
        {
            Response response = new Response();
            if (ModelState.IsValid)
            {
                
                _context.Add(flor);
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

        #region Delete
        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> Deleteflor(int id)
        {
            Response response = new Response();
            var flor = await _context.Flores.FindAsync(id);
            if (flor == null)
            {
                response.statusCode = 200;
                response.succes = false;
                response.message = "El ID a eliminar no corresponde con los datos del producto enviado";
                return Ok(response);
            }
            try
            {
                _context.Remove(flor);
                await _context.SaveChangesAsync();
                response.statusCode = 200;
                response.succes = true;
                response.message = "Flor Eliminada con exito.";
                return Ok(response);
            }
            catch (Exception e)
            {
                response.statusCode = 200;
                response.succes = false;
                response.message = $"Error al eliminar: {e.Message}";
                return Ok(response);
            }
        }
        #endregion
    }
}

