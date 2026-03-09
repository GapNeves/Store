using Store.Domain.Models;

namespace Store.AppService.Interfaces;
public interface IProdutoAppService
{
    void Add(Produto produto);
    void Update(Produto produto);
    void Delete(Guid id);
    Produto GetById(Guid id);
    IEnumerable<Produto> GetAll();
}
