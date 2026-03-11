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
        try
        {
            var clientes = _clienteAppService.GetAll();
            return Ok(clientes);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Erro ao processar a solicitação", details = ex.Message });
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Cliente), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        try
        {
            var cliente = _clienteAppService.GetById(id);

            if (cliente == null)
                return NotFound(new { message = "Cliente não encontrado" });

            return Ok(cliente);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Erro ao processar a solicitação", details = ex.Message });
        }
    }

    [HttpGet("cpf/{cpf}")]
    [ProducesResponseType(typeof(Cliente), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetByCpf(string cpf)
    {
        try
        {
            var cliente = _clienteAppService.GetByCpf(cpf);

            if (cliente == null)
                return NotFound(new { message = "Cliente não encontrado" });

            return Ok(cliente);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Erro ao processar a solicitação", details = ex.Message });
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(Cliente), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] Cliente cliente)
    {
        try
        {
            cliente.Id = Guid.NewGuid();
            _clienteAppService.Add(cliente);

            return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, cliente);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Erro ao processar a solicitação", details = ex.Message });
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

            _clienteAppService.Update(cliente);

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Erro ao processar a solicitação", details = ex.Message });
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
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Erro ao processar a solicitação", details = ex.Message });
        }
    }
}
 