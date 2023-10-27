using CRUD.Modelos;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Data
{
    public class ClientesAPIDbContext : DbContext
    {
        public ClientesAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Ciente> Clientes { get; set; }
        public DbSet<Tienda> Tiendas { get; set; }
        public DbSet<Articulos> Articulos { get; set; }
        public DbSet<ClientesArticulos> ClientesArticulos { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tienda>()
                .HasMany(c => c.Articulos)
                .WithOne(p => p.Tienda)
                .HasForeignKey(p => p.TiendaId);

            modelBuilder.Entity<ClientesArticulos>()
                .HasKey(ca => new { ca.ClienteId, ca.ArticuloId });

            modelBuilder.Entity<ClientesArticulos>()
                .HasOne(ca => ca.Ciente)
                .WithMany(c => c.ClientesArticulos)
                .HasForeignKey(ca => ca.ClienteId);

            modelBuilder.Entity<ClientesArticulos>()
                .HasOne(ca => ca.Articulos)
                .WithMany(a => a.ClientesArticulos)
                .HasForeignKey(ca => ca.ArticuloId);
        }
    }
}
