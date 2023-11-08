﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcProje.Models
{
    public class Kiralama
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string OgrenciId { get; set; }
        [ValidateNever]
        public int KitapId { get; set; }
        [ForeignKey("KitapId")]

        [ValidateNever]
        public  Kitap Kitap{ get; set; }
         
    }
}
