using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        

    }
}
