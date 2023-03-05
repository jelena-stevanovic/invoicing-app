import {Component, OnInit} from '@angular/core';
import {map, Observable, of, switchMap} from 'rxjs';
import Invoice from "./models/invoice";
import {InvoiceService} from "./invoice.service";
import {ActivatedRoute} from "@angular/router";
import InvoiceItem from "./models/invoice-item";
import { Router } from '@angular/router';


@Component({
  selector: 'app-invoices',
  templateUrl: './invoices.component.html',
  styleUrls: ['./invoices.component.css']
})
export class InvoicesComponent implements OnInit {

  newInvoiceItem: InvoiceItem = getEmptyInvoiceItem();
  invoice$: Observable<Invoice> | undefined;
  isNew = false;

  private invoiceId = 0;

  constructor(private invoiceService: InvoiceService, private route: ActivatedRoute, private router: Router) {
  }

  ngOnInit(): void {
    this.invoice$ = this.route.params.pipe(
      map((p) => p['id']),
      switchMap((id) => {
        this.isNew = id == 0;
        this.invoiceId = id;
        return this.isNew ? this.getInvoice({}) : this.invoiceService.getInvoice(id);
      })
    );
  }

  addInvoiceItem(invoice: Invoice, event: Event): void {
    event.stopPropagation();
    event.preventDefault();

    let updatedInvoice: Invoice = {
      ...invoice,
      items: [
        ...invoice.items,
        this.newInvoiceItem,
      ] as InvoiceItem[]
    };

    this.newInvoiceItem = getEmptyInvoiceItem();
    this.invoice$ = this.getInvoice(updatedInvoice);
  }

  updateInvoice(invoice: Invoice) {
    console.log('inside updateinvoice');
    console.log(`invoice id is ${invoice.id}`);
    this.invoice$ = this.getInvoice(invoice);
  }

  removeInvoiceItem(invoice: Invoice, invoiceItem: InvoiceItem) {
    console.log('inside removeInvoice');
    console.log(`invoice id is ${invoiceItem.id}`);
    invoice.items.splice(invoice.items.indexOf(invoiceItem), 1);
    this.invoice$ = this.getInvoice(invoice);
  }

  saveInvoice(invoice: Invoice) {
    this.invoiceService.saveInvoice(invoice).subscribe((result) => {
      this.router.navigate(['/invoices', result.id]);
    });
  }


  recalculateItem(item: InvoiceItem): void {
    item.noTaxAmount = +(item.quantity * item.price).toFixed(2);
    item.rebateAmount = +(item.noTaxAmount * item.rebatePercentage / 100).toFixed(2);
    item.noTaxRebateAmount = +(item.noTaxAmount - item.rebateAmount).toFixed(2);
    item.tax = +(item.noTaxRebateAmount * 0.17).toFixed(2);
    item.totalAmount = item.noTaxRebateAmount + item.tax;
  }

  private getInvoice(invoice: Partial<Invoice>): Observable<Invoice> {
    invoice = {
      ...getEmptyInvoice(),
      ...invoice
    };
    return of(this.recalculate(invoice as Invoice));
  }

  private recalculate(invoice: Invoice): Invoice {
    invoice.noTaxAmount = 0;
    invoice.rebateAmount = 0;
    invoice.noTaxRebateAmount = 0;
    invoice.tax = 0;
    invoice.totalAmount = 0;

    invoice.items.forEach(i => {
      invoice.noTaxAmount += i.noTaxAmount;
      invoice.rebateAmount += i.rebateAmount;
      invoice.noTaxRebateAmount += i.noTaxRebateAmount;
      invoice.tax += i.tax;
      invoice.totalAmount += i.totalAmount;
    })

    invoice.rebatePercentage = +((invoice.rebateAmount / invoice.noTaxAmount) * 100).toFixed(2);
    invoice.rebatePercentage = isNaN(invoice.rebatePercentage) ? 0 : invoice.rebatePercentage;

    return invoice;
  }

}

function getEmptyInvoice(): Invoice {
  return {
    id: 0,
    partner: 'Partner Name',
    date: new Date().toISOString(),
    noTaxAmount: 0,
    rebatePercentage: 0,
    rebateAmount: 0,
    noTaxRebateAmount: 0,
    tax: 0,
    totalAmount: 0,
    items: [],
  }
}

function getEmptyInvoiceItem(): InvoiceItem {
  return {
    articleName: "Article Name",
    id: 0,
    ordinalNumber: 0,
    invoiceId: 0,
    tax: 0,
    totalAmount: 0,
    noTaxAmount: 0,
    noTaxRebateAmount: 0,
    price: 0,
    quantity: 0,
    rebateAmount: 0,
    rebatePercentage: 0
  }
}
