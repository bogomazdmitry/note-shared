import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { AuthService } from './auth.service';
import { Observable, throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(public authService: AuthService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    request = request.clone({
      setHeaders: {
        Authorization: `Bearer ${this.authService.getAccessToken()}`
      }
    });
    return next.handle(request)
      .pipe(catchError(this.handleError));
  }

  handleError(error: HttpErrorResponse): Observable<HttpEvent<any>> {
    if(error && error.status === 400 && error.error && error.error.error === 'invalid_grant'){
      return this.handle400Error(error);
    }
    else if(error && error.status === 401){
      return this.handle400Error(error);
    }
    return throwError(error);
  }

  handle400Error(error: HttpErrorResponse): Observable<HttpEvent<any>> {
    return new Observable<any>();
  }

  handle401Error(error: HttpErrorResponse): Observable<HttpEvent<any>> {
    return new Observable<any>();
  }
}
