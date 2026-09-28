using Microsoft.AspNetCore.Mvc;
using Ejercicio4.Models;

namespace Ejercicio4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DronesController : ControllerBase
    {
        private static List<Drone> drones = new List<Drone>();
        private static List<Mision> misiones = new List<Mision>();

        [HttpPost]
        public IActionResult Registrar(Drone drone)
        {
            drone.Id = drones.Count + 1;
            drones.Add(drone);

            return Ok(drone);
        }

        [HttpPost("misiones")]
        public IActionResult AsignarMision(Mision mision)
        {
            var drone = drones.FirstOrDefault(d => d.Id == mision.DroneId);

            if (drone == null)
            {
                return NotFound("El drone no existe.");
            }

            if (drone.Estado != "Disponible")
            {
                return BadRequest("El drone no está disponible.");
            }

            if (drone.Bateria < 30)
            {
                return BadRequest("El drone necesita al menos 30% de batería.");
            }

            if (misiones.Any(m =>
                m.DroneId == mision.DroneId &&
                !m.Completada))
            {
                return BadRequest("El drone ya tiene una misión activa.");
            }

            mision.Id = misiones.Count + 1;
            mision.Completada = false;

            misiones.Add(mision);

            drone.Estado = "EnMision";

            return Ok(mision);
        }

        [HttpGet("disponibles")]
        public IActionResult Disponibles()
        {
            return Ok(drones
                .Where(d => d.Estado == "Disponible")
                .ToList());
        }

        [HttpGet("bateria")]
        public IActionResult BateriaInferior(double porcentaje)
        {
            return Ok(drones
                .Where(d => d.Bateria < porcentaje)
                .ToList());
        }

        [HttpGet("modelo")]
        public IActionResult BuscarModelo(string modelo)
        {
            return Ok(drones
                .Where(d => d.Modelo.ToLower() == modelo.ToLower())
                .ToList());
        }

        [HttpPut("misiones/{id}/finalizar")]
        public IActionResult FinalizarMision(int id)
        {
            var mision = misiones.FirstOrDefault(m => m.Id == id);

            if (mision == null)
            {
                return NotFound("La misión no existe.");
            }

            if (mision.Completada)
            {
                return BadRequest("La misión ya fue finalizada.");
            }

            var drone = drones.FirstOrDefault(d => d.Id == mision.DroneId);

            mision.Completada = true;

            if (drone != null)
            {
                drone.Estado = "Disponible";
            }

            return Ok(mision);
        }

        [HttpGet("{id}/misiones")]
        public IActionResult MisionesDrone(int id)
        {
            return Ok(misiones
                .Where(m => m.DroneId == id)
                .ToList());
        }

        [HttpGet("distancia/{id}")]
        public IActionResult DistanciaTotal(int id)
        {
            double total = misiones
                .Where(m => m.DroneId == id && m.Completada)
                .Sum(m => m.DistanciaKm);

            return Ok(total);
        }

        [HttpGet("mayor-distancia")]
        public IActionResult MayorDistancia()
        {
            var resultado = drones
                .Select(d => new
                {
                    Drone = d,
                    DistanciaTotal = misiones
                        .Where(m => m.DroneId == d.Id && m.Completada)
                        .Sum(m => m.DistanciaKm)
                })
                .OrderByDescending(x => x.DistanciaTotal)
                .FirstOrDefault();

            return Ok(resultado);
        }

  
        [HttpGet("promedio-bateria")]
        public IActionResult PromedioBateria()
        {
            var disponibles = drones
                .Where(d => d.Estado == "Disponible")
                .ToList();

            if (!disponibles.Any())
            {
                return BadRequest("No hay drones disponibles.");
            }

            return Ok(disponibles.Average(d => d.Bateria));
        }

   
        [HttpGet("misiones-pendientes")]
        public IActionResult MisionesPendientes()
        {
            return Ok(misiones
                .Where(m => !m.Completada)
                .OrderBy(m => m.Fecha)
                .ToList());
        }
    }
}