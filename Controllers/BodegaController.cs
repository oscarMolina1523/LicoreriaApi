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
    public class BodegaController : ControllerBase
    {
        private readonly IBodegaRepository _bodegaRepository;
        private readonly IMapper _mapper;
        public BodegaController(IBodegaRepository bodegaRepository, IMapper mapper)
        {
            _bodegaRepository = bodegaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Bodega>))]
        public IActionResult GetBodegas()
        {

            var bodegas = _mapper.Map<List<BodegaDto>>(_bodegaRepository.GetBodegas());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bodegas);
        }

        [HttpGet("{bodegaId}")]
        [ProducesResponseType(200, Type = typeof(Bodega))]
        [ProducesResponseType(400)]
        public IActionResult GetBodega(int bodegaId)
        {
            if (!_bodegaRepository.BodegaExists(bodegaId))
                return NotFound();

            var bodega = _mapper.Map<BodegaDto>(_bodegaRepository.GetBodega(bodegaId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bodega);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateBodega([FromBody] BodegaDto bodegaCreate)
        {
            if (bodegaCreate == null)
                return BadRequest(ModelState);

            var bodega = _bodegaRepository.GetBodegas()
                .Where(c => c.Id_producto == bodegaCreate.Id_producto)
                .FirstOrDefault();

            if (bodega != null)
            {
                ModelState.AddModelError("", "Este producto ya existe en bodega");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bodegaMap = _mapper.Map<Bodega>(bodegaCreate);

            if (!_bodegaRepository.CreateBodega(bodegaMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{bodegaId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateBodega(int bodegaId, [FromBody] BodegaDto updatedBodega)
        {
            if (updatedBodega == null)
                return BadRequest(ModelState);

            if (!_bodegaRepository.BodegaExists(bodegaId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var bodegaMap = _mapper.Map<Bodega>(updatedBodega);

            bodegaMap.Id_bodega = bodegaId;

            if (!_bodegaRepository.UpdateBodega(bodegaMap))
            {
                ModelState.AddModelError("", "Something went wrong updating bodega");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{bodegaId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteBodega(int bodegaId)
        {
            if (!_bodegaRepository.BodegaExists(bodegaId))
            {
                return NotFound();
            }

            var bodegaToDelete = _bodegaRepository.GetBodega(bodegaId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_bodegaRepository.DeleteBodega(bodegaToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting bodega");
            }

            return NoContent();
        }
    }
}
