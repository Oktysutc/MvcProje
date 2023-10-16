using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcProje.Models
{
    public class KitapTuru
    {
        [Key]// primary key
        public int Id { get; set; }
        [Required(ErrorMessage ="Kitap tür adı boş bırakılamaz")]// not null
        [MaxLength(25)]// max karakter izni
        [DisplayName("kitap türü adı")]// ekranda gözükecek butonun üstündeki label

        public string Ad { get; set; }    
    }
}
