using CRUD.Data;
using CRUD.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace CRUD.Services
{
    public interface IClienteService
    {
        Task<Ciente> GetClienteAsync(Guid id);
        Task<IEnumerable<Ciente>> GetClienteAsync();
        Task<Ciente> CreateClienteAsync(ClienteRequest clienteRequest);
        Task<Ciente> UpdateClienteAsync(Guid id, UdateCleineRequest udateCleineRequest);
        Task<object> DeleteClienteAsync(Guid id);
        Task<string> LoginClienteAsync(LoginRequest loginRequest);
    }

    public class ClienteService : IClienteService
    {
        private readonly ClientesAPIDbContext dbContext;
        
        public ClienteService(ClientesAPIDbContext clientesAPIDbContext)
        {
            dbContext = clientesAPIDbContext;
        }
        public async Task<Ciente> GetClienteAsync(Guid id)
        {
            return await dbContext.Clientes.FindAsync(id);
        }

        public async Task<IEnumerable<Ciente>> GetClienteAsync()
        {

            return await dbContext.Clientes.ToListAsync();
        }

        public async Task<Ciente> CreateClienteAsync(ClienteRequest clienteRequest)
        {
            if (dbContext.Clientes.Any(cliente => cliente.Username == clienteRequest.Username))
            {
                return null;
            }
            var cliente = new Ciente()
            {
                Id = Guid.NewGuid(),
                Username = clienteRequest.Username,
                Contraseña = clienteRequest.Contraseña,
                Nombre = clienteRequest.Nombre,
                Apellidos = clienteRequest.Apellidos,
                Direccion = clienteRequest.Direccion

            };

            await dbContext.Clientes.AddAsync(cliente);
            await dbContext.SaveChangesAsync();

            return cliente;
        }

        public async Task<Ciente> UpdateClienteAsync(Guid id, UdateCleineRequest udateCleineRequest)
        {
            
            var cliente = await dbContext.Clientes.FindAsync(id);
            if (dbContext.Clientes.Any(cli => cli.Username == cliente.Username))
            {
                return null;
            }

            if (cliente != null)
            {
                cliente.Username = udateCleineRequest.Username;
                cliente.Contraseña = udateCleineRequest.Contraseña;
                cliente.Nombre = udateCleineRequest.Nombre;
                cliente.Apellidos = udateCleineRequest.Apellidos;
                cliente.Direccion = udateCleineRequest.Direccion;

                await dbContext.SaveChangesAsync();
                return cliente;
            }

            return null;
        }

        public async Task<object> DeleteClienteAsync(Guid id)
        {
            var cliente = await dbContext.Clientes.FindAsync(id);
            if (cliente != null)
            {
                dbContext.Remove(cliente);
                await dbContext.SaveChangesAsync();
                return cliente;
            }
            return null;
        }

        public async Task<string> LoginClienteAsync(LoginRequest loginRequest)
        {
            var cliente =  dbContext.Clientes.FirstOrDefault(usere => usere.Username == loginRequest.Username);
            if (cliente.Username == loginRequest.Username && cliente.Contraseña == loginRequest.Contraseña)
            {
                return "OK";
            }
            return null;
        }
    }
}
