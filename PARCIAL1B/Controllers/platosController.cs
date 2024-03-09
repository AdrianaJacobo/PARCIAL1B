using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1B.Models;


namespace PARCIAL1B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class platosController : ControllerBase
    {
        private readonly Parcial1bContext _parcialContexto;

        public platosController(Parcial1bContext parcialContexto)
        {
            _parcialContexto = parcialContexto;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<Plato> listadoPlatos = (from e in _parcialContexto.Platos
                                          select e).ToList();

            if (listadoPlatos.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoPlatos);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarPlato([FromBody] Plato plato)
        {
            try
            {
                _parcialContexto.Platos.Add(plato);
                _parcialContexto.SaveChanges();
                return Ok(plato);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }


        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarPlato(int id, [FromBody] Plato platoModificar)
        {
            Plato? platoActual = (from e in _parcialContexto.Platos
                                    where e.PlatoId == id
                                    select e).FirstOrDefault();

            if (platoActual == null)
            {
                return NotFound();
            }

            platoActual.NombrePlato = platoModificar.NombrePlato;
            platoActual.DescripcionPlato = platoModificar.DescripcionPlato;
            platoActual.Costo = platoModificar.Costo; //ese es precio, no costo, pero se fue costo
            platoActual.EmpresaId = platoModificar.EmpresaId;
            platoActual.GrupoId = platoModificar.GrupoId;


            _parcialContexto.Entry(platoActual).State = EntityState.Modified;
            _parcialContexto.SaveChanges();

            return Ok(platoModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult EliminarPlato(int id)
        {
            Plato? plato = (from e in _parcialContexto.Platos
                              where e.PlatoId == id
                              select e).FirstOrDefault();

            if (plato == null)
            {
                return NotFound();
            }

            _parcialContexto.Platos.Attach(plato);
            _parcialContexto.Platos.Remove(plato);
            _parcialContexto.SaveChanges();

            return Ok(plato);

        }


    }
}
