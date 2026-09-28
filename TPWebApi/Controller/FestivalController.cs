using Microsoft.AspNetCore.Mvc;
using Ejercicio5.Models;

namespace Ejercicio5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FestivalController : ControllerBase
    {
        private static List<Actividad> actividades = new List<Actividad>();
        private static List<Participante> participantes = new List<Participante>();
        private static List<ReservaActividad> reservas = new List<ReservaActividad>();

      
        [HttpPost("actividades")]
        public IActionResult CrearActividad(Actividad actividad)
        {
            actividad.Id = actividades.Count + 1;
            actividades.Add(actividad);

            return Ok(actividad);
        }

    
        [HttpPost("participantes")]
        public IActionResult RegistrarParticipante(Participante participante)
        {
            if (participantes.Any(p =>
                p.Email.ToLower() == participante.Email.ToLower()))
            {
                return BadRequest("El email ya está registrado.");
            }

            participante.Id = participantes.Count + 1;
            participantes.Add(participante);

            return Ok(participante);
        }

  
        [HttpPost("reservas")]
        public IActionResult Inscribir(ReservaActividad reserva)
        {
            var actividad = actividades
                .FirstOrDefault(a => a.Id == reserva.ActividadId);

            var participante = participantes
                .FirstOrDefault(p => p.Id == reserva.ParticipanteId);

            if (actividad == null)
            {
                return NotFound("La actividad no existe.");
            }

            if (participante == null)
            {
                return NotFound("El participante no existe.");
            }

            if (!actividad.Activa)
            {
                return BadRequest("La actividad está inactiva.");
            }

            if (reservas.Any(r =>
                r.ActividadId == reserva.ActividadId &&
                r.ParticipanteId == reserva.ParticipanteId))
            {
                return BadRequest("La persona ya está inscripta.");
            }

            int cantidad = reservas.Count(r =>
                r.ActividadId == reserva.ActividadId);

            if (cantidad >= actividad.Capacidad)
            {
                return BadRequest("La actividad está completa.");
            }

            DateTime inicioNueva = actividad.Horario;
            DateTime finNueva = actividad.Horario
                .AddMinutes(actividad.DuracionMinutos);

            var reservasParticipante = reservas
                .Where(r => r.ParticipanteId == participante.Id)
                .ToList();

            foreach (var reservaExistente in reservasParticipante)
            {
                var actividadExistente = actividades
                    .FirstOrDefault(a => a.Id == reservaExistente.ActividadId);

                if (actividadExistente != null)
                {
                    DateTime inicioExistente = actividadExistente.Horario;
                    DateTime finExistente = actividadExistente.Horario
                        .AddMinutes(actividadExistente.DuracionMinutos);

                    bool seSuperpone =
                        inicioNueva < finExistente &&
                        finNueva > inicioExistente;

                    if (seSuperpone)
                    {
                        return BadRequest(
                            "El participante ya tiene una actividad en ese horario.");
                    }
                }
            }

            reserva.Id = reservas.Count + 1;
            reserva.FechaReserva = DateTime.Now;

            reservas.Add(reserva);

            return Ok(reserva);
        }

     
        [HttpDelete("reservas/{id}")]
        public IActionResult CancelarInscripcion(int id)
        {
            var reserva = reservas.FirstOrDefault(r => r.Id == id);

            if (reserva == null)
            {
                return NotFound("La reserva no existe.");
            }

            reservas.Remove(reserva);

            return Ok("Inscripción cancelada.");
        }

        [HttpGet("actividades/disponibles")]
        public IActionResult ActividadesDisponibles()
        {
            var resultado = actividades
                .Where(a =>
                    a.Activa &&
                    reservas.Count(r => r.ActividadId == a.Id) < a.Capacidad)
                .ToList();

            return Ok(resultado);
        }

        [HttpGet("actividades/{id}/participantes")]
        public IActionResult ParticipantesActividad(int id)
        {
            var resultado = reservas
                .Where(r => r.ActividadId == id)
                .Select(r => participantes
                    .FirstOrDefault(p => p.Id == r.ParticipanteId))
                .Where(p => p != null)
                .ToList();

            return Ok(resultado);
        }

        [HttpGet("participantes/{id}/actividades")]
        public IActionResult ActividadesParticipante(int id)
        {
            var resultado = reservas
                .Where(r => r.ParticipanteId == id)
                .Select(r => actividades
                    .FirstOrDefault(a => a.Id == r.ActividadId))
                .Where(a => a != null)
                .ToList();

            return Ok(resultado);
        }

        [HttpGet("actividades/buscar")]
        public IActionResult BuscarNombre(string nombre)
        {
            return Ok(actividades
                .Where(a => a.Nombre.ToLower().Contains(nombre.ToLower()))
                .ToList());
        }

        [HttpGet("actividades/tipo")]
        public IActionResult FiltrarTipo(string tipo)
        {
            return Ok(actividades
                .Where(a => a.Tipo.ToLower() == tipo.ToLower())
                .ToList());
        }

      
        [HttpGet("actividades/completas")]
        public IActionResult ActividadesCompletas()
        {
            return Ok(actividades
                .Where(a =>
                    reservas.Count(r => r.ActividadId == a.Id) >= a.Capacidad)
                .ToList());
        }

      
        [HttpGet("actividades/ordenadas")]
        public IActionResult ActividadesOrdenadas()
        {
            var resultado = actividades
                .Select(a => new
                {
                    Actividad = a,
                    Inscriptos = reservas.Count(r => r.ActividadId == a.Id)
                })
                .OrderByDescending(x => x.Inscriptos)
                .ToList();

            return Ok(resultado);
        }

      
        [HttpGet("actividades/mayor-cantidad")]
        public IActionResult MayorCantidad()
        {
            var resultado = actividades
                .Select(a => new
                {
                    Actividad = a,
                    Inscriptos = reservas.Count(r => r.ActividadId == a.Id)
                })
                .OrderByDescending(x => x.Inscriptos)
                .FirstOrDefault();

            return Ok(resultado);
        }


        [HttpGet("actividades/ocupacion")]
        public IActionResult PorcentajeOcupacion()
        {
            var resultado = actividades
                .Select(a => new
                {
                    Actividad = a,
                    Inscriptos = reservas.Count(r => r.ActividadId == a.Id),
                    Porcentaje = a.Capacidad == 0
                        ? 0
                        : (double)reservas.Count(
                            r => r.ActividadId == a.Id)
                            / a.Capacidad * 100
                })
                .ToList();

            return Ok(resultado);
        }
    }
}