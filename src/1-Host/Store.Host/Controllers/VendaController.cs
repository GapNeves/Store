using Microsoft.AspNetCore.Mvc;
using Store.AppService.Interfaces;
using Store.Domain.Models;

namespace Store.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VendaController : ControllerBase
{
    private readonly IVendaAppService _vendaAppService;

    public VendaController(IVendaAppService vendaAppService)
    {
        _vendaAppService = vendaAppService;

    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Venda>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        try
        {
            var vendas = _vendaAppService.GetAll();
            return Ok(vendas);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Erro ao processar a solicitação", details = ex.Message });
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Venda), StatusCodes.Status200OK)]
    public IActionResult GetById(Guid id)
    {
        try
        {
            var venda = _vendaAppService.GetById(id);

            if (venda == null)
                return NotFound(new { message = "Venda não encontrada" });

            return Ok(venda);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Erro ao processar a solicitação", details = ex.Message });
        }
    }

    [HttpGet("cliente/{cpf}")]
    [ProducesResponseType(typeof(IEnumerable<Venda>), StatusCodes.Status200OK)]
    public IActionResult GetVendasByCpf(string cpf)
    {
        try
        {
            var vendas = _vendaAppService.GetByCpf(cpf);
            return Ok(vendas);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Erro ao processar a solicitação", details = ex.Message });
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(Venda), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] Venda venda)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            venda.Id = Guid.NewGuid();

            _vendaAppService.Add(venda);

            return CreatedAtAction(nameof(GetById), new { id = venda.Id }, venda);
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
    public IActionResult Update(Guid id, [FromBody] Venda venda)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingVenda = _vendaAppService.GetById(id);

            if (existingVenda == null)
                return NotFound(new { message = "Venda não encontrada" });

            venda.Id = existingVenda.Id;

            _vendaAppService.Update(venda);

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Erro ao processar a solicitação", details = ex.Message });
        }
    }

}
