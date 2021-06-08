
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using task7.Models;

namespace task7.Controllers
{
    public class HomeController : Controller
    {
        private masterContext db;
        public HomeController(masterContext books)
        {
            db = books;
        }
        public IActionResult Index()
        {
            ViewData["Title"] = "Books";
            var books = db.Books.Include(a => a.Author).ToList();
            return View(books);
        }

        public IActionResult Create()
        {
            Book book = new Book();
            SelectList authors = new SelectList(db.Authors, "Id", "Name");
            ViewBag.Authors = authors;
            return View(book);
        }
        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                if (book.Id == 0)
                {
                    book.Author = db.Authors.FirstOrDefault(b => b.Id == book.AuthorId);

                    db.Books.Add(book);
                }
                else
                {
                    var mus = db.Books.First(b => b.Id == book.Id);
                    mus.Name = book.Name;
                    mus.Label = book.Label;
                    mus.Language = book.Language;
                    mus.Year = book.Year;
                    mus.AuthorId = book.AuthorId;
                    mus.Author = db.Authors.FirstOrDefault(b => b.Id == book.AuthorId);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                SelectList authors = new SelectList(db.Authors, "Id", "Name");
                ViewBag.Authors = authors;
                return View(book);
            }
        }

        public IActionResult Edit(Book book)
        {
            SelectList authors = new SelectList(db.Authors, "Id", "Name");
            ViewBag.Authors = authors;
            return View("Create", book);
        }

        public IActionResult Delete(Book book)
        {
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Show_author(int id)
        {
            var auths = db.Authors.FirstOrDefault(b => b.Id == id);
            var bk = db.Books.Where(x => x.AuthorId == id);
            ViewData["Books"] = bk;
            return View(auths);
        }

        public IActionResult Edit_auth(Author author)
        {
            return View("Edit_author", author);
        }

        [HttpPost]
        public IActionResult Edit_author(Author author)
        {
            var auth = db.Authors.FirstOrDefault(b => b.Id == author.Id);
            auth.Name = author.Name;
            auth.Year = author.Year;
            db.SaveChanges();
            var bk = db.Books.Where(x => x.AuthorId == author.Id);
            ViewData["Books"] = bk;
            return View("Show_author", author);
        }

        public IActionResult Metrica()
        {
            var auths = db.Authors.ToList();
            var books = db.Books.ToList();
            ViewData["CountAuth"] = auths.Count();
            ViewData["CountBooks"] = books.Count();
            ViewData["Count"] = auths.Count() + books.Count();

            var GrLanguage = books.GroupBy(x => x.Language).ToList();
            ViewData["GrLanguage"] = GrLanguage;

            var GrAuths = books.GroupBy(x => x.AuthorId).ToList();
            ViewData["GrAuths"] = GrAuths;

            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
