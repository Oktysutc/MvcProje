using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using MvcProje.Models;

namespace MvcProje.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public int Ogrencino { get; set; }
        public string? Adres { get; set; }
       // public string? Fakülte { get; set; }
       // public string? bolum { get; set; }
    }
}
