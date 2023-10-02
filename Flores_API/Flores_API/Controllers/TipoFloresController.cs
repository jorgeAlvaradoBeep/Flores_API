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
    public class TipoFloresController : Controller
    {
        private readonly FloresAPIContext _context;

        public TipoFloresController(FloresAPIContext context)
        {
            _context = context;
        }

        #region GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Response>>> GetTipoFlores()
        {
            Services.Response response = new Services.Response();
            response.data = await _context.TipoFlores.ToListAsync();
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

        //Obtenemos el tipo de flor por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Response>>> GetTipoFlor(int id)
        {
            Response response = new Response();
            response.data = await _context.TipoFlores.FindAsync(id);
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

        //Extraemos el tipo de flor por referencia de busqueda con busqueda por su tipo de flor
        [HttpGet("Tipo/{cs}")]
        public async Task<ActionResult<IEnumerable<Response>>> GetTipo(string cs)
        {
            Response response = new Response();
            response.data = await _context.TipoFlores.Where(a => a.TipoFlorP.Contains(cs)).ToListAsync();
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
        //Extraemos el tipo de flor por referencia de busqueda con busqueda por su nombre especifico
        [HttpGet("NombreEspecifico/{cs}")]
        public async Task<ActionResult<IEnumerable<Response>>> GetTipoEspecifico(string cs)
        {
            Response response = new Response();
            response.data = await _context.TipoFlores.Where(a => a.NombreEspecifico.Contains(cs)).ToListAsync();
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
        public async Task<IActionResult> Create(TipoFlor tipoFlor)
        {
            Response response = new Response();
            if (ModelState.IsValid)
            {
                _context.Add(tipoFlor);
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

