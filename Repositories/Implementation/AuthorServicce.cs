using Microsoft.AspNetCore.Authorization;
using OnlineBookStore.Models.Domain;
using OnlineBookStore.Repositories.Abstract;

namespace OnlineBookStore.Repositories.Implementation
{
    public class AuthorServicce : lAuthorService
    {
        private readonly DatabaseContent content;
        public AuthorServicce(DatabaseContent content)
        {
            this.content = content;
        }
        public bool Add(Author model)
        {
            try
            {
                content.Author.Add(model);
                content.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.FindById(id);
                if (data == null)
                    return false;
                content.Remove(data);
                content.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Author FindById(int id)
        {
            return content.Author.Find(id);
        }

        public IEnumerable<Author> GetAll()
        {
            return content.Author.ToList();
        }

        public bool Update(Author model)
        {
            try
            {
                content.Update(model);
                content.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
