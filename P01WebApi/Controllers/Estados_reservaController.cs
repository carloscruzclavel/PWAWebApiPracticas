using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P01WebApi.Models;

namespace P01WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Estados_reservaController : ControllerBase
    {
        public readonly equiposContext _equiposContext;

        public Estados_reservaController(equiposContext equiposContext)
        {
            _equiposContext= equiposContext;
        }

        [HttpGet]
        [Route("getall")]

        public IActionResult ObtenerEstadosreserva() 
        {
            List<estados_reserva> listadoEstadoreservas = (from e in _equiposContext.estados_reserva 
                                                           select e).ToList();

            if (listadoEstadoreservas.Count == 0) { return NotFound(); }

            return Ok(listadoEstadoreservas);
        }


        [HttpGet]
        [Route("add")]

        public IActionResult crear([FromBody] estados_reserva estadoreservaNueva)
        {
            try
            {
                _equiposContext.estados_reserva.Add(estadoreservaNueva);
                _equiposContext.SaveChanges();

                return Ok(estadoreservaNueva);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult actualizarEstadoreserva(int id, [FromBody] estados_reserva estadoreservaModificar)
        {
            estados_reserva? estadoreservaExiste = (from e in _equiposContext.estados_reserva
                                                    where e.estado_res_id == id
                                                    select e).FirstOrDefault();
            if(estadoreservaExiste == null) { return NotFound(); }

            estadoreservaExiste.estado = estadoreservaModificar.estado;

            _equiposContext.Entry(estadoreservaExiste).State = EntityState.Modified;
            _equiposContext.SaveChanges();

            return Ok(estadoreservaExiste);
        }


        [HttpDelete]
        [Route("delete/{id}")]

        public IActionResult eliminarEstadoreserva(int id)
        {
            estados_reserva? estadoreservaExiste = (from e in _equiposContext.estados_reserva
                                     where e.estado_res_id == id
                                     select e).FirstOrDefault();
            if (estadoreservaExiste == null) return NotFound();


            _equiposContext.estados_reserva.Attach(estadoreservaExiste);
            _equiposContext.estados_reserva.Remove(estadoreservaExiste);
            _equiposContext.SaveChanges();

            return Ok(estadoreservaExiste);
        }


    }
}
