using InvoicingApp.Data.Invoices;

namespace InvoiceApp.Service.Models;

public class GetInvoicesResponse : BaseResponse
{
    public List<Invoice> Invoices { get; set; }
}