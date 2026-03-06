using Store.Domain.Models;

namespace Store.AppService.Interfaces;
public interface IProdutoAppService
{
    void Add(Produto produto);
    void Update(Produto produto);
    void Delete(int id);
    Produto GetById(int id);
    IEnumerable<Produto> GetAll();
}
