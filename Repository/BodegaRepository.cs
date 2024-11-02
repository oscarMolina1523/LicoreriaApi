using LicoreriaBackend.Data;
using LicoreriaBackend.Interfaces;
using LicoreriaBackend.Models;

namespace LicoreriaBackend.Repository
{
    public class BodegaRepository : IBodegaRepository
    {
        private readonly DataContext context;

        public BodegaRepository(DataContext context)
        {
            this.context = context;
        }

        public bool BodegaExists(int id)
        {
            return context.Bodegas.Any(c => c.Id_bodega == id);
        }

        public bool CreateBodega(Bodega bodega)
        {
            context.Add(bodega);
            return Save();
        }

        public bool DeleteBodega(Bodega bodega)
        {
            context.Remove(bodega);
            return Save();
        }

        public Bodega GetBodega(int id)
        {
            return context.Bodegas.Where(e => e.Id_bodega == id).FirstOrDefault();
        }

        public ICollection<Bodega> GetBodegas()
        {
            return context.Bodegas.ToList();
        }

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateBodega(Bodega bodega)
        {
            context.Update(bodega);
            return Save();
        }
    }
}
