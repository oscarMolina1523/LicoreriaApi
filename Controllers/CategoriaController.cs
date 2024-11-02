using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using LicoreriaBackend.Dto;
using LicoreriaBackend.Models;
using LicoreriaBackend.Interfaces;

namespace LicoreriaBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriaController : ControllerBase
    {
        //primero mandamos a llamar a la interfaz que conecta con el servivio de repositoty 
        private readonly ICategoriaRepository _categoriaRepository;
        //luego mandamos a llamar al mapper ya que sin el no podremos convertir las entidades 
        private readonly IMapper _mapper;

        //esto se le llama injeccion de dependencia para que solo exista una unica instancia
        public CategoriaController(ICategoriaRepository categoriaRepository, IMapper mapper)
        {
            //y le pasamos la instancia que recibimos de la web a la local
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        //esta es la etique que simboliza que solo pedimos lo datos
        [HttpGet]
        //y esto nos dice que es lo que vamos a recibir es decir una lista de categorias
        [ProducesResponseType(200, Type = typeof(IEnumerable<Categoria>))]
        //este es el metodo que usamos 
        public IActionResult GetCategorias()
        {
            //primeor mandamos a hacer el mapeo de conversion de Categoria a CategoriaDto en el mapper y 
            //haciendo uso de la instancia de la interfaz repositorio accedemos al metodo de 
            //GetCategorias que trae toda la lista de las categorias
            var categories = _mapper.Map<List<CategoriaDto>>(_categoriaRepository.GetCategorias());

            //validamos si el modelado no es valido retornamos una respuesta de incorrecto
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //pero si todo esta bien retornamos la lista de categorias
            return Ok(categories);
        }

        //esta etiqueta tambien es de obtener datos pero si vemos pide un valor que es el id de la categoria 
        //que vamos a buscar
        [HttpGet("{categoriaId}")]
        //aca decimos que unicamente se retorna una unica entidad no dos sino una de categoria
        [ProducesResponseType(200, Type = typeof(Categoria))]
        [ProducesResponseType(400)]
        //igualmente este es el metodo que usamos y recibimos el id que nos pasan en la web de tipo entero
        public IActionResult GetCategoria(int categoriaId)
        {
            //validamos con el metodo de la interfza de repository con el metodo que hay 
            //para ver si no existe y le pasamos el id, si existe se salta esto
            if (!_categoriaRepository.CategoriaExists(categoriaId))
                return NotFound();

            //aca ya sabemos que si existe y haciendo uso del mapper mapeamos de entidad completa a dto 
            //para no mostrar el id de la categoria que es una buena practica de programacion y lo mandamos a 
            //llamar con el metodo getCategoria que devuelve solo una categoria que es la que le estamos pasando
            //el id 
            var category = _mapper.Map<CategoriaDto>(_categoriaRepository.GetCategoria(categoriaId));

            //validamos si no se mapeo bien la transformacion, si se mapeo bien se salta esto
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //si todo esta bien devuelve la categoria obtenida en repository
            return Ok(category);
        }

        //esta etiqueta simboliza que vamos a mandar datos 
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        //este metodo es el que usaremos y recibe una entidad de categoria dto el cual no pide un id
        public IActionResult CreateCategory([FromBody] CategoriaDto categoriaCreate)
        {
            //validamos si lo que nos mandan es nulo devolvemos una respuesta de error si todo esta bien se 
            //salta esto
            if (categoriaCreate == null)
                return BadRequest(ModelState);

            //ya aca primero vemos si la categoria ya existe comparando el campo de descripcion de los de la base
            //de datos y el que le estoy pasando desde la web
            var category = _categoriaRepository.GetCategorias()
                .Where(c => c.Descripcion.Trim().ToUpper() == categoriaCreate.Descripcion.TrimEnd().ToUpper())
                .FirstOrDefault();

            //valisamos si ya existe una categoria con esta descripcion le decimos que ya existe y no dejamos
            //qeu se inserte nuevamente
            if (category != null)
            {
                ModelState.AddModelError("", "Esta categoria ya existe");
                return StatusCode(422, ModelState);
            }

            //si el modelo no es valido mandamos error 
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //si pasamos todo eso y no existe aun se mapea de dto a entidad completa con el mapper
            //pasandole lo que le mandamos desde la web
            var categoryMap = _mapper.Map<Categoria>(categoriaCreate);

            //ahora haciendo uso de la interfaz de repository mandamos a llamar el metodo de createCategoria y 
            //le pasamos la categoria mapeada
            if (!_categoriaRepository.CreateCategoria(categoryMap))
            {
                //si no se crea de manera correcta o hay un problema da este error 
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            //si todo paso bien retorna el mensaje de ok
            return Ok("Successfully created");
        }

        //esta etiqueta simboliza que vamos a hacer una actualizacion
        [HttpPut("{categoriaId}")]
        //estas son los tipos de respuestas
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        //este es el metodo que usaremos para obtener los datos de la web
        //donde recibimos un id y el dto que es el cuerpo que vamos a actualizar
        public IActionResult UpdateCategory(int categoriaId, [FromBody] CategoriaDto updatedCategory)
        {
            //validamos si el cuerpo que vamos a actualizar es nulo que mande un error
            if (updatedCategory == null)
                return BadRequest(ModelState);

            //validamos si la categoria no existe , si no existe mandamos error, si existe se salta esto
            if (!_categoriaRepository.CategoriaExists(categoriaId))
                return NotFound();

            //validamos si el modelo no es valido mandamos error
            if (!ModelState.IsValid)
                return BadRequest();
            
            //procedemos a mapear de dto a entidad completa y le pasamos el cuerpo de la web
            var categoryMap = _mapper.Map<Categoria>(updatedCategory);
            //aca le pasamos el id de la categoria que queremos actualizar sino da error
            categoryMap.CodigoCategoria = categoriaId;

            //hacemos uso de la interfaz de repository y uso del metodo de update para actualizar
            //y le pasamos la categoria mapeada ya, validamos si se hace bien
            if (!_categoriaRepository.UpdateCategoria(categoryMap))
            {
                //si ocurre un error retorna el error
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            //si todo esta correcto no retorna nada pero sin errores
            return NoContent();
        }

        //esta etiqueta simboliza que vamos a eliminar y recibe un valor de id de quien vamos a borrar
        [HttpDelete("{categoriaId}")]
        //tipos de respuestas
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        //este es el metodo que vamos a usar y recibimos el id de la web de quien vamos a borrar
        public IActionResult DeleteCategory(int categoriaId)
        {
            //validamos si no existe la categoria que vamos a borrar, si existe se salta esto pero sino se regresa
            //porque no hay nadie a quien borrar
            if (!_categoriaRepository.CategoriaExists(categoriaId))
            {
                return NotFound();
            }
            //aca hacemos uso de la interfaxz de repository y mandamos a obtener la categoria que vamos a borrar
            //y le pasamos el id para saber quien debe ser
            var categoryToDelete = _categoriaRepository.GetCategoria(categoriaId);

            //validamos que el modelo sea correcto
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //y mandamos a borrar con el uso de la interfaz de repository le pasmos la categoria y si
            //algo sale mal retorna error
            if (!_categoriaRepository.DeleteCategoria(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            //si todo salio bien no retorna nada pero sin errores
            return NoContent();
        }


    }
}
