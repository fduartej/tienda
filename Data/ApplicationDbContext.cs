using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using apptienda.Models;

namespace apptienda.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    public DbSet<Contacto> DbSetContactos { get; set; }
    public DbSet<Customer> DbSetCustomer { get; set; }
    public DbSet<Producto> DbSetProducto { get; set; }
}
