using Microsoft.AspNetCore.Mvc;
using Mission7.Models;
using Mission7.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission7.Controllers
{
    public class HomeController : Controller
    {
        private IBookstoreRepository repo;

        public HomeController (IBookstoreRepository temp)
        {
            repo = temp;
        }

        //This will get all the info for the books and put them into the index page
        public IActionResult Index(string category, int pageNum = 1)
        {
            int pageSize = 10;

            var x = new BooksViewModel
            {
                Books = repo.Books
                .Where(p => p.Category == category | category == null)
                .OrderBy(b => b.Title)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumBooks = 
                        (category == null 
                            ? repo.Books.Count()
                            : repo.Books.Where(x => x.Category == category).Count()),
                    BooksPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };

            //var blah = repo.Books
            //    .OrderBy(b => b.Title)
            //    .Skip((pageNum - 1) * pageSize)
            //    .Take(pageSize);

            return View(x);
        }
    }
}
