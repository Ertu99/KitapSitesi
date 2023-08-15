using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WebWebWeb.Models;

public class Kitap
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string KitapAdi { get; set; }
    public string Tanim { get; set; }
    [Required]
    public string Yazar { get; set; }
    [Required]
    [Range(10, 1000)]
    public double Fiyat { get; set; }
    
    [ValidateNever]
    public int KitapTuruId { get; set; }
    [ForeignKey("KitapTuruId")]
    
    [ValidateNever]
    public KitapTuru KitapTuru { get; set; }
    
    [ValidateNever]
    public string ResimUrl { get; set; }
    
}