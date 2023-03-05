using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InvoicingApp.Data.Identity;

namespace InvoicingApp.Data.Invoices;

public class Invoice
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
        
    public string UserId { get; set; }
        
    public string Partner { get; set; }
        
    public DateTime Date { get; set; }
        
    public double NoTaxAmount  { get; set; }
        
    public double RebatePercentage  { get; set; }
        
    public double RebateAmount { get; set; }
        
    public double NoTaxRebateAmount { get; set; }
        
    public double Tax { get; set; }
        
    public double TotalAmount { get; set; }
        
    public ApplicationUser User { get; set; }

    public ICollection<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
}