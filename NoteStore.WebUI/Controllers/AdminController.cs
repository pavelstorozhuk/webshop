using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NoteStore.Domain.Abstract;
using NoteStore.Domain.Entities;

namespace NoteStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        INoteRepository repository;

        public AdminController(INoteRepository _repository)
        {
            repository = _repository;
        }

        public ViewResult Index()
        {
            return View(repository.Notes);
        }
        public ViewResult Edit(int NoteId)
        {
            Note note = repository.Notes
                .FirstOrDefault(g => g.NoteId == NoteId);
            return View(note);
        }
       
        public ViewResult Create()
        {
            return View("Edit", new Note());
        }
        [HttpPost]
        public ActionResult Delete(int noteId)
        {
            Note deletedNote = repository.DeleteNote(noteId);
            if (deletedNote != null)
            {
                TempData["message"] = string.Format("Ноутбук \"{0}\" был удален",
                    deletedNote.Name);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(Note note, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    note.ImageMimeType = image.ContentType;
                    note.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(note.ImageData, 0, image.ContentLength);
                }
                repository.SaveNote(note);
                TempData["message"] = string.Format("Изменения в игре \"{0}\" были сохранены", note.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так со значениями данных
                return View(note);
            }
        }

    }
}