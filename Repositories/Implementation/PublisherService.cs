using Microsoft.AspNetCore.Authorization;
using OnlineBookStore.Models.Domain;
using OnlineBookStore.Repositories.Abstract;

namespace OnlineBookStore.Repositories.Implementation
{
    public class PublisherService : IPublisherService
    {
        private readonly DatabaseContent content;
        public PublisherService(DatabaseContent content)
        {
            this.content = content;
        }
        public bool Add(Publisher model)
        {
            try
            {
                content.Publisher.Add(model);
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

        public Publisher FindById(int id)
        {
            return content.Publisher.Find(id);
        }

        public IEnumerable<Publisher> GetAll()
        {
            return content.Publisher.ToList();
        }

        public bool Update(Publisher model)
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
