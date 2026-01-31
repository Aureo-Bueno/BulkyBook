using BulkyBook.Domain.Entities;

namespace BulkyBook.Application.Interfaces.Repositories;

public interface IProductRepository
{
    IEnumerable<Product> GetAll();
    Product? GetById(int id);
    void Add(Product product);
    void Update(Product product);
    void Remove(Product product);
    void Save();
}
