using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P01WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace P01WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcasController : ControllerBase
    {
        private readonly equiposContext _equiposContext;
        public MarcasController(equiposContext equiposContext)
        {
            _equiposContext = equiposContext;
        }



        [HttpGet]
        [Route("getall")]
        public IActionResult ObtenerMarcas()
        {
            List<marcas> listadoMarcas = (from e in _equiposContext.marcas
                                          where e.estados == "A"
                                          select e).ToList();
            if (listadoMarcas.Count == 0) { return NotFound(); }

            return Ok(listadoMarcas);
        }


        [HttpPost]
        [Route("add")]

        public IActionResult crear([FromBody] marcas marcaNueva)
        {
            try
            {
                marcaNueva.estados = "A";
                _equiposContext.marcas.Add(marcaNueva);
                _equiposContext.SaveChanges();

                return Ok(marcaNueva);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult actualizarMarca(int id, [FromBody]marcas marcaModificar)
        {
            marcas? marcaExiste = (from e in _equiposContext.marcas
                                      where e.id_marcas == id
                                      select e).FirstOrDefault();

            if (marcaExiste == null)
                return NotFound();

            marcaExiste.nombre_marca = marcaModificar.nombre_marca;
            marcaExiste.estados = marcaModificar.estados;

            _equiposContext.Entry(marcaExiste).State = EntityState.Modified;
            _equiposContext.SaveChanges();

            return Ok(marcaExiste);
        }


        [HttpDelete]
        [Route("delete/{id}")]

        public IActionResult eliminarMarca(int id)
        {
            marcas? marcaExiste = (from e in _equiposContext.marcas
                                     where e.id_marcas == id
                                     select e).FirstOrDefault();

            if (marcaExiste == null) return NotFound();

            marcaExiste.estados = "I";

            _equiposContext.Entry(marcaExiste).State = EntityState.Modified;
            //_equiposContext.equipos.Attach(equipoExiste);
            //_equiposContext.equipos.Remove(equipoExiste);
            _equiposContext.SaveChanges();

            return Ok(marcaExiste);
        }


    }
}
