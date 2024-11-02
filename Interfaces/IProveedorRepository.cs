using LicoreriaBackend.Models;

namespace LicoreriaBackend.Interfaces
{
    public interface IProveedorRepository
    {
        ICollection<Proveedor> GetProveedores();
        Proveedor GetProveedor(int id);
        bool ProveedorExists(int id);
        bool CreateProveedor(Proveedor proveedor);
        bool UpdateProveedor(Proveedor proveedor);
        bool DeleteProveedor(Proveedor proveedor);
        bool Save();
    }
}
