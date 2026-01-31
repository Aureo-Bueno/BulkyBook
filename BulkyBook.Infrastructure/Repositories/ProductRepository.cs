using BulkyBook.Application.Interfaces.Repositories;
using BulkyBook.Domain.Entities;
using BulkyBook.Infrastructure.Data;

namespace BulkyBook.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _db;

    public ProductRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public IEnumerable<Product> GetAll()
    {
        return _db.Products.ToList();
    }

    public Product? GetById(int id)
    {
        return _db.Products.Find(id);
    }

    public void Add(Product product)
    {
        _db.Products.Add(product);
    }

    public void Update(Product product)
    {
        _db.Products.Update(product);
    }

    public void Remove(Product product)
    {
        _db.Products.Remove(product);
    }

    public void Save()
    {
        _db.SaveChanges();
    }
}
