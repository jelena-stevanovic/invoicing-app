using InvoiceApp.Service;
using InvoiceApp.Service.Models;
using InvoicingApp.Api.Invoices;
using InvoicingApp.Data.Invoices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvoicingApp.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class InvoiceController : BaseController
{
    private readonly IInvoiceService invoiceService;

    public InvoiceController(IInvoiceService invoiceService)
    {
        this.invoiceService = invoiceService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {   
        var getInvoicesResponse = await invoiceService.GetInvoices(UserId);

        if (!getInvoicesResponse.Success)
        {
            return UnprocessableEntity(getInvoicesResponse);
        }

        var invoicesResponse = getInvoicesResponse.Invoices.ConvertAll(MapInvoice);

        return Ok(invoicesResponse);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var getInvoicesResponse = await invoiceService.GetInvoices(UserId);

        if (!getInvoicesResponse.Success)
        {
            return UnprocessableEntity(getInvoicesResponse);
        }

        var invoicesResponse = getInvoicesResponse.Invoices.First(i => i.Id == id);

        return Ok(MapInvoice(invoicesResponse));
    }

    [HttpPost]
    public async Task<IActionResult> Post(InvoiceRequest invoiceRequest)
    {
        var invoice = MapInvoice(invoiceRequest);

        var saveInvoiceResponse = await invoiceService.SaveInvoice(invoice);

        if (!saveInvoiceResponse.Success)
        {
            return UnprocessableEntity(saveInvoiceResponse);
        }

        var invoiceResponse = MapInvoice(saveInvoiceResponse.Invoice);

        return Ok(invoiceResponse);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id == 0)
        {
            return BadRequest(
                new DeleteInvoiceResponse {Success = false, ErrorCode = "D01", Error = "Invalid Invoice id"});
        }

        var deleteInvoiceResponse = await invoiceService.DeleteInvoice(id, UserId);
        if (!deleteInvoiceResponse.Success)
        {
            return UnprocessableEntity(deleteInvoiceResponse);
        }

        return Ok(deleteInvoiceResponse.InvoiceId);
    }

    [HttpPut]
    public async Task<IActionResult> Put(InvoiceRequest invoiceRequest)
    {
        var invoice = MapInvoice(invoiceRequest);

        var saveInvoiceResponse = await invoiceService.SaveInvoice(invoice);

        if (!saveInvoiceResponse.Success)
        {
            return UnprocessableEntity(saveInvoiceResponse);
        }

        var invoiceResponse = MapInvoice(saveInvoiceResponse.Invoice);

        return Ok(invoiceResponse);
    }

    private InvoiceResponse MapInvoice(Invoice o) => new()
    {
        Id = o.Id,
        Partner = o.Partner,
        Date = o.Date,
        NoTaxAmount = o.NoTaxAmount,
        RebatePercentage = o.RebatePercentage,
        RebateAmount = o.RebateAmount,
        NoTaxRebateAmount = o.NoTaxRebateAmount,
        Tax = o.Tax,
        TotalAmount = o.TotalAmount,
        Items = o.Items.Select(i => new InvoiceItemDto
        {
            Id = i.Id,
            OrdinalNumber = i.OrdinalNumber,
            ArticleName = i.ArticleName,
            Quantity = i.Quantity,
            Price = i.Price,
            NoTaxAmount = i.NoTaxAmount,
            RebatePercentage = i.RebatePercentage,
            RebateAmount = i.RebateAmount,
            NoTaxRebateAmount = i.NoTaxRebateAmount,
            Tax = i.NoTaxRebateAmount
        })
    };

    private Invoice MapInvoice(InvoiceRequest invoiceRequest)
    {
        return new Invoice
        {
            Id = invoiceRequest.Id,
            Date = invoiceRequest.Date,
            NoTaxAmount = invoiceRequest.NoTaxAmount,
            RebatePercentage = invoiceRequest.RebatePercentage,
            RebateAmount = invoiceRequest.RebateAmount,
            NoTaxRebateAmount = invoiceRequest.NoTaxRebateAmount,
            Tax = invoiceRequest.Tax,
            TotalAmount = invoiceRequest.TotalAmount,
            Partner = invoiceRequest.Partner,
            UserId = UserId,
            Items = invoiceRequest.Items.Select(i => new InvoiceItem
            {
                Id = i.Id,
                OrdinalNumber = i.OrdinalNumber,
                ArticleName = i.ArticleName,
                Quantity = i.Quantity,
                Price = i.Price,
                NoTaxAmount = i.NoTaxAmount,
                RebatePercentage = i.RebatePercentage,
                RebateAmount = i.RebateAmount,
                NoTaxRebateAmount = i.NoTaxRebateAmount,
                Tax = i.Tax,
                TotalAmount = i.TotalAmount
            }).ToList()
        };
    }
}