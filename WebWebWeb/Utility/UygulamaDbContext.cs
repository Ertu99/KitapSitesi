using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebWebWeb.Models;

namespace WebWebWeb.Utility;

public class UygulamaDbContext : IdentityDbContext
{
    public DbSet<KitapTuru> KitapTurleri { get; set; }
    public DbSet<Kitap> Kitaplar { get; set; }

    public DbSet<Kiralama> Kiralamalar { get; set; }

    public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options) { }

}