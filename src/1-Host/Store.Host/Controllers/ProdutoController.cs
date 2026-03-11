using Microsoft.AspNetCore.Mvc;
using Store.AppService.Interfaces;
using Store.Domain;
using Store.Domain.Models;

namespace Store.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoAppService _produtoAppService;
    private readonly ProdutoService _produtoService;

    public ProdutoController(IProdutoAppService produtoAppService)
    {
        _produtoAppService = produtoAppService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Produto>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        try
        {
            var produtos = _produtoAppService.GetAll();
            return Ok(produtos);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Erro ao processar a solicitação", details = ex.Message });
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        try
        {
            var produto = _produtoAppService.GetById(id);

            if (produto == null)
                return NotFound(new { message = "Produto não encontrado" });

            return Ok(produto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Erro ao processar a solicitação", details = ex.Message });
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(Produto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] Produto produto)
    {
        try
        {
            produto.Id = Guid.NewGuid();
            _produtoAppService.Add(produto);

            return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
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
    public IActionResult Update(Guid id, [FromBody] Produto produto)
    {
        try
        {
            var produtoExistente = _produtoAppService.GetById(id);

            if (produtoExistente == null)
                return NotFound(new { message = "Produto não encontrado" });

            _produtoAppService.Update(produto);

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
            var produto = _produtoAppService.GetById(id);

            if (produto == null)
                return NotFound(new { message = "Produto não encontrado" });

            _produtoAppService.Delete(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Erro ao processar a solicitação", details = ex.Message });
        }
    }
}
