using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P01WebApi.Models;

namespace P01WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrerasController : ControllerBase
    {
        public readonly equiposContext _equiposContext;

        public CarrerasController(equiposContext equiposContext)
        {
            _equiposContext = equiposContext;
        }



        [HttpGet]
        [Route("getall")]

        public IActionResult ObtenerCarreras()
        {
            var listadocarreras = (from e in _equiposContext.carreras
                                              join t in _equiposContext.facultades
                                              on e.facultad_id equals t.facultad_id
                                              select new
                                              {
                                                  e.carrera_id,
                                                  e.nombre_carrera,
                                                  nombre_de_facultad= t.nombre_facultad
                                              }).ToList();

            if (listadocarreras.Count == 0) { return NotFound(); }

            return Ok(listadocarreras);

        }

        [HttpPost]
        [Route("add/{id}")]

        public IActionResult crearCarrera([FromBody] carreras carreraNuevo)
        {
            try
            {
                _equiposContext.carreras.Add(carreraNuevo);
                _equiposContext.SaveChanges();

                return Ok(carreraNuevo);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult actualizarCarrera(int id, [FromBody] carreras carreraModificar)
        {
            carreras? carreraExiste = (from e in _equiposContext.carreras
                                     where e.carrera_id == id
                                     select e).FirstOrDefault();

            if (carreraExiste == null)
                return NotFound();

            carreraExiste.nombre_carrera = carreraModificar.nombre_carrera;
            carreraExiste.facultad_id = carreraModificar.facultad_id;

            _equiposContext.Entry(carreraExiste).State = EntityState.Modified;
            _equiposContext.SaveChanges();

            return Ok(carreraExiste);
        }


        [HttpDelete]
        [Route("delete/{id}")]

        public IActionResult eliminarCarrera(int id)
        {
            carreras? carreraExiste = (from e in _equiposContext.carreras
                                     where e.carrera_id == id
                                     select e).FirstOrDefault();
            if (carreraExiste == null) return NotFound();

            _equiposContext.carreras.Attach(carreraExiste);
            _equiposContext.carreras.Remove(carreraExiste);
            _equiposContext.SaveChanges();

            return Ok(carreraExiste);
        }



    }
}
