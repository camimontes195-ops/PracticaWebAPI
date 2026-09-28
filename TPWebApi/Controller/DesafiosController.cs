using Microsoft.AspNetCore.Mvc;
using Ejercicio3.Models;

namespace Ejercicio3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DesafiosController : ControllerBase
    {
        private static List<Participante> participantes = new List<Participante>();
        private static List<Desafio> desafios = new List<Desafio>();
        private static List<Resolucion> resoluciones = new List<Resolucion>();


        [HttpPost("participantes")]
        public IActionResult RegistrarParticipante(Participante participante)
        {
            if (participantes.Any(p => p.Email.ToLower() == participante.Email.ToLower()))
            {
                return BadRequest("El email ya está registrado.");
            }

            participante.Id = participantes.Count + 1;
            participantes.Add(participante);

            return Ok(participante);
        }

   
        [HttpPost]
        public IActionResult CrearDesafio(Desafio desafio)
        {
            desafio.Id = desafios.Count + 1;
            desafios.Add(desafio);

            return Ok(desafio);
        }

   
        [HttpPost("resoluciones")]
        public IActionResult RegistrarResolucion(Resolucion resolucion)
        {
            var participante = participantes
                .FirstOrDefault(p => p.Id == resolucion.ParticipanteId);

            var desafio = desafios
                .FirstOrDefault(d => d.Id == resolucion.DesafioId);

            if (participante == null)
            {
                return NotFound("El participante no existe.");
            }

            if (desafio == null)
            {
                return NotFound("El desafío no existe.");
            }

            if (!desafio.Activo)
            {
                return BadRequest("El desafío está inactivo.");
            }

            if (resoluciones.Any(r =>
                r.ParticipanteId == resolucion.ParticipanteId &&
                r.DesafioId == resolucion.DesafioId))
            {
                return BadRequest("El participante ya resolvió este desafío.");
            }

            if (resolucion.PuntajeObtenido > desafio.PuntajeMaximo)
            {
                return BadRequest("El puntaje supera el máximo.");
            }

            resolucion.Id = resoluciones.Count + 1;
            resoluciones.Add(resolucion);

            return Ok(resolucion);
        }

        [HttpGet("activos")]
        public IActionResult DesafiosActivos()
        {
            return Ok(desafios.Where(d => d.Activo).ToList());
        }

        [HttpGet("dificultad")]
        public IActionResult BuscarDificultad(string dificultad)
        {
            return Ok(desafios
                .Where(d => d.Dificultad.ToLower() == dificultad.ToLower())
                .ToList());
        }

        [HttpGet("participantes/{id}/resoluciones")]
        public IActionResult ResolucionesParticipante(int id)
        {
            return Ok(resoluciones
                .Where(r => r.ParticipanteId == id)
                .ToList());
        }

        [HttpGet("participantes/puntaje")]
        public IActionResult ParticipantesPuntaje(int puntaje)
        {
            var resultado = resoluciones
                .Where(r => r.PuntajeObtenido > puntaje)
                .Select(r => participantes.FirstOrDefault(
                    p => p.Id == r.ParticipanteId))
                .Where(p => p != null)
                .Distinct()
                .ToList();

            return Ok(resultado);
        }

        [HttpGet("{id}/promedio")]
        public IActionResult PromedioDesafio(int id)
        {
            var datos = resoluciones
                .Where(r => r.DesafioId == id)
                .ToList();

            if (!datos.Any())
            {
                return BadRequest("No hay resoluciones.");
            }

            return Ok(datos.Average(r => r.PuntajeObtenido));
        }

        [HttpGet("participante-mayor-puntaje")]
        public IActionResult MayorPuntaje()
        {
            var resultado = participantes
                .Select(p => new
                {
                    Participante = p,
                    PuntajeTotal = resoluciones
                        .Where(r => r.ParticipanteId == p.Id)
                        .Sum(r => r.PuntajeObtenido)
                })
                .OrderByDescending(x => x.PuntajeTotal)
                .FirstOrDefault();

            return Ok(resultado);
        }

        [HttpGet("participantes/ordenados")]
        public IActionResult ParticipantesOrdenados()
        {
            var resultado = participantes
                .Select(p => new
                {
                    Participante = p,
                    PuntajeTotal = resoluciones
                        .Where(r => r.ParticipanteId == p.Id)
                        .Sum(r => r.PuntajeObtenido)
                })
                .OrderByDescending(x => x.PuntajeTotal)
                .ToList();

            return Ok(resultado);
        }

        [HttpGet("participantes/{id}/pendientes")]
        public IActionResult DesafiosPendientes(int id)
        {
            var resultado = desafios
                .Where(d => !resoluciones.Any(
                    r => r.ParticipanteId == id &&
                    r.DesafioId == d.Id))
                .ToList();

            return Ok(resultado);
        }
    }
}