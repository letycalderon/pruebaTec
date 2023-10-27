using CRUD.Data;
using CRUD.Modelos;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Services
{
    public interface ITiendaService
    {
        Task<Tienda> GetTiendaAsync(Guid id);
        Task<IEnumerable<Tienda>> GetTiendaAsync();
        Task<Tienda> CreateTiendaAsync(TiendaRequest tiendaRequest);
        Task<Tienda> UpdateTiendaAsync(Guid id, TiendaRequest tiendaRequest);
        Task<object> DeleteTiendaAsync(Guid id);
    }
    public class TiendaService : ITiendaService
    {
        private readonly ClientesAPIDbContext dbContext;

        public TiendaService(ClientesAPIDbContext clientesAPIDbContext)
        {
            dbContext = clientesAPIDbContext;
        }

        public async Task<Tienda> CreateTiendaAsync(TiendaRequest tiendaRequest)
        {
            var tienda = new Tienda
            {
                Id = Guid.NewGuid(),
                Sucursal = tiendaRequest.Sucursal,
                Dirección = tiendaRequest.Dirección,
                Articulos = new List<Articulos> (tiendaRequest.Articulos)
            };

            await dbContext.Tiendas.AddAsync(tienda);
            await dbContext.SaveChangesAsync();

            return tienda;
        }

        public async Task<object> DeleteTiendaAsync(Guid id)
        {
            var tienda = await dbContext.Tiendas
                .Include(tienda => tienda.Articulos)
                .FirstOrDefaultAsync(tienda => tienda.Id == id);
            if (tienda != null)
            {
                dbContext.Remove(tienda);
                await dbContext.SaveChangesAsync();
                return tienda;
            }
            return null;
        }

        public async Task<Tienda> GetTiendaAsync(Guid id)
        {
            return await dbContext.Tiendas
            .Include(tienda => tienda.Articulos)
            .FirstOrDefaultAsync(tienda => tienda.Id == id);
        }

        public async Task<IEnumerable<Tienda>> GetTiendaAsync()
        {
            return await dbContext.Tiendas.Include(tienda => tienda.Articulos).ToListAsync();
        
        }

        public async Task<Tienda> UpdateTiendaAsync(Guid id, TiendaRequest tiendaRequest)
        {
            var tienda = await dbContext.Tiendas.FindAsync(id);

            if (tienda != null)
            {
                tienda.Sucursal = tiendaRequest.Sucursal;
                tienda.Dirección = tiendaRequest.Dirección;
                tienda.Articulos = new List<Articulos>(tiendaRequest.Articulos);

                await dbContext.SaveChangesAsync();
                return tienda;
            }

            return null;
        }
    }
}
