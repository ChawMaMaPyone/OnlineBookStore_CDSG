using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineBookStore.Models.Domain;
using OnlineBookStore.Repositories.Abstract;
using OnlineBookStore.Repositories.Implementation;

namespace OnlineBookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService bookService ;
        private readonly lAuthorService authorService;
        private readonly IGenreService genreService;
        private readonly IPublisherService publishService;
        public BookController(IBookService bookService,IGenreService genreService, IPublisherService publisherService,lAuthorService authorService)
        {
            this.bookService = bookService;
            this.authorService = authorService;
            this.genreService = genreService;
            this.publishService = publisherService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            var model = new Book();
            model.AuthorList = authorService.GetAll().Select(a=> new SelectListItem{ Text = a.AuthorName,Value=a.Id.ToString()}).ToList();
            model.PublisherList = publishService.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString() }).ToList();
            model.GenreList = genreService.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }).ToList();
            return View(model);
        }
        [HttpPost]
        public IActionResult Add(Book model)
        {
            model.AuthorList = authorService.GetAll().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString(),Selected=a.Id==model.AuthorId }).ToList();
            model.PublisherList = publishService.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString(),Selected=a.Id==model.PublisherId }).ToList();
            model.GenreList = genreService.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString(),Selected=a.Id == model.GenreId }).ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = bookService.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(Add));
            }
            TempData["msg"] = "Error has occured on server side";
            return View(model);
        }
        public IActionResult Update(int id)
        {
            var record = bookService.FindById(id);
            record.AuthorList = authorService.GetAll().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString(), Selected = a.Id == record.AuthorId }).ToList();
            record.PublisherList = publishService.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString(), Selected = a.Id == record.PublisherId }).ToList();
            record.GenreList = genreService.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString(), Selected = a.Id == record.GenreId }).ToList();

            if (record == null)
            {
                return NotFound();  // Return 404 if the genre doesn't exist.
            }
            return View(record); // Pass the genre to the view
        }

        [HttpPost]
        public IActionResult Update(Book model)
        {
            model.AuthorList = authorService.GetAll().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString(), Selected = a.Id == model.AuthorId }).ToList();
            model.PublisherList = publishService.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString(), Selected = a.Id == model.PublisherId }).ToList();
            model.GenreList = genreService.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString(), Selected = a.Id == model.GenreId }).ToList();

            if (!ModelState.IsValid)
            {
                return View(model); // Return the view if validation fails
            }

            var result = bookService.Update(model);  // Call Update instead of Add
            if (result)
            {
                TempData["msg"] = "Updated Successfully";
                return RedirectToAction("GetAll");
            }
            TempData["msg"] = "Error has occurred on server side";
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var result = bookService.Delete(id);
            if (result)
            {
                TempData["msg"] = "Deleted Successfully";
            }
            else
            {
                TempData["msg"] = "Error occurred while deleting";
            }
            return RedirectToAction("GetAll");
        }
       

        public IActionResult GetAll()
        {
            var data = bookService.GetAll();
            return View(data);


        }
    }
}
