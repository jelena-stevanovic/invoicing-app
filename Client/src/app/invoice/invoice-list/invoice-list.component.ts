import {Component, OnInit} from '@angular/core';
import {Observable} from 'rxjs';
import Invoice from "../models/invoice";
import {InvoiceService} from "../invoice.service";

@Component({
  selector: 'app-invoice-list',
  templateUrl: './invoice-list.component.html'
})
export class InvoiceListComponent implements OnInit {

  invoices$: Observable<Invoice[]> | undefined;

  constructor(private invoiceService: InvoiceService) {
  }

  ngOnInit(): void {
    this.invoices$ = this.invoiceService.getInvoices();
  }

  removeInvoice(invoice: Invoice) {
    console.log('inside removeInvoice');
    console.log(`invoice id is ${invoice.id}`);
    this.invoiceService.deleteInvoice(invoice.id).subscribe(() => this.invoices$ = this.invoiceService.getInvoices());
  }
}
