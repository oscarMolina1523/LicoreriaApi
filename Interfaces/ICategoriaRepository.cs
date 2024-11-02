using LicoreriaBackend.Models;

namespace LicoreriaBackend.Interfaces
{
    //aca estamos creando los metodos de la interfaz de repository
    public interface ICategoriaRepository
    {
        //esta significa que va retorna una coleccion de categorias
        ICollection<Categoria> GetCategorias();
        //esta significa que va retornar una unica categoria por eso se le pasa un id
        Categoria GetCategoria(int id);
        //esta va retornar o true o false dependiendo si existe o no la categoria
        bool CategoriaExists(int Id_cat);
        //aca vamos a crear la categoria por eso recibe una categoria
        bool CreateCategoria(Categoria categoria);
        //aca vamos a actualizar una categoria por eso recibe una categoria
        bool UpdateCategoria(Categoria categoria);
        //aca vamos a elimianr una categoria por eso recibe una categoria que se va eliminar
        bool DeleteCategoria(Categoria categoria);
        //con este solo guardamos cambios
        bool Save();
    }
}
