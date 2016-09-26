using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NoteStore.Domain.Abstract;
using NoteStore.Domain.Entities;
using NoteStore.WebUI.Controllers;
using NoteStore.WebUI.Models;
using NoteStore.WebUI.HtmlHelpers;


namespace NoteStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {


            Mock<INoteRepository> mock = new Mock<INoteRepository>();
            mock.Setup(m => m.Notes).Returns(new List<Note>
            {
                new Note { NoteId = 1, Name = "Note1"},
                new Note { NoteId = 2, Name = "Note2"},
                new Note { NoteId = 3, Name = "Note3"},
                new Note { NoteId = 4, Name = "Note4"},
                new Note { NoteId = 5, Name = "Note5"}
            });
            NoteController controller = new NoteController(mock.Object);
            controller.pageSize = 3;

            // Действие (act)
            NotesListViewModel result =(NotesListViewModel) controller.List(2).Model ;

            // Утверждение (assert)
            List<Note> notes = result.Notes.ToList();
           Assert.IsTrue(notes.Count == 2);
            Assert.AreEqual(notes[0].Name, "Note4");
           Assert.AreEqual(notes[1].Name, "Note5");
        }
        [TestMethod]
        public void Can_Generate_Page_Links()
        {

            // Организация - определение вспомогательного метода HTML - это необходимо
            // для применения расширяющего метода
            HtmlHelper myHelper = null;

            // Организация - создание объекта PagingInfo
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            // Организация - настройка делегата с помощью лямбда-выражения
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Действие
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Утверждение
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }
        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            // Организация (arrange)
            Mock<INoteRepository> mock = new Mock<INoteRepository>();
            mock.Setup(m => m.Notes).Returns
                (new List<Note>
            {
                new Note { NoteId = 1, Name = "Note1"},
                new Note { NoteId  = 2, Name = "Note2"},
                new Note { NoteId = 3, Name = "Note3"},
                new Note { NoteId  = 4, Name = "Note4"},
                new Note { NoteId  = 5, Name = "Note5"}
            });
            NoteController controller = new NoteController(mock.Object);
            controller.pageSize = 3;

            // Act
            NotesListViewModel result
                = (NotesListViewModel)controller.List(2).Model;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }
    }
}
