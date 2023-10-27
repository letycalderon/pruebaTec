using CRUD.Data;
using CRUD.Modelos;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Services
{
    public interface IClientesArticulosService
    {
        Task<ClientesArticulos> GetClientesArticulosAsync(Guid idC, Guid idAr);
        Task<IEnumerable<ClientesArticulos>> GetClientesArticulosAsync();
        Task<ClientesArticulos> CreateClientesArticulosAsync(ClientesArticulosRequest clientesArticulosRequest);
        Task<ClientesArticulos> UpdateClientesArticulosAsync(Guid idC, Guid idAr, ClientesArticulosRequest clientesArticulosRequest);
        Task<object> DeleteClientesArticulosAsync(Guid idC, Guid idAr);
    }
    public class ClientesArticulosService : IClientesArticulosService
    {
        private readonly ClientesAPIDbContext dbContext;

        public ClientesArticulosService(ClientesAPIDbContext clientesAPIDbContext)
        {
            dbContext = clientesAPIDbContext;
        }

        public async Task<ClientesArticulos> CreateClientesArticulosAsync(ClientesArticulosRequest clientesArticulosRequest)
        {
            var ac = new ClientesArticulos()
            {
                Id = Guid.NewGuid(),
                ArticuloId = clientesArticulosRequest.ArticuloId,
                ClienteId = clientesArticulosRequest.ClienteId
            };

            await dbContext.ClientesArticulos.AddAsync(ac);
            await dbContext.SaveChangesAsync();

            return ac;
        }

        public async Task<object> DeleteClientesArticulosAsync(Guid idC, Guid idAr)
        {
            var ac = await dbContext.ClientesArticulos.FindAsync(idC, idAr);
            if (ac != null)
            {
                dbContext.Remove(ac);
                await dbContext.SaveChangesAsync();
                return ac;
            }
            return null;
        }

        public async Task<ClientesArticulos> GetClientesArticulosAsync(Guid idC, Guid idAr)
        {
            return await dbContext.ClientesArticulos.FindAsync(idC, idAr);
        }

        public async Task<IEnumerable<ClientesArticulos>> GetClientesArticulosAsync()
        {
            return await dbContext.ClientesArticulos.ToListAsync();
        }

        public async Task<ClientesArticulos> UpdateClientesArticulosAsync(Guid idC, Guid idAr, ClientesArticulosRequest clientesArticulosRequest)
        {
            var ac = await dbContext.ClientesArticulos.FindAsync(idC, idAr);

            if (ac != null)
            {
                ac.ArticuloId = clientesArticulosRequest.ArticuloId;
                ac.ClienteId = clientesArticulosRequest.ClienteId;

                await dbContext.SaveChangesAsync();
                return ac;
            }

            return null;
        }
    }
}
