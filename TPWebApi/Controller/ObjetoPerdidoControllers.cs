using Microsoft.AspNetCore.Mvc;
using Ejercicio1ObjetosPerdidos.Models;

namespace Ejercicio1ObjetosPerdidos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ObjetosPerdidosController : ControllerBase
    {
        private static List<ObjetoPerdido> objetos = new List<ObjetoPerdido>();


        [HttpPost]
        public IActionResult RegistrarObjeto(ObjetoPerdido objeto)
        {
            
            if (string.IsNullOrWhiteSpace(objeto.Descripcion))
            {
                return BadRequest("La descripción no puede estar vacía.");
            }

            if (objeto.FechaEncontrado > DateTime.Now)
            {
                return BadRequest("La fecha no puede ser futura.");
            }

            objeto.Id = objetos.Count + 1;

            objeto.Reclamado = false;
            objeto.NombrePersonaQueRetiro = null;

            objetos.Add(objeto);

            return Ok(objeto);
        }

        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            return Ok(objetos);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            var objeto = objetos.FirstOrDefault(o => o.Id == id);

            if (objeto == null)
            {
                return NotFound("No se encontró el objeto.");
            }

            return Ok(objeto);
        }

        [HttpPut("{id}")]
        public IActionResult Modificar(int id, ObjetoPerdido objetoModificado)
        {
            var objeto = objetos.FirstOrDefault(o => o.Id == id);

            if (objeto == null)
            {
                return NotFound("No se encontró el objeto.");
            }

            if (string.IsNullOrWhiteSpace(objetoModificado.Descripcion))
            {
                return BadRequest("La descripción no puede estar vacía.");
            }

            if (objetoModificado.FechaEncontrado > DateTime.Now)
            {
                return BadRequest("La fecha no puede ser futura.");
            }

            objeto.Descripcion = objetoModificado.Descripcion;
            objeto.Categoria = objetoModificado.Categoria;
            objeto.LugarEncontrado = objetoModificado.LugarEncontrado;
            objeto.FechaEncontrado = objetoModificado.FechaEncontrado;

            return Ok(objeto);
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            var objeto = objetos.FirstOrDefault(o => o.Id == id);

            if (objeto == null)
            {
                return NotFound("No se encontró el objeto.");
            }

            objetos.Remove(objeto);

            return Ok("Objeto eliminado correctamente.");
        }

        [HttpGet("buscar")]
        public IActionResult BuscarPorDescripcion(string descripcion)
        {
            var resultado = objetos
                .Where(o => o.Descripcion.ToLower().Contains(descripcion.ToLower()))
                .ToList();

            return Ok(resultado);
        }

        [HttpGet("categoria")]
        public IActionResult FiltrarPorCategoria(string categoria)
        {
            var resultado = objetos
                .Where(o => o.Categoria.ToLower() == categoria.ToLower())
                .ToList();

            return Ok(resultado);
        }

        [HttpGet("no-reclamados")]
        public IActionResult ObtenerNoReclamados()
        {
            var resultado = objetos
                .Where(o => o.Reclamado == false)
                .ToList();

            return Ok(resultado);
        }

        [HttpGet("despues-de")]
        public IActionResult ObtenerDespuesDe(DateTime fecha)
        {
            var resultado = objetos
                .Where(o => o.FechaEncontrado > fecha)
                .ToList();

            return Ok(resultado);
        }

        [HttpGet("ordenados")]
        public IActionResult ObtenerOrdenados()
        {
            var resultado = objetos
                .OrderByDescending(o => o.FechaEncontrado)
                .ToList();

            return Ok(resultado);
        }

        [HttpPut("{id}/reclamar")]
        public IActionResult ReclamarObjeto(int id, string nombrePersona)
        {
            var objeto = objetos.FirstOrDefault(o => o.Id == id);

            if (objeto == null)
            {
                return NotFound("No se encontró el objeto.");
            }

            if (objeto.Reclamado)
            {
                return BadRequest("El objeto ya fue reclamado.");
            }

            if (string.IsNullOrWhiteSpace(nombrePersona))
            {
                return BadRequest("Debe indicar quién retiró el objeto.");
            }

            objeto.Reclamado = true;
            objeto.NombrePersonaQueRetiro = nombrePersona;

            return Ok("Objeto reclamado correctamente.");
        }

        [HttpGet("existe-categoria")]
        public IActionResult ExisteCategoria(string categoria)
        {
            bool existe = objetos.Any(
                o => o.Categoria.ToLower() == categoria.ToLower()
            );

            return Ok(existe);
        }

        [HttpGet("cantidad")]
        public IActionResult ObtenerCantidad()
        {
            int cantidad = objetos.Count();

            return Ok(cantidad);
        }
    }
}