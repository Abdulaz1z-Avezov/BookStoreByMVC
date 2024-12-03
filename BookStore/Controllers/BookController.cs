using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        public AppDbContext _dbcontext;
        public BookController(AppDbContext  appDbContext) 
        {
            _dbcontext = appDbContext;
        }

        public IActionResult Index()
        {
            var books=_dbcontext.Books.ToList();
            return View( books);
        }

        [HttpGet]
        public IActionResult Create() 
        { 
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book books)
        {
            if(ModelState.IsValid)
            {
                _dbcontext.Books.Add(books);
                _dbcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View( books);
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Edit(Book book)
        {
            if(ModelState.IsValid)
            {
                _dbcontext.Books.Update(book);
                _dbcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

  
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var book=_dbcontext.Books.Find(id);
            if(book==null)
            {
                return NotFound();
            }
            return View(book);
        }


        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfir(int id)
        {
            var book = _dbcontext.Books.Find(id);
            if (book != null)
            {
                _dbcontext.Books.Remove(book);
                _dbcontext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}
