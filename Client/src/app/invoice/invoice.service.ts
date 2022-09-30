import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {map, Observable, Subscription} from 'rxjs';
import {environment} from 'src/environments/environment';
import Invoice from "./models/invoice";
import InvoiceItem from "./models/invoice-item";

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {

  constructor(private httpClient: HttpClient) {
  }

  getInvoices(): Observable<Invoice[]> {
    return this.httpClient.get<Invoice[]>(`${environment.apiUrl}/invoice`);
  }

  getInvoice(id: number): Observable<Invoice> {
    return this.httpClient.get<Invoice>(`${environment.apiUrl}/invoice/${id}`);
  }

  saveInvoice(invoice: Invoice): Observable<Invoice> {
    return this.httpClient.post<Invoice>(`${environment.apiUrl}/invoice`, invoice);
  }

  updateInvoice(invoice: Invoice): Observable<Invoice> {
    return this.httpClient.put<Invoice>(`${environment.apiUrl}/invoice`, invoice);
  }

  deleteInvoice(invoiceId: number) {
    return this.httpClient.delete<Invoice>(`${environment.apiUrl}/invoice/${invoiceId}`);
  }

  deleteInvoiceItem(itemId: number) {
    return this.httpClient.delete<Invoice>(`${environment.apiUrl}/invoice/item/${itemId}`);
  }

}
