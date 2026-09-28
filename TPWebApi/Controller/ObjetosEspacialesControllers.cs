using Microsoft.AspNetCore.Mvc;
using Ejercicio6.Models;

namespace Ejercicio6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ObjetosEspacialesController : ControllerBase
    {
        private static List<ObjetoEspacial> objetos = new List<ObjetoEspacial>();
        private static List<Observacion> observaciones = new List<Observacion>();

        [HttpPost]
        public IActionResult RegistrarObjeto(ObjetoEspacial objeto)
        {
            objeto.Id = objetos.Count + 1;
            objetos.Add(objeto);

            return Ok(objeto);
        }

        [HttpPost("observaciones")]
        public IActionResult RegistrarObservacion(Observacion observacion)
        {
            var objeto = objetos
                .FirstOrDefault(o => o.Id == observacion.ObjetoEspacialId);

            if (objeto == null)
            {
                return NotFound("El objeto espacial no existe.");
            }

            observacion.Id = observaciones.Count + 1;
            observaciones.Add(observacion);

            return Ok(observacion);
        }

        [HttpGet("{id}/observaciones")]
        public IActionResult ObservacionesObjeto(int id)
        {
            return Ok(observaciones
                .Where(o => o.ObjetoEspacialId == id)
                .ToList());
        }

        [HttpGet("buscar")]
        public IActionResult BuscarNombre(string nombre)
        {
            return Ok(objetos
                .Where(o => o.Nombre.ToLower().Contains(nombre.ToLower()))
                .ToList());
        }

        [HttpGet("tipo")]
        public IActionResult FiltrarTipo(string tipo)
        {
            return Ok(objetos
                .Where(o => o.Tipo.ToLower() == tipo.ToLower())
                .ToList());
        }

        [HttpGet("riesgo")]
        public IActionResult FiltrarRiesgo(int nivel)
        {
            return Ok(objetos
                .Where(o => o.NivelRiesgo >= nivel)
                .ToList());
        }

        [HttpGet("distancia")]
        public IActionResult DistanciaInferior(double distancia)
        {
            return Ok(objetos
                .Where(o => o.Distancia < distancia)
                .ToList());
        }

        [HttpGet("velocidad-promedio")]
        public IActionResult VelocidadPromedio()
        {
            if (!observaciones.Any())
            {
                return BadRequest("No hay observaciones.");
            }

            return Ok(observaciones.Average(o => o.Velocidad));
        }

        [HttpGet("mayor-cantidad-observaciones")]
        public IActionResult MayorCantidadObservaciones()
        {
            var resultado = objetos
                .Select(o => new
                {
                    Objeto = o,
                    Cantidad = observaciones
                        .Count(x => x.ObjetoEspacialId == o.Id)
                })
                .OrderByDescending(x => x.Cantidad)
                .FirstOrDefault();

            return Ok(resultado);
        }

        [HttpGet("sin-observaciones")]
        public IActionResult SinObservaciones()
        {
            var resultado = objetos
                .Where(o => !observaciones.Any(
                    x => x.ObjetoEspacialId == o.Id))
                .ToList();

            return Ok(resultado);
        }


        [HttpGet("ordenados-riesgo")]
        public IActionResult OrdenadosRiesgo()
        {
            return Ok(objetos
                .OrderByDescending(o => o.NivelRiesgo)
                .ToList());
        }


        [HttpGet("alertas")]
        public IActionResult Alertas(double distancia = 100000)
        {
            var resultado = objetos
                .Where(o =>
                    o.NivelRiesgo >= 8 &&
                    o.Distancia < distancia &&
                    o.Activo)
                .ToList();

            return Ok(resultado);
        }

    
        [HttpGet("estadisticas")]
        public IActionResult Estadisticas()
        {
            var resultado = new
            {
                TotalObjetos = objetos.Count,
                TotalObservaciones = observaciones.Count,
                ObjetosActivos = objetos.Count(o => o.Activo),
                ObjetosAltoRiesgo = objetos.Count(o => o.NivelRiesgo >= 8),
                VelocidadPromedio = observaciones.Any()
                    ? observaciones.Average(o => o.Velocidad)
                    : 0
            };

            return Ok(resultado);
        }
    }
}