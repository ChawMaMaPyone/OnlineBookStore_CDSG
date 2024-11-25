using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.Models.Domain;
using OnlineBookStore.Repositories.Abstract;

namespace OnlineBookStore.Controllers
{
    public class AuthorController : Controller
    {
        private readonly lAuthorService service;
        public AuthorController(lAuthorService service)
        {
            this.service = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Author model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = service.Add(model);
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
            var record = service.FindById(id);
            if (record == null)
            {
                return NotFound();  // Return 404 if the genre doesn't exist.
            }
            return View(record); // Pass the genre to the view
        }

        [HttpPost]
        public IActionResult Update(Author model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Return the view if validation fails
            }

            var result = service.Update(model);  // Call Update instead of Add
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
            var result = service.Delete(id);
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
            var data = service.GetAll();
            return View(data);


        }
    }
}
