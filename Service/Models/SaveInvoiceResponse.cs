using InvoicingApp.Data.Invoices;

namespace InvoiceApp.Service.Models;

public class SaveInvoiceResponse : BaseResponse
{
    public Invoice Invoice { get; set; }
}