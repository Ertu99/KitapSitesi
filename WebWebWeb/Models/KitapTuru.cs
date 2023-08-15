using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebWebWeb.Models;

public class KitapTuru
{
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Kitap Tur Adi bos birakilamaz!")]
    [MaxLength(25)]
    [DisplayName("Kitap Türü Adi")]
    public string Ad { get; set; }
    
}