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
        var vendas = _vendaAppService.GetAll();
        return Ok(vendas);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Venda), StatusCodes.Status200OK)]
    public IActionResult GetById(int id)
    {
        var venda = _vendaAppService.GetById(id);
        
        if (venda == null)
            return NotFound(new { message = "Venda não encontrada" });
        return Ok(venda);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Venda), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] Venda venda)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        venda.Id = Guid.NewGuid();

        _vendaAppService.Add(venda);

        return CreatedAtAction(nameof(GetById), new { id = venda.Id }, venda);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update(int id, [FromBody] Venda venda)
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

}
