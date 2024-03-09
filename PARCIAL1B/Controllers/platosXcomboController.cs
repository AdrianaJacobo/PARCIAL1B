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



    }
}
