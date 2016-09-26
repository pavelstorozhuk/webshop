using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoteStore.Domain.Abstract;
using NoteStore.Domain.Entities;
using Moq;
using System.Web.Mvc;
using NoteStore.WebUI.Controllers;
namespace NoteStore.UnitTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            // Организация - создание нескольких тестовых игр
            Note note1 = new Note { NoteId = 1, Name = "Note1" };
            Note note2 = new Note { NoteId = 2, Name = "Note2" };

            // Организация - создание корзины
            Cart cart = new Cart();

            // Действие
            cart.AddItem(note1, 1);
            cart.AddItem(note2, 1);
            List<CartLine> results = cart.Lines.ToList();

            // Утверждение
            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Note, note1);
            Assert.AreEqual(results[1].Note, note2);
        }
        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            Note note1 = new Note { NoteId = 1, Name = "Note1" };
            Note note2 = new Note { NoteId = 2, Name = "Note2" };

            // Организация - создание корзины
            Cart cart = new Cart();

            // Действие
            cart.AddItem(note1, 5);
            cart.AddItem(note2, 2);
            cart.AddItem(note1, 1);
            List<CartLine> results = cart.Lines.OrderBy(c => c.Note.NoteId).ToList();

            // Утверждение
            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Quantity, 6);
            Assert.AreEqual(results[1].Quantity, 2);
        }
        [TestMethod]
        public void Can_Remove_Line()
        {
            Note note1 = new Note { NoteId = 1, Name = "Note1" };
            Note note2 = new Note { NoteId = 2, Name = "Note2" };
            Note note3 = new Note { NoteId = 3, Name = "Note3" };
            Cart cart = new Cart();
            cart.AddItem(note1, 1);
            cart.AddItem(note2, 1);
            cart.AddItem(note3, 1);
             cart.RemoveLine(note2);
              
           
            Assert.AreEqual(cart.Lines.Count(), 2);
            Assert.AreEqual(cart.Lines.Where(c=>c.Note==note2).Count(), 0);
        }
        [TestMethod]
        public void  Calculate_Cart_Total_Price()
        {
            Note note1 = new Note { NoteId = 1, Name = "Note1",Price=400 };
            Note note2 = new Note { NoteId = 2, Name = "Note2",Price=500 };
            Note note3 = new Note { NoteId = 3, Name = "Note3",Price=600 };
            Cart cart = new Cart();
            cart.AddItem(note1, 1);
            cart.AddItem(note2, 2);
            cart.AddItem(note3, 3);
            decimal test_sum = note1.Price*1+note2.Price*2+note3.Price*3;
            Assert.AreEqual(cart.ComputeTotalValue(), test_sum);
        }
         [TestMethod]
        public void  Can_Clear_Cart()
        {
            Note note1 = new Note { NoteId = 1, Name = "Note1", Price = 400 };
            Note note2 = new Note { NoteId = 2, Name = "Note2", Price = 500 };
            Note note3 = new Note { NoteId = 3, Name = "Note3", Price = 600 };
            Cart cart = new Cart();
            cart.AddItem(note1, 1);
            cart.AddItem(note2, 2);
            cart.AddItem(note3, 3);
            cart.Clear();
            Assert.AreEqual(cart.Lines.Count(), 0);
        }
         [TestMethod]
         public void Cannot_Checkout_Empty_Cart()
         {
             // Организация - создание имитированного обработчика заказов
             Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

             // Организация - создание пустой корзины
             Cart cart = new Cart();

             // Организация - создание деталей о доставке
             ShoppingDetails shippingDetails = new ShoppingDetails();

             // Организация - создание контроллера
             CartController controller = new CartController(null, mock.Object);

             // Действие
             ViewResult result = controller.Checkout(cart, shippingDetails);

             // Утверждение — проверка, что заказ не был передан обработчику 
             mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShoppingDetails>()),
                 Times.Never());

             // Утверждение — проверка, что метод вернул стандартное представление 
             Assert.AreEqual("", result.ViewName);

             // Утверждение - проверка, что-представлению передана неверная модель
             Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
         }
    }
}

