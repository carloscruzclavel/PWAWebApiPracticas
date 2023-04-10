using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P01WebApi.Models;
using System.ComponentModel.DataAnnotations;

namespace P01WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Estado_equiposController : ControllerBase
    {
        private readonly equiposContext _equiposContext;
        public Estado_equiposController(equiposContext equiposContext) 
        {
            _equiposContext= equiposContext;
        }


        [HttpGet]
        [Route("getall")]
        public IActionResult ObtenerEstadoequipo()
        {
            List<estados_equipo> listadoEstadoequipo = (from e in _equiposContext.estados_equipo
                                                   where e.estado == "A"
                                                   select e).ToList();
            if (listadoEstadoequipo.Count == 0) { return NotFound(); }

            return Ok(listadoEstadoequipo);
        }

        [HttpPost]
        [Route("add")]

        public IActionResult crearEstadoequipo([FromBody] estados_equipo EstadoequipoNuevo)
        {
            try
            {
                _equiposContext.estados_equipo.Add(EstadoequipoNuevo);
                _equiposContext.SaveChanges();

                return Ok(EstadoequipoNuevo);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult actualizarEstadoequipo(int id, [FromBody] estados_equipo EstadoequipoModificar)
        {
            estados_equipo? estadoequipoExiste = (from e in _equiposContext.estados_equipo
                                             where e.id_estados_equipo == id
                                             select e).FirstOrDefault();

            if (estadoequipoExiste == null)
                return NotFound();

            estadoequipoExiste.descripcion = EstadoequipoModificar.descripcion;
            estadoequipoExiste.estado = EstadoequipoModificar.estado;

            _equiposContext.Entry(estadoequipoExiste).State = EntityState.Modified;
            _equiposContext.SaveChanges();

            return Ok(estadoequipoExiste);
        }


        [HttpDelete]
        [Route("delete/{id}")]

        public IActionResult eliminarEstadoequipo(int id)
        {
            estados_equipo? estadoequipoExiste = (from e in _equiposContext.estados_equipo
                                             where e.id_estados_equipo == id
                                             select e).FirstOrDefault();

            if (estadoequipoExiste == null) return NotFound();

            _equiposContext.estados_equipo.Attach(estadoequipoExiste);
            _equiposContext.estados_equipo.Remove(estadoequipoExiste);
            _equiposContext.SaveChanges();

            return Ok(estadoequipoExiste);
        }



    }
}
