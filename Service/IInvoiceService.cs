using InvoiceApp.Service.Models;
using InvoicingApp.Data.Invoices;

namespace InvoiceApp.Service;

public interface IInvoiceService
{
    Task<GetInvoicesResponse> GetInvoices(string userId);

    Task<SaveInvoiceResponse> SaveInvoice(Invoice invoice);

    Task<DeleteInvoiceResponse> DeleteInvoice(int invoiceId, string userId);
}