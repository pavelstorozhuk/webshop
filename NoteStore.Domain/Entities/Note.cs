using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace NoteStore.Domain.Entities
{
    public class Note
    {
        [Key]
        
        [HiddenInput(DisplayValue = false)]
        public int NoteId { get; set; }
           [Required(ErrorMessage = "Пожалуйста, введите название ноутбука")]
        public string Name { get; set; }
           [Required(ErrorMessage = "Пожалуйста, введите операционую систему")]

        public string OperationSystem { get; set; }
           [Required(ErrorMessage = "Пожалуйста, введите поставщика")]

        public string Producer { get; set; }
           [Required(ErrorMessage = "Пожалуйста, введите диагональ экрана ноутбука")]
        public double Diagonal{ get;set;}
           [Required(ErrorMessage = "Пожалуйста, введите объем жесткого диска")]
        public int HDD { get; set; }
           [Required(ErrorMessage = "Пожалуйста, введите объем оперативной памяти")]
        public int RAM { get; set; }
           [Required(ErrorMessage = "Пожалуйста, введите название процессора Intel/Amd")]
        public string Processor{ get;set;}
           [Required(ErrorMessage = "Пожалуйста, введите объем видео памяти")]
        public int VideoMemory { get; set; }
       
        public bool Touchscreen { get; set; }
           [Required(ErrorMessage = "Пожалуйста, описание ноутбука")]
        public string Description { get; set; }
           [Required(ErrorMessage = "Пожалуйста, введите цену")]
        public decimal Price { get; set; }
   
           public byte[] ImageData { get; set; }
           public string ImageMimeType { get; set; }
    }
}
