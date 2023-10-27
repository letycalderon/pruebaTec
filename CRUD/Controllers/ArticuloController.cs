using CRUD.Data;
using CRUD.Modelos;
using CRUD.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticuloController : Controller
    {
        private readonly ClientesAPIDbContext dbContext;
        private IArticuloService articuloService;

        public ArticuloController(ClientesAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
            articuloService = new ArticuloService(dbContext);
        }



        [HttpGet]
        public async Task<IActionResult> GetArticulos()
        {
            var articulos = articuloService.GetArticuloAsync();
            return Ok(await articulos);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetArticulos([FromRoute] Guid id)
        {
            var articulo = articuloService.GetArticuloAsync(id);
            if (articulo == null)
            {
                return NotFound();
            }
            return Ok(await articulo);
        }

        [HttpPost]
        public async Task<IActionResult> SaveArticulos(ArticuloRequest articuloRequest)
        {
            var articulo = articuloService.CreateArticuloAsync(articuloRequest);
            return Ok(await articulo);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UdateArticulos([FromRoute] Guid id, ArticuloRequest articuloRequest)
        {
            var articulo = articuloService.UpdateArticuloAsync(id, articuloRequest);
            if (articulo == null)
            {
                return NotFound();
            }
            return Ok(await articulo);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteArticulos([FromRoute] Guid id)
        {
            var articulo = articuloService.DeleteArticuloAsync(id);
            if (articulo == null)
            {
                return NotFound();
            }
            return Ok(await articulo);
        }
    }
}
