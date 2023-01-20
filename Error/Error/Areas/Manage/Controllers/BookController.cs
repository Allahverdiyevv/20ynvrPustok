using Error.Helpers;
using Error.Models;
using Error.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Error.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin")]
    public class BookController : Controller
    {
     
        private readonly PustokContext _pustokContext;
        private readonly IWebHostEnvironment _env;

        public BookController(PustokContext pustokContext , IWebHostEnvironment env)
        {
            _pustokContext = pustokContext;
            _env = env;
        }



        public IActionResult Index()
        {
           
            return View(_pustokContext.Books.ToList());
        }
        public IActionResult Create()
        {
            ViewBag.Authors = _pustokContext.Authors.ToList();
            ViewBag.Genres = _pustokContext.Genres.ToList();
            return View();
        }

        //[HttpPost]
        //public IActionResult Create(BookCreateViewModel bookVM) 
        //{
        //    ViewBag.Authors = _pustokContext.Authors.ToList();
        //    ViewBag.Genres = _pustokContext.Genres.ToList();
        //    if (!ModelState.IsValid) return View();

        //    Book book = new Book
        //    {
        //        Name = bookVM.Name,
        //        CostPrice = bookVM.CostPrice,
        //        SalePrice = bookVM.SalePrice,
        //        AuthorId = bookVM.AuthorId,
        //        GenreId = bookVM.GenreId,
        //        DiscountPrice = bookVM.DiscountPrice,
        //        Code = bookVM.Code,
        //        Desc = bookVM.Desc,
        //        IsAvailable = bookVM.IsAvailable,
        //        IsNew=bookVM.IsNew,
        //        IsFeature=bookVM.IsFeatured

        //    };

        [HttpPost]
        public IActionResult Create(Book book)
        {
            ViewBag.Authors = _pustokContext.Authors.ToList();
            ViewBag.Genres = _pustokContext.Genres.ToList();
            if (!ModelState.IsValid) return View();

            if (book.PosterImageFile != null)
            {
                if (book.PosterImageFile.ContentType != "image/png" && book.PosterImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("PosterImageFile", "Ancaq png ve jpg ola biler");
                    return View();
                }
                /*Guid.NewGuid().ToString()+*/
                if (book.PosterImageFile.Length > 3145728)
                {
                    ModelState.AddModelError("PosterImageFile", "Sekilin olcusu max 3 mb olmalidir");
                    return View();
                }
                BookImage bookImage = new BookImage
                {
                    Book = book,
                    Image = FileManager.SaveFile(_env.WebRootPath, "uploads\\books", book.PosterImageFile),
                    IsPoster = true
                };
                _pustokContext.bookImages.Add(bookImage);

            }

            if (book.HoverImageFile != null)
            {
                if (book.HoverImageFile.ContentType != "image/png" && book.HoverImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("HoverImageFile", "Ancaq png ve jpg ola biler");
                    return View();
                }
                /*Guid.NewGuid().ToString()+*/
                if (book.HoverImageFile.Length > 3145728)
                {
                    ModelState.AddModelError("HoverImageFile", "Sekilin olcusu max 3 mb olmalidir");
                    return View();
                }
                BookImage bookImage = new BookImage
                {
                    Book = book,
                    Image = FileManager.SaveFile(_env.WebRootPath, "uploads\\books", book.HoverImageFile),
                    IsPoster = false
                };
                _pustokContext.bookImages.Add(bookImage);

            }

            if (book.ImageFile !=null)
            {
                foreach (IFormFile imageFile  in book.ImageFile)
                {
                    if (imageFile.ContentType != "image/png" && imageFile.ContentType != "image/jpeg")
                    {
                        ModelState.AddModelError("ImageFiles", "Ancaq png ve jpg ola biler");
                        return View();
                    }
                    /*Guid.NewGuid().ToString()+*/
                    if (imageFile.Length > 3145728)
                    {
                        ModelState.AddModelError("ImageFiles", "Sekilin olcusu max 3 mb olmalidir");
                        return View();
                    }

                    BookImage bookImage = new BookImage
                    {
                        Book=book,
                        Image = FileManager.SaveFile(_env.WebRootPath, "uploads\\books", imageFile),
                        IsPoster = null
                        
                    };

                    _pustokContext.bookImages.Add(bookImage);
                }
            }
           
          
            _pustokContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        
        }
        public IActionResult Update(int id)
        {
            ViewBag.Authors = _pustokContext.Authors.ToList();
            ViewBag.Genres = _pustokContext.Genres.ToList();

            Book book = _pustokContext.Books.Include(x=>x.bookImages).FirstOrDefault(x=>x.Id==id);
            if (book == null) return View("Eorror");

            return View(book);


        }
        [HttpPost] 
        public IActionResult Update(Book book)
        {
            //return Ok(book.BookImageIds);
            ViewBag.Authors = _pustokContext.Authors.ToList();
            ViewBag.Genres = _pustokContext.Genres.ToList();

            Book existBook= _pustokContext.Books.Include(x => x.bookImages).FirstOrDefault(x => x.Id == book.Id);

            existBook.bookImages.RemoveAll(bi => !book.BookImageIds.Contains(bi.Id) && bi.IsPoster==null);


            if (book == null) return View("Eorror");

            if(!ModelState.IsValid) return View();

            if (book.ImageFile != null)
            {
                foreach (IFormFile imageFile in book.ImageFile)
                {
                    if (imageFile.ContentType != "image/png" && imageFile.ContentType != "image/jpeg")
                    {
                        ModelState.AddModelError("ImageFiles", "Ancaq png ve jpg ola biler");
                        return View();
                    }
                    /*Guid.NewGuid().ToString()+*/
                    if (imageFile.Length > 3145728)
                    {
                        ModelState.AddModelError("ImageFiles", "Sekilin olcusu max 3 mb olmalidir");
                        return View();
                    }

                    BookImage bookImage = new BookImage
                    {
                        BookId = book.Id,
                        Image = FileManager.SaveFile(_env.WebRootPath, "uploads\\books", imageFile),
                        IsPoster = null

                    };

                    existBook.bookImages.Add(bookImage);
                    //_pustokContext.bookImages.Add(bookImage);
                }
            }
            existBook.Name = book.Name;
            existBook.CostPrice = book.CostPrice;
            existBook.SalePrice = book.SalePrice;
            existBook.AuthorId = book.AuthorId;
            existBook.GenreId = book.GenreId;
            existBook.DiscountPrice = book.DiscountPrice;
            existBook.Code = book.Code;
            existBook.Desc = book.Desc;
            existBook.IsAvailable = book.IsAvailable;

            _pustokContext.SaveChanges();

            return RedirectToAction("Index");

        }

        public IActionResult Delete(int id)
        {
            Book book = _pustokContext.Books.FirstOrDefault(s => s.Id == id);
            if (book == null) return NotFound();

       

            _pustokContext.Books.Remove(book);
            _pustokContext.SaveChanges();
            return Ok();
        }
    }
}
