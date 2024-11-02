using LicoreriaBackend.Models;

namespace LicoreriaBackend.Interfaces
{
    public interface IClienteRepository
    {
        ICollection<Cliente> GetClientes();
        Cliente GetCliente(int id);
        bool ClienteExists(int id);
        bool CreateCliente(Cliente cliente);
        bool UpdateCliente(Cliente cliente);
        bool DeleteCliente(Cliente cliente);
        bool Save();
    }
}
