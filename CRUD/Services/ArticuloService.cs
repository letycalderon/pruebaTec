using CRUD.Data;
using CRUD.Modelos;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Services
{
    public interface IArticuloService
    {
        Task<Articulos> GetArticuloAsync(Guid id);
        Task<IEnumerable<Articulos>> GetArticuloAsync();
        Task<Articulos> CreateArticuloAsync(ArticuloRequest articuloRequest);
        Task<Articulos> UpdateArticuloAsync(Guid id, ArticuloRequest articuloRequest);
        Task<object> DeleteArticuloAsync(Guid id);
    }
    public class ArticuloService : IArticuloService
    {
        private readonly ClientesAPIDbContext dbContext;

        public ArticuloService(ClientesAPIDbContext clientesAPIDbContext)
        {
            dbContext = clientesAPIDbContext;
        }

        public async Task<Articulos> CreateArticuloAsync(ArticuloRequest articuloRequest)
        {
            var articulo = new Articulos()
            {
                Id = Guid.NewGuid(),
                Codigo = articuloRequest.Codigo,
                Descripcion = articuloRequest.Descripcion,
                Precio = articuloRequest.Precio,
                Imagen = articuloRequest.Imagen,
                Stock = articuloRequest.Stock,
                TiendaId = articuloRequest.TiendaId
            };

            await dbContext.Articulos.AddAsync(articulo);
            await dbContext.SaveChangesAsync();

            return articulo;
        }

        public async Task<object> DeleteArticuloAsync(Guid id)
        {
            var articulo = await dbContext.Articulos.FindAsync(id);
            if (articulo != null)
            {
                dbContext.Remove(articulo);
                await dbContext.SaveChangesAsync();
                return articulo;
            }
            return null;
        }

        public async Task<Articulos> GetArticuloAsync(Guid id)
        {
            return await dbContext.Articulos.FindAsync(id);
        }

        public async Task<IEnumerable<Articulos>> GetArticuloAsync()
        {
            return await dbContext.Articulos.ToListAsync();
        }

        public async Task<Articulos> UpdateArticuloAsync(Guid id, ArticuloRequest articuloRequest)
        {
            var articulo = await dbContext.Articulos.FindAsync(id);

            if (articulo != null)
            {
                articulo.Codigo = articuloRequest.Codigo;
                articulo.Descripcion = articuloRequest.Descripcion;
                articulo.Precio = articuloRequest.Precio;
                articulo.Imagen = articuloRequest.Imagen;
                articulo.Stock = articuloRequest.Stock;
                articulo.TiendaId = articuloRequest.TiendaId;

                await dbContext.SaveChangesAsync();
                return articulo;
            }

            return null;
        }
    }
}
