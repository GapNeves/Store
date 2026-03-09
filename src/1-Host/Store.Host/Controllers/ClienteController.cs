using Microsoft.AspNetCore.Mvc;
using Store.AppService.Interfaces;
using Store.Domain;
using Store.Domain.Models;

namespace Store.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClienteAppService _clienteAppService;
    private readonly ClienteService _clienteService;

    public ClienteController(IClienteAppService clienteAppService, ClienteService clienteService)
    {
        _clienteAppService = clienteAppService;
        _clienteService = clienteService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Cliente>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var clientes = _clienteAppService.GetAll();
        return Ok(clientes);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Cliente), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var cliente = _clienteAppService.GetById(id);
        
        if (cliente == null)
            return NotFound(new { message = "Cliente não encontrado" });

        return Ok(cliente);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Cliente), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] Cliente cliente)
    {
        try
        {
            cliente.Id = Guid.NewGuid();
            _clienteService.ValidarEAdicionar(cliente);

            return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, cliente);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update(Guid id, [FromBody] Cliente cliente)
    {
        try
        {
            var clienteExistente = _clienteAppService.GetById(id);
        
            if (clienteExistente == null)
                return NotFound(new { message = "Cliente não encontrado" });

            _clienteService.ValidarEAtualizar(cliente);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id)
    {
        try
        {
            var cliente = _clienteAppService.GetById(id);
        
            if (cliente == null)
                return NotFound(new { message = "Cliente não encontrado" });

            _clienteAppService.Delete(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
 