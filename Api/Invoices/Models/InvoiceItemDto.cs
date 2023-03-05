namespace InvoicingApp.Api.Invoices;

public class InvoiceItemDto
{
    public int Id { get; set; }
    
    public int OrdinalNumber { get; set; }

    public string ArticleName { get; set; }

    public int Quantity { get; set; }

    public double Price { get; set; }

    public double NoTaxAmount { get; set; }

    public double RebatePercentage { get; set; }

    public double RebateAmount { get; set; }

    public double NoTaxRebateAmount { get; set; }
    
    public double Tax { get; set; }
    
    public double TotalAmount { get; set; }
}