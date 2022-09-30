export default interface InvoiceItem {
  id: number;
  ordinalNumber: number;
  articleName: string;
  quantity: number;
  price: number;
  tax: number;
  totalAmount: number;
  noTaxAmount: number;
  rebatePercentage: number;
  rebateAmount: number;
  noTaxRebateAmount: number;
  invoiceId: number;
}
