import {Injectable} from '@angular/core';
import {
  HTTP_INTERCEPTORS,
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest
} from '@angular/common/http';
import {catchError, Observable, tap, throwError} from 'rxjs';
import {Router} from '@angular/router';
import {TokenService} from "../auth/token.service";
import {AccountService} from "../auth/account.service";
import {ErrorResponse} from "../auth/models/error-response";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private tokenService: TokenService, private userService: AccountService, private router: Router) {
  }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request)
      .pipe(
        tap(response => console.log(JSON.stringify(response))),
        catchError((error: HttpErrorResponse) => {
          console.log(JSON.stringify(error));
          let errorResponse: ErrorResponse = error.error;
          console.log(JSON.stringify(errorResponse));

          return throwError(() => errorResponse);
        })
      );
  }
}

export const ErrorInterceptorProvider = {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true};
