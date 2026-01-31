using BulkyBook.Domain.Entities;

namespace BulkyBook.Application.Interfaces.Repositories;

public interface ICategoryRepository
{
    IEnumerable<Category> GetAll();
    Category? GetById(int id);
    void Add(Category category);
    void Update(Category category);
    void Remove(Category category);
    void Save();
}
