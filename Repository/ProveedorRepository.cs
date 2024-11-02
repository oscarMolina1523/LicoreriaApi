using LicoreriaBackend.Data;
using LicoreriaBackend.Interfaces;
using LicoreriaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace LicoreriaBackend.Repository
{
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly DataContext context;

        public ProveedorRepository(DataContext context)
        {
            this.context = context;
        }

        public bool CreateProveedor(Proveedor proveedor)
        {
            context.Add(proveedor);
            return Save();
        }

        public bool DeleteProveedor(Proveedor proveedor)
        {
            context.Remove(proveedor);
            return Save();
        }

        public Proveedor GetProveedor(int id)
        {
            return context.Proveedores.Where(e => e.Id_proveedor == id).FirstOrDefault();
        }

        public ICollection<Proveedor> GetProveedores()
        {
            return context.Proveedores.ToList();
        }

        public bool ProveedorExists(int id)
        {
            return context.Proveedores.Any(c => c.Id_proveedor == id);
        }

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false; ;
        }

        public bool UpdateProveedor(Proveedor proveedor)
        {
            context.Update(proveedor);
            return Save();
        }
    }
}
