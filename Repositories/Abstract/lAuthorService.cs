using OnlineBookStore.Models.Domain;

namespace OnlineBookStore.Repositories.Abstract
{
    public interface lAuthorService
    {
        bool Add(Author model);
        bool Update(Author model);
        bool Delete(int id);
        Author FindById(int id);
        IEnumerable<Author> GetAll();
    }
}
