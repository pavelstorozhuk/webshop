using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteStore.Domain.Entities;
using System.Data.Entity;
namespace NoteStore.Domain.Concrete
{
   public class EFDbContext:DbContext
    {
       public EFDbContext()
       {
          
       }
        public DbSet<Note> Notes { get; set; }
    }
}
