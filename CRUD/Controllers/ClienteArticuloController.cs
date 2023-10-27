using CRUD.Data;
using CRUD.Modelos;
using CRUD.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteArticuloController : Controller
    {
        private readonly ClientesAPIDbContext dbContext;
        private IClientesArticulosService clientesArticulosService;

        public ClienteArticuloController(ClientesAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
            clientesArticulosService = new ClientesArticulosService(dbContext);
        }

        [HttpGet]
        public async Task<IActionResult> GetClientesArticulos()
        {
            var ac = clientesArticulosService.GetClientesArticulosAsync();
            return Ok(await ac);
        }

        [HttpGet]
        [Route("{idC:guid}/{idAr:guid}")]
        public async Task<IActionResult> GetClientesArticulos([FromRoute] Guid idC, Guid idAr)
        {
            var ac = clientesArticulosService.GetClientesArticulosAsync(idC, idAr);
            if (ac == null)
            {
                return NotFound();
            }
            return Ok(await ac);
        }

        [HttpPost]
        public async Task<IActionResult> SaveClientesArticulos(ClientesArticulosRequest clientesArticulosRequest)
        {
            var ac = clientesArticulosService.CreateClientesArticulosAsync(clientesArticulosRequest);
            return Ok(await ac);
        }

        [HttpPut]
        [Route("{idC:guid}/{idAr:guid}")]
        public async Task<IActionResult> UdateClientesArticulos([FromRoute] Guid idC, Guid idAr, ClientesArticulosRequest clientesArticulosRequest)
        {
            var ac = clientesArticulosService.UpdateClientesArticulosAsync(idC, idAr, clientesArticulosRequest);
            if (ac == null)
            {
                return NotFound();
            }
            return Ok(await ac);
        }

        [HttpDelete]
        [Route("{idC:guid}/{idAr:guid}")]
        public async Task<IActionResult> DeleteClientesArticulos([FromRoute] Guid idC, Guid idAr)
        {
            var ac = clientesArticulosService.DeleteClientesArticulosAsync(idC, idAr);
            if (ac == null)
            {
                return NotFound();
            }
            return Ok(await ac);
        }
    }
}
