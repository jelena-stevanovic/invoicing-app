import InvoiceItem from "./invoice-item";

export default interface Invoice {
  id: number;
  partner: string;
  date: string;
  noTaxAmount: number;
  rebatePercentage: number;
  rebateAmount: number;
  noTaxRebateAmount: number;
  tax: number;
  totalAmount: number;
  items: InvoiceItem[]
}
