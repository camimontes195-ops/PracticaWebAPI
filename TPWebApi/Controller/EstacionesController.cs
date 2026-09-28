using Microsoft.AspNetCore.Mvc;
using Ejercicio2.Models;

namespace Ejercicio2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstacionesController : ControllerBase
    {
        private static List<Estacion> estaciones = new List<Estacion>();
        private static List<Medicion> mediciones = new List<Medicion>();

        
        [HttpPost]
        public IActionResult RegistrarEstacion(Estacion estacion)
        {
            estacion.Id = estaciones.Count + 1;
            estaciones.Add(estacion);

            return Ok(estacion);
        }

      
        [HttpPost("mediciones")]
        public IActionResult RegistrarMedicion(Medicion medicion)
        {
            var estacion = estaciones.FirstOrDefault(e => e.Id == medicion.EstacionId);

            if (estacion == null)
            {
                return NotFound("La estación no existe.");
            }

            if (!estacion.Activa)
            {
                return BadRequest("La estación está inactiva.");
            }

            if (medicion.Humedad < 0 || medicion.Humedad > 100)
            {
                return BadRequest("La humedad debe estar entre 0 y 100.");
            }

            if (medicion.VelocidadViento < 0)
            {
                return BadRequest("La velocidad del viento no puede ser negativa.");
            }

            if (medicion.FechaHora > DateTime.Now)
            {
                return BadRequest("La fecha no puede ser futura.");
            }

            medicion.Id = mediciones.Count + 1;
            mediciones.Add(medicion);

            return Ok(medicion);
        }

    
        [HttpGet("mediciones")]
        public IActionResult ObtenerMediciones()
        {
            return Ok(mediciones);
        }

       
        [HttpGet("{id}/mediciones")]
        public IActionResult ObtenerMedicionesEstacion(int id)
        {
            var resultado = mediciones
                .Where(m => m.EstacionId == id)
                .ToList();

            return Ok(resultado);
        }

      
        [HttpGet("localidad")]
        public IActionResult BuscarPorLocalidad(string localidad)
        {
            var resultado = estaciones
                .Where(e => e.Localidad.ToLower() == localidad.ToLower())
                .ToList();

            return Ok(resultado);
        }

    
        [HttpGet("temperatura-superior")]
        public IActionResult TemperaturaSuperior(double temperatura)
        {
            var resultado = mediciones
                .Where(m => m.Temperatura > temperatura)
                .ToList();

            return Ok(resultado);
        }

        [HttpGet("ordenadas")]
        public IActionResult MedicionesOrdenadas()
        {
            var resultado = mediciones
                .OrderByDescending(m => m.Temperatura)
                .ToList();

            return Ok(resultado);
        }

        [HttpGet("temperatura-promedio")]
        public IActionResult TemperaturaPromedio()
        {
            if (!mediciones.Any())
            {
                return BadRequest("No hay mediciones.");
            }

            double promedio = mediciones.Average(m => m.Temperatura);

            return Ok(promedio);
        }

   
        [HttpGet("temperatura-maxima")]
        public IActionResult TemperaturaMaxima()
        {
            if (!mediciones.Any())
            {
                return BadRequest("No hay mediciones.");
            }

            double maxima = mediciones.Max(m => m.Temperatura);

            return Ok(maxima);
        }

        [HttpGet("temperatura-minima")]
        public IActionResult TemperaturaMinima()
        {
            if (!mediciones.Any())
            {
                return BadRequest("No hay mediciones.");
            }

            double minima = mediciones.Min(m => m.Temperatura);

            return Ok(minima);
        }

        [HttpGet("{id}/cantidad-mediciones")]
        public IActionResult CantidadMediciones(int id)
        {
            int cantidad = mediciones.Count(m => m.EstacionId == id);

            return Ok(cantidad);
        }
    }
}