﻿using System.Security.Cryptography.X509Certificates;
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
    public DbSet<DetalleOrden> DbSetDetalleOrden { get; set; }
    public DbSet<Orden> DbSetOrden { get; set; }
    public DbSet<Pago> DbSetPago { get; set; }
    public DbSet<PreOrden> DbSetPreOrden { get; set; }

    public DbSet<Rating> DbSetRating { get; set; }
}
