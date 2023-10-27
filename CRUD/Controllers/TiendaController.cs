using CRUD.Data;
using CRUD.Modelos;
using CRUD.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CRUD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TiendaController : Controller
    {
        private readonly ClientesAPIDbContext dbContext;
        private ITiendaService tiendaService;

        public TiendaController(ClientesAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
            tiendaService = new TiendaService(dbContext);
        }

        [HttpGet]
        public async Task<IActionResult> GetTiendas()
        {
            var tiendas = tiendaService.GetTiendaAsync();
            return Ok(await tiendas);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetTienda([FromRoute] Guid id)
        {
            var tienda = tiendaService.GetTiendaAsync(id);
            if (tienda == null)
            {
                return NotFound();
            }
            return Ok(await tienda);
        }

        [HttpPost]
        public async Task<IActionResult> SaveTienda(string tiendaRequest)
        {
            var tienda = tiendaService.CreateTiendaAsync(JsonConvert.DeserializeObject<TiendaRequest>(tiendaRequest));
            return Ok(await tienda);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UdateTienda([FromRoute] Guid id, string tiendaRequest)
        {
            var tienda = tiendaService.UpdateTiendaAsync(id, JsonConvert.DeserializeObject<TiendaRequest>(tiendaRequest));
            if (tienda == null)
            {
                return NotFound();
            }
            return Ok(await tienda);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteTienda([FromRoute] Guid id)
        {
            var tienda = tiendaService.DeleteTiendaAsync(id);
            if (tienda == null)
            {
                return NotFound();
            }
            return Ok(await tienda);
        }
    }
}
