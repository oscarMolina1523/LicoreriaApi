using LicoreriaBackend.Models;

namespace LicoreriaBackend.Interfaces
{
    public interface IBodegaRepository
    {
        ICollection<Bodega> GetBodegas();
        Bodega GetBodega(int id);
        bool BodegaExists(int id);
        bool CreateBodega(Bodega bodega);
        bool UpdateBodega(Bodega bodega);
        bool DeleteBodega(Bodega bodega);
        bool Save();
    }
}
