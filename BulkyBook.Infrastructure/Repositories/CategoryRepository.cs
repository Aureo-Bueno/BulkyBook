using BulkyBook.Application.Interfaces.Repositories;
using BulkyBook.Domain.Entities;
using BulkyBook.Infrastructure.Data;

namespace BulkyBook.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _db;

    public CategoryRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public IEnumerable<Category> GetAll()
    {
        return _db.Categories.ToList();
    }

    public Category? GetById(int id)
    {
        return _db.Categories.Find(id);
    }

    public void Add(Category category)
    {
        _db.Categories.Add(category);
    }

    public void Update(Category category)
    {
        _db.Categories.Update(category);
    }

    public void Remove(Category category)
    {
        _db.Categories.Remove(category);
    }

    public void Save()
    {
        _db.SaveChanges();
    }
}
