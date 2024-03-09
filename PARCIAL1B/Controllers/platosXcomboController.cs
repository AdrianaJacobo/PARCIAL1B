using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1B.Models;

namespace PARCIAL1B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class platosXcomboController : ControllerBase
    {
        private readonly Parcial1bContext _parcialContexto;

        public platosXcomboController(Parcial1bContext parcialContexto)
        {
            _parcialContexto = parcialContexto;
        }


        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            var listadoPlatosXcombo = (from e in _parcialContexto.PlatoPorCombos
                                 join t in _parcialContexto.Platos
                                         on e.PlatoId equals t.PlatoId
                                 select new
                                 {
                                     e.PlatosPorComboId,
                                     e.EmpresaId,
                                     e.ComboId,
                                     e.Estado,
                                     Nombre_Plato = t.NombrePlato,
                                     Descripcion_Plato = t.DescripcionPlato,
                                     Precio_Plato = t.Costo,
                                     GrupoID = t.GrupoId,
                                     EstadoID = t.EmpresaId
                                 }).ToList();

            if (listadoPlatosXcombo.Count() == 0)
            {
                return NotFound();
            }

            return Ok(listadoPlatosXcombo);
        }


        [HttpPost]
        [Route("Add")]

        public IActionResult AgregarPlatoXcombo([FromBody] PlatoPorCombo platosXcombo)
        {
            try
            {
                _parcialContexto.PlatoPorCombos.Add(platosXcombo);
                _parcialContexto.SaveChanges();

                var agregarnuevo = new
                {
                    empresaID = platosXcombo.EmpresaId,
                    comboID = platosXcombo.ComboId,
                    platoID = platosXcombo.PlatoId,
                    estado = platosXcombo.Estado,
                };
                return Ok(agregarnuevo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]


        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult EliminarPlatoXcombo(int id)
        {
           
            PlatoPorCombo? platoXcombo = (from e in _parcialContexto.PlatoPorCombos
                              where e.PlatosPorComboId == id
                              select e).FirstOrDefault();
            

            if (platoXcombo == null)
            {
                return NotFound();
            }
            //Eliminación del registro
            _parcialContexto.PlatoPorCombos.Attach(platoXcombo);
            _parcialContexto.PlatoPorCombos.Remove();
            _parcialContexto.SaveChanges();

            return Ok(equipo);
        }


        //Listado de los platos y sus elementos al ingresar el id del combo

        [HttpGet]
        [Route("platos-por-combo")]
        public IActionResult ListarPlatosPorCombo(int idCombo)
        {
            var platosPorCombo = (from platoPorCombo in _parcialContexto.PlatoPorCombos
                                  join plato in _parcialContexto.Platos on platoPorCombo.PlatoId equals plato.PlatoId
                                  where platoPorCombo.ComboId == idCombo
                                  select new
                                  {
                                      Plato = plato.NombrePlato,
                                      Descripcion = plato.DescripcionPlato,
                                      Precio = plato.Costo,
                                      Elementos = (from elementosPorPlato in _parcialContexto.ElementosPorPlatos
                                                   join elemento in _parcialContexto.Elementos on elementosPorPlato.ElementoId equals elemento.ElementoId
                                                   where elementosPorPlato.PlatoId == plato.PlatoId
                                                   select new
                                                   {
                                                       NombreElemento = elemento.Elemento1,
                                                       Cantidad = elementosPorPlato.Cantidad,
                                                       UnidadMedida = elemento.UnidadMedida,
                                                       Costo = elemento.Costo
                                                   }).ToList()
                                  }).ToList();

            if (platosPorCombo.Count == 0)
            {
                return NotFound("No se encontraron platos para el combo especificado.");
            }

            return Ok(platosPorCombo);
        }

    }



}

