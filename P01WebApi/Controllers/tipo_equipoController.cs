using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P01WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace P01WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class tipo_equipoController : ControllerBase
    {

        private readonly equiposContext _equiposContext;
        public tipo_equipoController(equiposContext equiposContext)
        {
            _equiposContext= equiposContext;
        }


        [HttpGet]
        [Route("getall")]
        public IActionResult ObtenerMarcas()
        {
            List<tipo_equipo> listadoTipoequipo = (from e in _equiposContext.tipo_equipo
                                          select e).ToList();
            if (listadoTipoequipo.Count == 0) { return NotFound(); }

            return Ok(listadoTipoequipo);
        }



        [HttpPost]
        [Route("add")]

        public IActionResult crearTipoequipo([FromBody] tipo_equipo tipoequipoNuevo)
        {
            try
            {
                _equiposContext.tipo_equipo.Add(tipoequipoNuevo);
                _equiposContext.SaveChanges();

                return Ok(tipoequipoNuevo);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult actualizarTequipo(int id, [FromBody] tipo_equipo tipoequipoModificar)
        {
            tipo_equipo? tipoequipoExiste = (from e in _equiposContext.tipo_equipo
                                   where e.id_tipo_equipo == id
                                   select e).FirstOrDefault();

            if (tipoequipoExiste == null)
                return NotFound();

            tipoequipoExiste.descripcion = tipoequipoModificar.descripcion;
            tipoequipoExiste.estado = tipoequipoModificar.estado;

            _equiposContext.Entry(tipoequipoExiste).State = EntityState.Modified;
            _equiposContext.SaveChanges();

            return Ok(tipoequipoExiste);
        }



        [HttpDelete]
        [Route("delete/{id}")]

        public IActionResult eliminarTipoequipo(int id)
        {
            tipo_equipo? tipoequipoExiste = (from e in _equiposContext.tipo_equipo
                                   where e.id_tipo_equipo == id
                                   select e).FirstOrDefault();

            if (tipoequipoExiste == null) return NotFound();

            _equiposContext.tipo_equipo.Attach(tipoequipoExiste);
            _equiposContext.tipo_equipo.Remove(tipoequipoExiste);
            _equiposContext.SaveChanges();

            return Ok(tipoequipoExiste);
        }


    }
}
