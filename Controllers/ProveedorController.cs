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
    public class ProveedorController : ControllerBase
    {
        private readonly IProveedorRepository _proveedorRepository;
        private readonly IMapper _mapper;
        public ProveedorController(IProveedorRepository proveedorRepository, IMapper mapper)
        {
            _proveedorRepository = proveedorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Proveedor>))]
        public IActionResult GetProveedores()
        {

            var proveedores = _mapper.Map<List<ProveedorDto>>(_proveedorRepository.GetProveedores());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(proveedores);
        }

        [HttpGet("{proveedorId}")]
        [ProducesResponseType(200, Type = typeof(Proveedor))]
        [ProducesResponseType(400)]
        public IActionResult GetProveedor(int proveedorId)
        {
            if (!_proveedorRepository.ProveedorExists(proveedorId))
                return NotFound();

            var proveedor = _mapper.Map<ProveedorDto>(_proveedorRepository.GetProveedor(proveedorId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(proveedor);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProveedor([FromBody] ProveedorDto proveedorCreate)
        {
            if (proveedorCreate == null)
                return BadRequest(ModelState);

            var proveedor = _proveedorRepository.GetProveedores()
                .Where(c => c.Correo.Trim().ToUpper() == proveedorCreate.Correo.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (proveedor != null)
            {
                ModelState.AddModelError("", "Este proveedor ya existe con este correo");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var proveedorMap = _mapper.Map<Proveedor>(proveedorCreate);

            if (!_proveedorRepository.CreateProveedor(proveedorMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{proveedorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProveedor(int proveedorId, [FromBody] ProveedorDto updatedProveedor)
        {
            if (updatedProveedor == null)
                return BadRequest(ModelState);

            if (!_proveedorRepository.ProveedorExists(proveedorId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var proveedorMap = _mapper.Map<Proveedor>(updatedProveedor);

            proveedorMap.Id_proveedor = proveedorId;

            if (!_proveedorRepository.UpdateProveedor(proveedorMap))
            {
                ModelState.AddModelError("", "Something went wrong updating proveedor");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{proveedorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProveedor(int proveedorId)
        {
            if (!_proveedorRepository.ProveedorExists(proveedorId))
            {
                return NotFound();
            }

            var proveedorToDelete = _proveedorRepository.GetProveedor(proveedorId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_proveedorRepository.DeleteProveedor(proveedorToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting proveedor");
            }

            return NoContent();
        }
    }
}
