using AutoMapper;
using LicoreriaBackend.Dto;
using LicoreriaBackend.Interfaces;
using LicoreriaBackend.Models;
using LicoreriaBackend.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LicoreriaBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        public UsuarioController(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Usuario>))]
        public IActionResult GetUsuarios()
        {

            var usuarios = _mapper.Map<List<UsuarioDto>>(_usuarioRepository.GetUsuarios());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(usuarios);
        }

        [HttpGet("{usuarioId}")]
        [ProducesResponseType(200, Type = typeof(Usuario))]
        [ProducesResponseType(400)]
        public IActionResult GetUsuario(int usuarioId)
        {
            if (!_usuarioRepository.UsuarioExists(usuarioId))
                return NotFound();

            var usuario = _mapper.Map<UsuarioDto>(_usuarioRepository.GetUsuario(usuarioId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(usuario);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUsuario([FromBody] UsuarioDto usuarioCreate)
        {
            if (usuarioCreate == null)
                return BadRequest(ModelState);

            var usuario = _usuarioRepository.GetUsuarios()
                .Where(c => c.Nombre.Trim().ToUpper() == usuarioCreate.Nombre.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (usuario != null)
            {
                ModelState.AddModelError("", "Este usuario ya existe con este nombre");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuarioMap = _mapper.Map<Usuario>(usuarioCreate);

            if (!_usuarioRepository.CreateUsuario(usuarioMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{usuarioId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUsuario(int usuarioId, [FromBody] UsuarioDto updatedUsuario)
        {
            if (updatedUsuario == null)
                return BadRequest(ModelState);

            if (!_usuarioRepository.UsuarioExists(usuarioId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var usuarioMap = _mapper.Map<Usuario>(updatedUsuario);

            usuarioMap.Idusuario = usuarioId;

            if (!_usuarioRepository.UpdateUsuario(usuarioMap))
            {
                ModelState.AddModelError("", "Something went wrong updating usuario");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{usuarioId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUsuario(int usuarioId)
        {
            if (!_usuarioRepository.UsuarioExists(usuarioId))
            {
                return NotFound();
            }

            var usuarioToDelete = _usuarioRepository.GetUsuario(usuarioId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_usuarioRepository.DeleteUsuario(usuarioToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting usuario");
            }

            return NoContent();
        }
    }
}
