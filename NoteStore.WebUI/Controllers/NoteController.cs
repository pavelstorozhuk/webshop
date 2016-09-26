using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NoteStore.Domain.Entities;
using NoteStore.Domain.Abstract;
using NoteStore.WebUI.Models;
using NoteStore.Domain.Concrete; //временно
using System.Data.Entity;
namespace NoteStore.WebUI.Controllers
{
    public class NoteController : Controller
    {
        EFDbContext db = new EFDbContext(); //временно
        private INoteRepository repository;
        public int pageSize = 4;
        public NoteController(INoteRepository _repository)
        {
            repository = _repository;
        }
        // GET: Note
        public ViewResult List(int page = 1)
        {
            NotesListViewModel model = new NotesListViewModel
            {
                Notes = repository.Notes
                    .OrderBy(game => game.NoteId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = repository.Notes.Count()
                }
            };
            return View(model);
        }
        public FileContentResult GetImage(int noteId)
        {
            Note note = repository.Notes
                .FirstOrDefault(n => n.NoteId == noteId);

            if (note != null)
            {
                return File(note.ImageData, note.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}