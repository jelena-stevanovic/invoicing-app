namespace InvoicingApp.Api.Invoices;

public class InvoiceResponse
{
    public int Id { get; set; }

    public string Partner { get; set; }

    public DateTime Date { get; set; }
    
    public IEnumerable<InvoiceItemDto> Items { get; set; }
    
    public double NoTaxAmount { get; set; }
    
    public double RebatePercentage { get; set; }
    
    public double RebateAmount { get; set; }
    
    public double NoTaxRebateAmount { get; set; }
    
    public double Tax { get; set; }
    
    public double TotalAmount { get; set; }
}