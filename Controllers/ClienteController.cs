using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using LicoreriaBackend.Dto;
using LicoreriaBackend.Models;
using LicoreriaBackend.Interfaces;

namespace LicoreriaBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        public ClienteController(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Cliente>))]
        public IActionResult GetClientes()
        {

            var clientes = _mapper.Map<List<ClienteDto>>(_clienteRepository.GetClientes());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(clientes);
        }

        [HttpGet("{clienteId}")]
        [ProducesResponseType(200, Type = typeof(Cliente))]
        [ProducesResponseType(400)]
        public IActionResult GetCliente(int clienteId)
        {
            if (!_clienteRepository.ClienteExists(clienteId))
                return NotFound();

            var cliente = _mapper.Map<ClienteDto>(_clienteRepository.GetCliente(clienteId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(cliente);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCliente([FromBody] ClienteDto clienteCreate)
        {
            if (clienteCreate == null)
                return BadRequest(ModelState);

            var cliente = _clienteRepository.GetClientes()
                .Where(c => c.Nombre.Trim().ToUpper() == clienteCreate.Nombre.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (cliente != null)
            {
                ModelState.AddModelError("", "Este cliente ya existe");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var clienteMap = _mapper.Map<Cliente>(clienteCreate);

            if (!_clienteRepository.CreateCliente(clienteMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{clienteId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCliente(int clienteId, [FromBody] ClienteDto updatedCliente)
        {
            if (updatedCliente == null)
                return BadRequest(ModelState);

            if (!_clienteRepository.ClienteExists(clienteId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var clienteMap = _mapper.Map<Cliente>(updatedCliente);

            clienteMap.IdCliente = clienteId;

            if (!_clienteRepository.UpdateCliente(clienteMap))
            {
                ModelState.AddModelError("", "Something went wrong updating cliente");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{clienteId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCliente(int clienteId)
        {
            if (!_clienteRepository.ClienteExists(clienteId))
            {
                return NotFound();
            }

            var clienteToDelete = _clienteRepository.GetCliente(clienteId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_clienteRepository.DeleteCliente(clienteToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting cliente");
            }

            return NoContent();
        }

    }
}
