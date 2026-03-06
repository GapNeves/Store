using Microsoft.AspNetCore.Mvc;
using Store.AppService.Interfaces;
using Store.Domain.Models;

namespace Store.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoAppService _produtoAppService;

    public ProdutoController(IProdutoAppService produtoAppService)
    {
        _produtoAppService = produtoAppService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Produto>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var produtos = _produtoAppService.GetAll();
        return Ok(produtos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(int id)
    {
        var produto = _produtoAppService.GetById(id);
        
        if (produto == null)
            return NotFound(new { message = "Produto não encontrado" });
        return Ok(produto);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Produto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] Produto produto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        produto.Id = Guid.NewGuid();
        _produtoAppService.Add(produto);

        return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update(int id, [FromBody] Produto produto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var produtoExistente = _produtoAppService.GetById(id);

        if (produtoExistente == null)
            return NotFound(new { message = "Produto não encontrado" });
        
        _produtoAppService.Update(produto);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        var produto = _produtoAppService.GetById(id);

        if (produto == null)
            return NotFound(new { message = "Produto não encontrado" });
        
        _produtoAppService.Delete(id);
        
        return NoContent();
    }
}
