using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NoteStore.Domain.Entities
{
    public class ShoppingDetails
    {
        [Required(ErrorMessage = "Укажите как вас зовут")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите адрес доставки")]
        public string Line { get; set; }
    

        [Required(ErrorMessage = "Укажите город")]
        public string City { get; set; }

        [Required(ErrorMessage = "Укажите страну")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}
