using System.Text.Json.Serialization;

namespace InvoiceApp.Service.Models;

public class DeleteInvoiceResponse : BaseResponse
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int InvoiceId { get; set; }
}