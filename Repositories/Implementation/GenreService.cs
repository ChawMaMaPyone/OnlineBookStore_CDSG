using OnlineBookStore.Models.Domain;
using OnlineBookStore.Repositories.Abstract;

namespace OnlineBookStore.Repositories.Implementation
{
    public class GenreService : IGenreService

    {
        private readonly DatabaseContent content;
        public GenreService(DatabaseContent content)
        {
            this.content = content;
        }
        public bool Add(Genre model)
        {
            try
            {
                content.Genere.Add(model);
                content.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }


        public bool Delete(int id)
        {
            try
            {
                var data=this.FindById(id);
                if(data == null)
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

        public Genre FindById(int id)
        {
            return content.Genere.Find(id);
        }
        public bool Update(Genre model)
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

        IEnumerable<Genre> IGenreService.GetAll()
        {
            return content.Genere.ToList();
        }
    }
}
