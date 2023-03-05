using InvoiceApp.Service.Models;
using InvoicingApp.Data;
using InvoicingApp.Data.Invoices;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.Service;

public class InvoiceService : IInvoiceService
{
    private readonly ApplicationDbContext dbContext;

    public InvoiceService(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<GetInvoicesResponse> GetInvoices(string userId)
    {
        var invoices = await dbContext.Invoices
            .Where(o => o.UserId == userId)
            .Include(i => i.Items)
            .ToListAsync();

        return new GetInvoicesResponse {Success = true, Invoices = invoices};
    }

    public async Task<SaveInvoiceResponse> SaveInvoice(Invoice invoice)
    {
        Invoice entry;
        if (invoice.Id == 0)
        {
            entry = (await dbContext.Invoices.AddAsync(invoice)).Entity;
            await dbContext.InvoiceItems.AddRangeAsync(invoice.Items.Select(invoiceItem =>
            {
                invoiceItem.InvoiceId = entry.Id;
                return invoiceItem;
            }));
        }
        else
        {
            entry = dbContext.Invoices.Include(i => i.Items).FirstOrDefault(i => i.Id == invoice.Id);
            entry.Date = invoice.Date;
            entry.NoTaxAmount = invoice.NoTaxAmount;
            entry.RebatePercentage = invoice.RebatePercentage;
            entry.RebateAmount = invoice.RebateAmount;
            entry.NoTaxRebateAmount = invoice.NoTaxRebateAmount;
            entry.Tax = invoice.Tax;
            entry.TotalAmount =invoice.TotalAmount;
            entry.Partner = invoice.Partner;
            entry.UserId = invoice.UserId;
            entry.Date = invoice.Date;

            var deletedItems = entry.Items.Where(i => invoice.Items.All(item => item.Id != i.Id));
            dbContext.InvoiceItems.RemoveRange(deletedItems);
        }

        invoice.Id = entry.Id;
        await SaveInvoiceItems(invoice);

        var saveResponse = await dbContext.SaveChangesAsync();

        if (saveResponse >= 0)
        {
            return new SaveInvoiceResponse
            {
                Success = true,
                Invoice = invoice
            };
        }

        return new SaveInvoiceResponse
        {
            Success = false,
            Error = "Unable to save invoice",
            ErrorCode = "T05"
        };
    }

    public async Task<DeleteInvoiceResponse> DeleteInvoice(int invoiceId, string userId)
    {
        var invoice = await dbContext.Invoices.FindAsync(invoiceId);

        if (invoice == null)
        {
            return new DeleteInvoiceResponse
            {
                Success = false,
                Error = "Invoice not found",
                ErrorCode = "I01"
            };
        }

        if (invoice.UserId != userId)
        {
            return new DeleteInvoiceResponse
            {
                Success = false,
                Error = "You don't have access to delete this invoice",
                ErrorCode = "I02"
            };
        }

        dbContext.Invoices.Remove(invoice);

        var saveResponse = await dbContext.SaveChangesAsync();

        if (saveResponse >= 0)
        {
            return new DeleteInvoiceResponse
            {
                Success = true,
                InvoiceId = invoice.Id
            };
        }

        return new DeleteInvoiceResponse
        {
            Success = false,
            Error = "Unable to delete invoice",
            ErrorCode = "I03"
        };
    }

    private async Task SaveInvoiceItems(Invoice invoice)
    {
        foreach (var item in invoice.Items)
        {
            item.InvoiceId = invoice.Id;
            item.Quantity = item.Quantity;
            item.NoTaxAmount = item.NoTaxAmount;
            item.RebatePercentage = item.RebatePercentage;
            item.RebateAmount = item.RebateAmount;
            item.NoTaxRebateAmount = item.NoTaxRebateAmount;
            item.Tax = item.Tax;
            item.TotalAmount = item.TotalAmount;
            item.ArticleName = item.ArticleName;
            
            if (item.Id == 0)
            {
                await dbContext.InvoiceItems.AddAsync(item);
            }
            else
            {
                await dbContext.InvoiceItems.FindAsync(item.Id);
                // todo do some update/
            }
        }
    }
}