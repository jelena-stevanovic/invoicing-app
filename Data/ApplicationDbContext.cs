using System.Transactions;
using InvoicingApp.Data.Identity;
using InvoicingApp.Data.Invoices;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InvoicingApp.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Invoice>()
            
            .HasMany(g => g.Items)
            .WithOne(item => item.Invoice);
    }

    public DbSet<Invoice> Invoices { get; set; }
    
    public DbSet<InvoiceItem> InvoiceItems { get; set; }
    
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
}