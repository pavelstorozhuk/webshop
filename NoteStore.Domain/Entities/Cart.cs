using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteStore.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Note note, int quantity)
        {
            CartLine line = lineCollection
                .Where(g => g.Note.NoteId == note.NoteId)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Note = note,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Note note)
        {
            lineCollection.RemoveAll(l => l.Note.NoteId == note.NoteId);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Note.Price * e.Quantity);

        }
        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
        
    }
    public class CartLine
    {
        public Note Note { get; set; }
        public int Quantity { get; set; }
    }
}
