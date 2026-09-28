using Microsoft.AspNetCore.Mvc;
using Ejercicio7.Models;

namespace Ejercicio7.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpedicionesController : ControllerBase
    {
        private static List<Expedicion> expediciones = new List<Expedicion>();
        private static List<Explorador> exploradores = new List<Explorador>();
        private static List<Participacion> participaciones = new List<Participacion>();

        [HttpPost]
        public IActionResult CrearExpedicion(Expedicion expedicion)
        {
            if (expedicion.FechaFin < expedicion.FechaInicio)
            {
                return BadRequest(
                    "La fecha de fin no puede ser anterior a la fecha de inicio.");
            }

            expedicion.Id = expediciones.Count + 1;
            expediciones.Add(expedicion);

            return Ok(expedicion);
        }

        [HttpPost("exploradores")]
        public IActionResult RegistrarExplorador(Explorador explorador)
        {
            explorador.Id = exploradores.Count + 1;
            exploradores.Add(explorador);

            return Ok(explorador);
        }

        [HttpPost("participaciones")]
        public IActionResult AsignarExplorador(Participacion participacion)
        {
            var expedicion = expediciones
                .FirstOrDefault(e => e.Id == participacion.ExpedicionId);

            var explorador = exploradores
                .FirstOrDefault(e => e.Id == participacion.ExploradorId);

            if (expedicion == null)
            {
                return NotFound("La expedición no existe.");
            }

            if (explorador == null)
            {
                return NotFound("El explorador no existe.");
            }

            if (!explorador.Disponible)
            {
                return BadRequest("El explorador no está disponible.");
            }

            int cantidad = participaciones.Count(
                p => p.ExpedicionId == expedicion.Id);

            if (cantidad >= expedicion.Capacidad)
            {
                return BadRequest("La expedición está completa.");
            }

            if (participaciones.Any(p =>
                p.ExpedicionId == expedicion.Id &&
                p.ExploradorId == explorador.Id))
            {
                return BadRequest(
                    "El explorador ya está asignado a esta expedición.");
            }

            var otrasParticipaciones = participaciones
                .Where(p => p.ExploradorId == explorador.Id)
                .ToList();

            foreach (var otra in otrasParticipaciones)
            {
                var otraExpedicion = expediciones
                    .FirstOrDefault(e => e.Id == otra.ExpedicionId);

                if (otraExpedicion != null)
                {
                    bool seSuperponen =
                        expedicion.FechaInicio <= otraExpedicion.FechaFin &&
                        expedicion.FechaFin >= otraExpedicion.FechaInicio;

                    if (seSuperponen)
                    {
                        return BadRequest(
                            "El explorador ya participa en una expedición durante esas fechas.");
                    }
                }
            }

            participacion.Id = participaciones.Count + 1;

            participaciones.Add(participacion);

            explorador.Disponible = false;

            return Ok(participacion);
        }

        [HttpGet("{id}/integrantes")]
        public IActionResult Integrantes(int id)
        {
            var resultado = participaciones
                .Where(p => p.ExpedicionId == id)
                .Select(p => exploradores
                    .FirstOrDefault(e => e.Id == p.ExploradorId))
                .Where(e => e != null)
                .ToList();

            return Ok(resultado);
        }


        [HttpGet("exploradores/{id}/expediciones")]
        public IActionResult ExpedicionesExplorador(int id)
        {
            var resultado = participaciones
                .Where(p => p.ExploradorId == id)
                .Select(p => expediciones
                    .FirstOrDefault(e => e.Id == p.ExpedicionId))
                .Where(e => e != null)
                .ToList();

            return Ok(resultado);
        }

        [HttpGet("exploradores/especialidad")]
        public IActionResult BuscarEspecialidad(string especialidad)
        {
            return Ok(exploradores
                .Where(e =>
                    e.Especialidad.ToLower() == especialidad.ToLower())
                .ToList());
        }

        [HttpGet("exploradores/disponibles")]
        public IActionResult ExploradoresDisponibles()
        {
            return Ok(exploradores
                .Where(e => e.Disponible)
                .ToList());
        }

        [HttpGet("activas")]
        public IActionResult ExpedicionesActivas()
        {
            return Ok(expediciones
                .Where(e => e.Estado.ToLower() == "activa")
                .ToList());
        }

        [HttpGet("futuras")]
        public IActionResult ExpedicionesFuturas()
        {
            return Ok(expediciones
                .Where(e => e.FechaInicio > DateTime.Now)
                .ToList());
        }

        [HttpGet("completas")]
        public IActionResult ExpedicionesCompletas()
        {
            return Ok(expediciones
                .Where(e =>
                    participaciones.Count(
                        p => p.ExpedicionId == e.Id) >= e.Capacidad)
                .ToList());
        }

        [HttpGet("{id}/cantidad")]
        public IActionResult CantidadParticipantes(int id)
        {
            int cantidad = participaciones
                .Count(p => p.ExpedicionId == id);

            return Ok(cantidad);
        }

        [HttpGet("ocupacion")]
        public IActionResult PorcentajeOcupacion()
        {
            var resultado = expediciones
                .Select(e => new
                {
                    Expedicion = e,
                    Participantes = participaciones
                        .Count(p => p.ExpedicionId == e.Id),
                    Porcentaje = e.Capacidad == 0
                        ? 0
                        : (double)participaciones.Count(
                            p => p.ExpedicionId == e.Id)
                            / e.Capacidad * 100
                })
                .ToList();

            return Ok(resultado);
        }

        [HttpGet("ordenadas")]
        public IActionResult ExpedicionesOrdenadas()
        {
            return Ok(expediciones
                .OrderBy(e => e.FechaInicio)
                .ToList());
        }

        [HttpGet("exploradores/mayor-participacion")]
        public IActionResult MayorParticipacion()
        {
            var resultado = exploradores
                .Select(e => new
                {
                    Explorador = e,
                    Cantidad = participaciones
                        .Count(p => p.ExploradorId == e.Id)
                })
                .OrderByDescending(x => x.Cantidad)
                .ToList();

            return Ok(resultado);
        }

  
        [HttpGet("especialidades")]
        public IActionResult EspecialidadesUtilizadas()
        {
            var resultado = participaciones
                .Select(p => exploradores
                    .FirstOrDefault(e => e.Id == p.ExploradorId))
                .Where(e => e != null)
                .GroupBy(e => e.Especialidad)
                .Select(g => new
                {
                    Especialidad = g.Key,
                    Cantidad = g.Count()
                })
                .OrderByDescending(x => x.Cantidad)
                .ToList();

            return Ok(resultado);
        }
    }
}