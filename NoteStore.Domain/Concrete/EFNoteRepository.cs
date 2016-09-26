using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteStore.Domain.Abstract;

using NoteStore.Domain.Entities;

namespace NoteStore.Domain.Concrete
{
  public   class EFNoteRepository:INoteRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Note> Notes
        {
            get { return context.Notes; }
        }
        public void SaveNote(Note note)
        {
            if (note.NoteId == 0)
                context.Notes.Add(note);
            else
            {
                Note dbEntry = context.Notes.Find(note.NoteId);
                if (dbEntry != null)
                {
                    dbEntry.Name = note.Name;
                    dbEntry.Description = note.Description;
                    dbEntry.Diagonal = note.Diagonal;
                    dbEntry.HDD = note.HDD;
                    dbEntry.OperationSystem = note.OperationSystem;
                    dbEntry.Processor = note.Processor;
                    dbEntry.Price = note.Price;
                    dbEntry.Producer = note.Producer;
                    dbEntry.Touchscreen = note.Touchscreen;
                    dbEntry.VideoMemory = note.VideoMemory;
                    dbEntry.ImageData = note.ImageData;
                    dbEntry.ImageMimeType = note.ImageMimeType;
                }
            }
            context.SaveChanges();
        }
        public Note DeleteNote(int noteId)
        {
            Note dbEntry = context.Notes.Find(noteId);
            if (dbEntry != null)
            {
                context.Notes.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
