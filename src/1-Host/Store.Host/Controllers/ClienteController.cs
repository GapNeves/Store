using Microsoft.AspNetCore.Mvc;
using Store.AppService.Interfaces;
using Store.Domain.Models;

namespace Store.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClienteAppService _clienteAppService;

    public ClienteController(IClienteAppService clienteAppService)
    {
        _clienteAppService = clienteAppService;
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
    public IActionResult GetById(int id)
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
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        cliente.Id = Guid.NewGuid();
        _clienteAppService.Add(cliente);

        return CreatedAtAction(nameof(GetById), new { id = cliente.Cpf }, cliente);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update(int id, [FromBody] Cliente cliente)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var clienteExistente = _clienteAppService.GetById(id);
        
        if (clienteExistente == null)
            return NotFound(new { message = "Cliente não encontrado" });

        _clienteAppService.Update(cliente);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        var cliente = _clienteAppService.GetById(id);
        
        if (cliente == null)
            return NotFound(new { message = "Cliente não encontrado" });

        _clienteAppService.Delete(id);

        return NoContent();
    }
}
 