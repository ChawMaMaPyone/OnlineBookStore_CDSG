using Microsoft.AspNetCore.Authorization;
using OnlineBookStore.Models.Domain;
using OnlineBookStore.Repositories.Abstract;

namespace OnlineBookStore.Repositories.Implementation
{
    public class BookService : IBookService
    {
        private readonly DatabaseContent content;
        public BookService(DatabaseContent content)
        {
            this.content = content;
        }
        public bool Add(Book model)
        {
            try
            {
                content.Book.Add(model);
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

        public Book FindById(int id)
        {
            return content.Book.Find(id);
        }

        public IEnumerable<Book> GetAll()
        {
            var data = (from book in content.Book
                        join author in content.Author on book.AuthorId equals author.Id
                        join publisher in content.Publisher on book.PublisherId equals publisher.Id
                        join genre in content.Genere on book.GenreId equals genre.Id
                        select new Book // Use a ViewModel here
                        {
                            Id = book.Id,
                            AuthorId = book.AuthorId,
                            GenreId = book.GenreId,
                            Isbn = book.Isbn,
                            PublisherId = book.PublisherId,
                            Title = book.Title,
                            TotalPages = book.TotalPages,
                            GenreName = genre.Name,  // Use the genre name
                            AuthorName = author.AuthorName,  // Use the author name
                            PublisherName = publisher.PublisherName,  // Use the publisher name
                        }).ToList();

            return data;
        }

        public bool Update(Book model)
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
