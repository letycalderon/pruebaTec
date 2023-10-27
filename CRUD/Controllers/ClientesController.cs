using CRUD.Data;
using CRUD.Modelos;
using CRUD.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CRUD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : Controller
    {
        private readonly ClientesAPIDbContext dbContext;
        private IClienteService clienteService;

        public ClientesController(ClientesAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
            clienteService = new ClienteService(dbContext);
        }



        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            var cliente = clienteService.GetClienteAsync();
            return Ok( await cliente);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetClientes([FromRoute] Guid id)
        {
            var cliente = clienteService.GetClienteAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(await cliente);
        }

        [HttpPost]
        public async Task<IActionResult> SaveClentes(ClienteRequest clienteRequest) 
        {
            var cliente = clienteService.CreateClienteAsync(clienteRequest);
            if (cliente == null)
            {
                return Conflict("Usuario existente");
            }
            return Ok(await cliente);
        }

        [HttpPost]
        [Route("/login")]
        public async Task<string> LoginClentes(LoginRequest loginRequest)
        {
            var cliente = clienteService.LoginClienteAsync(loginRequest);
            if (cliente == null)
            {
                return "Denegado";
            }
            return await cliente;
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UdateClientes([FromRoute] Guid id, UdateCleineRequest udateCleineRequest)
        {
            var cliente = clienteService.UpdateClienteAsync(id, udateCleineRequest);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(await cliente);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteClientes([FromRoute] Guid id)
        {
            var cliente = clienteService.DeleteClienteAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(await cliente);
        }
    }
}
