using Error.Models;
using Error.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Error.Controllers
{
    public class HomeController : Controller
    {
        private readonly PustokContext _pustokContext;
     

        public HomeController(PustokContext  pustokContext)
        {
            _pustokContext = pustokContext;
        }

        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel
            {
                Sliders = _pustokContext.Sliders.OrderBy(x=>x.Order).ToList(),
                FeaturedBooks = _pustokContext.Books.Include(x => x.bookImages).Include(x => x.Author).Where(x => x.IsFeature).ToList(),
                NewBooks = _pustokContext.Books.Include(x => x.bookImages).Include(x => x.Author).Where(x => x.IsNew).ToList(),
                DiscountedBooks = _pustokContext.Books.Include(x => x.bookImages).Include(x => x.Author).Where(x => x.DiscountPrice>0).ToList()

            };
            return View(homeViewModel);
        }
        //public IActionResult SetSession(int id)
        //{
        //    HttpContext.Session.SetString("UserId" , id.ToString());

        //    return Content("Addet session");
        //}
        //public IActionResult GetSession()
        //{
        //   string id = HttpContext.Session.GetString("UserId");

        //    return Content(id);
            
        //}
        //public IActionResult RemoveSession()
        //{
        //    HttpContext.Session.Remove("UserId");

        //    return RedirectToAction("Index");
       // }
        //public IActionResult SetCookie(string name)
        //{
        //    HttpContext.Response.Cookies.Append("BookName", name);

        //    return Content("Added Cookie");
                
            
        //}
        //public IActionResult GetCookie()
        //{
        //     string bookName= HttpContext.Request.Cookies["BookName"];

        //    return Content(bookName);
        //}



        //public IActionResult SetCookie(int bookId)
        //{
        //    List<int> bookIds = new List<int>();

        //    string bookIdStr = HttpContext.Request.Cookies["BookIds"];
        //    if (bookIdStr!= null)
        //    {
        //        bookIds = JsonConvert.DeserializeObject<List<int>>(bookIdStr);
        //        bookIds.Add(bookId);
        //    }
        //    else
        //    {
        //        bookIds.Add(bookId);
        //    }
          
        //    bookIdStr = JsonConvert.SerializeObject(bookIds);   

        //    HttpContext.Response.Cookies.Append("BookId" , bookIds.ToString());

        //    return Content("Append cookie");
        //}


        //public IActionResult GetCookie()
        //{
        //    List<int> bookIds = new List<int>();

        //     string bookIdsStr=HttpContext.Request.Cookies["BookIds"];

        //    if (bookIdsStr !=null )
        //    {
        //       bookIds= JsonConvert.DeserializeObject<List<int>>(bookIdsStr);

        //    }


        //    return Json(bookIds);
        //}
    } 
}