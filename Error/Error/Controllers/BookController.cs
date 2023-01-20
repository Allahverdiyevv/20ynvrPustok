using Error.Models;
using Error.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace Error.Controllers
{
    public class BookController : Controller
    {
		private readonly UserManager<AppUser> _userManager;
		private readonly PustokContext _pustokContext;

        public BookController(PustokContext pustokContext ,UserManager<AppUser> userManager) 
        {
            _userManager = userManager;
			_pustokContext = pustokContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail(int id)
        {
            Book book = _pustokContext.Books
                .Include(b=>b.Author)
                .Include(b=>b.Genre)
                .Include(b=>b.bookImages)
                .FirstOrDefault(x => x.Id == id);

            if (book == null) return View("Error");

            BookDetailViewModel bookDetailVM = new BookDetailViewModel
            {
                Book = book,
                RelatedBooks = _pustokContext.Books
                .Include(x=>x.bookImages)
                .Include(x=>x.Author)
                .Where(x=>x.GenreId==book.GenreId && x.Id !=book.Id ).ToList(),
            };

           
            return View(bookDetailVM);
        }

        public async Task<IActionResult> AddToBasket(int bookId)
        {
            if (!_pustokContext.Books.Any(x => x.Id == bookId)) return NotFound();
            AppUser member = null;

            List<BasketItemViewModel> basketItems = new List<BasketItemViewModel>();

            BasketItemViewModel basketItem = null;
            string basketItemStr = HttpContext.Request.Cookies["Basket"];
          
            if (member==null)
            {
				if (basketItemStr != null)
				{
					basketItems = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketItemStr);

					basketItem = basketItems.FirstOrDefault(x => x.BookId == bookId);
					if (basketItem != null) basketItem.Count++;
					else
					{

						basketItem = new BasketItemViewModel
						{
							BookId = bookId,
							Count = 1
						};

						basketItems.Add(basketItem);

					}



				}
				else
				{
					basketItem = new BasketItemViewModel
					{
						BookId = bookId,
						Count = 1
					};

					basketItems.Add(basketItem);
				}

				basketItemStr = JsonConvert.SerializeObject(basketItems);

				HttpContext.Response.Cookies.Append("Basket", basketItemStr);
            }
            else
            {
                BasketItem basketItem1 = _pustokContext.basketItems.FirstOrDefault(x => x.AppUserId == member.Id && x.BookId == bookId);
                if (basketItem1 != null) basketItem1.Count++;
                else
                {
                    basketItem1 = new BasketItem
                    {
                        BookId = bookId,
                        AppUserId = member.Id,
                        Count = 1
                    };
                    _pustokContext.basketItems.Add(basketItem1);

                }

            }
            _pustokContext.SaveChanges();

            

            
            return Ok();

            
        }

        public IActionResult GetBasket()
        {
            List<BasketItemViewModel> basketItems = new List<BasketItemViewModel>();

            string basketItemStr = HttpContext.Request.Cookies["Basket"];


            if (basketItems !=null)
            {
                basketItems = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketItemStr);



            }
            return Json(basketItems);
        }

        public async Task<IActionResult> Checkout()
        {
            List<BasketItemViewModel> basketItems = new List<BasketItemViewModel>();

            List<CheckoutItemViewModel> checkoutItems = new List<CheckoutItemViewModel>();
            List<BasketItem> memberBasketItems = new List<BasketItem>();
            CheckoutItemViewModel checkoutItem = null;
            AppUser member = null;
			if (User.Identity.IsAuthenticated)
			{
				member = await _userManager.FindByNameAsync(User.Identity.Name);
			}
			string basketStr = HttpContext.Request.Cookies["Basket"];

            if (member ==null)
            {
				if (basketStr != null)
				{
					basketItems = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketStr);

					foreach (var item in basketItems)
					{
						checkoutItem = new CheckoutItemViewModel
						{
							Book = _pustokContext.Books.FirstOrDefault(x => x.Id == item.BookId),
							Count = item.Count
						};
						checkoutItems.Add(checkoutItem);

					}

				}
			}

            else
            {
                memberBasketItems =_pustokContext.basketItems.Include(x=>x.Book).Where(x=>x.AppUserId == member.Id).ToList();

                foreach (var item in memberBasketItems)
                {
                    checkoutItem = new CheckoutItemViewModel
                    {
                        Book = item.Book,
                        Count = item.Count
                        
                    };
                    checkoutItems.Add(checkoutItem);
				}
            }


            return View(checkoutItems);
        }

    }
}
