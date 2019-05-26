using Microsoft.AspNetCore.Mvc;
using Superdigital.Domain.Entities;
using Superdigital.Service.Lancamento;
using System.Linq;

namespace Superdigital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LancamentosController : ControllerBase
    {
        [Route("~/api/Superdigital")]
        [HttpPost]
        public IActionResult Post([FromBody] TransacaoEntity data)
        {
            var lancamento = new TransacaoApp().Lancamento(data);
            if (lancamento.Success)
            {
                return Ok();
            }
            else if (lancamento.Errors.Any())
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
