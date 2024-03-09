using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1B.Models;

namespace PARCIAL1B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElementosPorPlatoController : ControllerBase
    {
        private readonly Parcial1bContext _parcialContexto;

        public ElementosPorPlatoController(Parcial1bContext parcialContexto)
        {
            _parcialContexto = parcialContexto;
        }
        //Get
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            var listadoElementosP = (from e in _parcialContexto.ElementosPorPlatos
                                 join t in _parcialContexto.Platos
                                        on e.PlatoId equals t.PlatoId
                                 join m in _parcialContexto.Elementos
                                        on e.ElementoId equals m.ElementoId
                                 select new
                                 {
                                     e.ElementoPorPlatoId,
                                     e.EmpresaId,
                                     Plato = t.NombrePlato,
                                     Elemento = m.Elemento1,
                                     e.Cantidad,
                                     e.Estado,
                                 }).ToList();

            if (listadoElementosP.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoElementosP);
        }
        //Post
        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarElementoPorPlato([FromBody] ElementosPorPlato elementosPorPlato)
        {
            try
            {
                _parcialContexto.ElementosPorPlatos.Add(elementosPorPlato);
                _parcialContexto.SaveChanges();

                var elementoP = new
                {
                    EmpresaId = elementosPorPlato.EmpresaId,
                    PlatoId = elementosPorPlato.PlatoId,
                    ElementoId = elementosPorPlato.ElementoId,
                    Cantidad = elementosPorPlato.Cantidad,
                    Estado = elementosPorPlato.Estado
                };
                return Ok(elementoP);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Put
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarElementoPorPlato(int id, [FromBody] ElementosPorPlato elementosPModificar)
        {
            //obtencion de registro original
            ElementosPorPlato? elementoPActual = (from e in _parcialContexto.ElementosPorPlatos
                                    where e.ElementoPorPlatoId == id
                                    select e).FirstOrDefault();

            //verificacion de existencia del registro segun ID
            if (elementoPActual == null)
            {
                return NotFound();
            }
            //Alteración de los campos
            elementoPActual.EmpresaId = elementosPModificar.EmpresaId;
            elementoPActual.Cantidad = elementosPModificar.Cantidad;
            elementoPActual.Estado = elementosPModificar.Estado;
            //registro marcado como modificado en el contexto y se envia a la modificacion a la bd
            _parcialContexto.Entry(elementoPActual).State = EntityState.Modified;
            _parcialContexto.SaveChanges();

            return Ok(elementosPModificar);
        }
        //Delete
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarElementoPorPlato(int id)
        {
            //Obtención de registro
            ElementosPorPlato? elementoP = (from e in _parcialContexto.ElementosPorPlatos
                              where e.ElementoPorPlatoId == id
                              select e).FirstOrDefault();
            //verificacion de la existencia del registro
            if (elementoP == null)
            {
                return NotFound();
            }
            //Eliminación del registro
            _parcialContexto.ElementosPorPlatos.Attach(elementoP);
            _parcialContexto.ElementosPorPlatos.Remove(elementoP);
            _parcialContexto.SaveChanges();

            return Ok(elementoP);
        }
    }
}
