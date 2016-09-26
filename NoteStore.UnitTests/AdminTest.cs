using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NoteStore.Domain.Abstract;
using NoteStore.Domain.Entities;
using NoteStore.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace NoteStore.UnitTests
{
    [TestClass]
    public class AdminTest
    {
        [TestMethod]
      
        public void Index_Contains_All_Games()
        {
            // Организация - создание имитированного хранилища данных
            Mock<INoteRepository> mock = new Mock<INoteRepository>();
            mock.Setup(m => m.Notes).Returns(new List<Note>
            {
                new Note { NoteId = 1, Name = "Note1"},
                new Note { NoteId = 2, Name = "Note2"},
                new Note { NoteId = 3, Name = "Note3"},
                new Note { NoteId = 4, Name = "Note4"},
                new Note { NoteId = 5, Name = "Note5"}
            });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            List<Note> result = ((IEnumerable<Note>)controller.Index().
                ViewData.Model).ToList();

            // Утверждение
            Assert.AreEqual(result.Count(), 5);
            Assert.AreEqual("Note1", result[0].Name);
            Assert.AreEqual("Note2", result[1].Name);
            Assert.AreEqual("Note3", result[2].Name);
        }
    }
}
