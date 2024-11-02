using LicoreriaBackend.Interfaces;
using LicoreriaBackend.Data;
using LicoreriaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace Licoreria.Repository
{
    //aca esta clase esta implementando la interfaz de repository con sus metodos 
    public class CategoriaRepository : ICategoriaRepository
    {
        //primero recibimos el data context qie es donde esta la info de la base de datos
        private readonly DataContext context;

        //se lo pasamos al local 
        public CategoriaRepository(DataContext context)
        {
            this.context = context;
        }

        //metodo de validacion de si existe la categoria
        public bool CategoriaExists(int Id_cat)
        {
            //busca en todas las categorias si existe alguna con el mismo id que estamos recibiendo
            return context.Categorias.Any(c => c.CodigoCategoria == Id_cat);
        }

        //metodo de crear categoria
        public bool CreateCategoria(Categoria categoria)
        {
            //agrega unicamete en la base de datos la categoria
            context.Add(categoria);
            //guarda los cambios
            return Save();
        }

        //metodo de eliminar la categoria
        public bool DeleteCategoria(Categoria categoria)
        {
            //elimina la categoria qye le estan pasando
            context.Remove(categoria);
            //guarda los cambios
            return Save();
        }

        //metodo de obtener una unica categoria y recibe un id
        public Categoria GetCategoria(int id)
        {
            //retorna una categoria qe el codigo de categoria sea igual al id 
            //que recbimos el primero que salga
            return context.Categorias.Where(e => e.CodigoCategoria == id).FirstOrDefault();
        }

        //metodo de retornar una coleccionde categorias
        public ICollection<Categoria> GetCategorias()
        {
            //retorna una lista de categorias
            return context.Categorias.ToList();
        }

        //metodo de guardar cambios unicamente
        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }

        //metodo de actualizar una categoria recibe una categoria
        public bool UpdateCategoria(Categoria categoria)
        {
            //actualiza y guarda
            context.Update(categoria);
            return Save();
        }
    }
}
