using System.Linq;
using System.Web.Mvc;
using NoteStore.Domain.Entities;
using NoteStore.Domain.Abstract;
using NoteStore.WebUI.Models;


namespace NoteStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private INoteRepository repository;
        private IOrderProcessor orderProcessor;
        public CartController(INoteRepository _reposetory, IOrderProcessor processor)
        {
            repository = _reposetory;
            orderProcessor = processor;


        }
        
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
        // GET: Cart
        public RedirectToRouteResult AddToCart(Cart cart,int noteId, string returnUrl)
        {
            Note note = repository.Notes
                .FirstOrDefault(g => g.NoteId == noteId);

            if (note != null)
            {
                cart.AddItem(note, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart,int noteId, string returnUrl)
        {
            Note note = repository.Notes
                .FirstOrDefault(g => g.NoteId == noteId);

            if (note != null)
            {
                cart.RemoveLine(note);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

       /* public Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }*/
        public ViewResult Index(Cart cart,string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }
        public ViewResult Checkout()
        {
            return View(new ShoppingDetails());
        }
        [HttpPost]
        public ViewResult Checkout(Cart cart, ShoppingDetails shoppingDetails)
        {
           
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Извините, ваша корзина пуста!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shoppingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shoppingDetails);
            }
        }
    }
}