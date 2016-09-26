using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteStore.Domain.Entities;

namespace NoteStore.Domain.Abstract
{
    public interface INoteRepository
    {
        IEnumerable<Note> Notes { get; }
        void SaveNote(Note note);
        Note DeleteNote(int noteId);
        
    }
}
