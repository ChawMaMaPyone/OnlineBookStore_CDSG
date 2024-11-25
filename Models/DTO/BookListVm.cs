using OnlineBookStore.Models.Domain;

namespace OnlineBookStore.Models.DTO
{
    public class BookListVm
    {

        public List<Book> BookList { get; set; } = new List<Book>(); // Default to an empty list

        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string? Term { get; set; }

    }
}
