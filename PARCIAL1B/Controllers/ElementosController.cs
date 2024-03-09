using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PARCIAL1B.Models;
using Microsoft.AspNetCore.Mvc;

namespace PARCIAL1B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElementosController : ControllerBase
    {
        private readonly Parcial1bContext _parcialContexto;

        public ElementosController(Parcial1bContext parcialContexto)
        {
            _parcialContexto = parcialContexto;
        }
        //Get
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Elemento> listadoElementos = (from e in _parcialContexto.Elementos select e).ToList();

            if (listadoElementos.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoElementos);
        }
        //Post
        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarElemento([FromBody] Elemento elemento)
        {
            try
            {
                _parcialContexto.Elementos.Add(elemento);
                _parcialContexto.SaveChanges();
                return Ok(elemento);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Put
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarElemento(int id, [FromBody] Elemento elementoModificar)
        {
            //obtencion de registro original
            Elemento? elementoActual = (from e in _parcialContexto.Elementos    
                                      where e.ElementoId == id
                                      select e).FirstOrDefault();

            //verificacion de existencia del registro segun ID
            if (elementoActual == null)
            {
                return NotFound();
            }
            //Alteración de los campos
            elementoActual.EmpresaId = elementoModificar.EmpresaId;
            elementoActual.Elemento1 = elementoModificar.Elemento1;
            elementoActual.CantidadMinima = elementoModificar.CantidadMinima;
            elementoActual.UnidadMedida = elementoModificar.UnidadMedida;
            elementoActual.Costo = elementoModificar.Costo;
            elementoActual.Estado = elementoModificar.Estado;

            //registro marcado como modificado en el contexto y se envia a la modificacion a la bd
            _parcialContexto.Entry(elementoActual).State = EntityState.Modified;
            _parcialContexto.SaveChanges();

            return Ok(elementoModificar);
        }
        //Delete
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarElemento(int id)
        {
            //Obtención de registro
            Elemento? elemento = (from e in _parcialContexto.Elementos
                                where e.ElementoId == id
                                select e).FirstOrDefault();
            //verificacion de la existencia del registro
            if (elemento == null)
            {
                return NotFound();
            }
            //Eliminación del registro
            _parcialContexto.Elementos.Attach(elemento);
            _parcialContexto.Elementos.Remove(elemento);
            _parcialContexto.SaveChanges();

            return Ok(elemento);
        }
    }
}
