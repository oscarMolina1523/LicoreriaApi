using LicoreriaBackend.Data;
using LicoreriaBackend.Interfaces;
using LicoreriaBackend.Models;

namespace LicoreriaBackend.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly DataContext context;

        public ClienteRepository(DataContext context)
        {
            this.context = context;
        }

        public bool ClienteExists(int id)
        {
            return context.Clientes.Any(c => c.IdCliente == id);
        }

        public bool CreateCliente(Cliente cliente)
        {
            context.Add(cliente);
            return Save();
        }

        public bool DeleteCliente(Cliente cliente)
        {
            context.Remove(cliente);
            return Save();
        }

        public Cliente GetCliente(int id)
        {
            return context.Clientes.Where(e => e.IdCliente == id).FirstOrDefault();
        }

        public ICollection<Cliente> GetClientes()
        {
            return context.Clientes.ToList();
        }

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCliente(Cliente cliente)
        {
            context.Update(cliente);
            return Save();
        }
    }

}
