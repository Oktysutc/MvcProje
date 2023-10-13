using System.ComponentModel.DataAnnotations;

namespace MvcProje.Models
{
    public class KitapTuru
    {
        [Key]// primary key
        public int Id { get; set; }
        [Required]// not null
        public string Ad { get; set; }    
    }
}
