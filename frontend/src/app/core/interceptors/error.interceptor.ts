import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

import { SnackbarServiceService } from '../services/snackbar-service.service';
import { SnackType } from 'src/app/shared/components/snackbar/snackbar.component';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

    constructor(
        private snack: SnackbarServiceService
    ) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request)
          .pipe(
            catchError((error: HttpErrorResponse) => {
              let errorMsg = '';
              if (error.status === 0) {
                errorMsg = "Você não tem acesso ou o servidor está offline";
              }
              else if (error.error instanceof ErrorEvent) {
                console.log('this is client side error');
                errorMsg = `Error: ${error.error.message}`;
              }
              else if (error.error instanceof Array) {
                errorMsg = 'Verifique os erros e tente novamente<br/>'
                error.error.forEach(err => {
                    errorMsg += `- ${err.message}<br/>`;
                });
              }
              else {
                console.log('this is server side error');
                errorMsg = `Error Code: ${error.status},  Message: ${error.message}`;
              }
              
              this.snack.openSnackBar(errorMsg, '', SnackType.Error)
              console.log(errorMsg);
              return throwError(errorMsg);
            })
          )
      }
    
    // intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    //     return next.handle(request).pipe(retry(1), catchError(err => {
    //         debugger
    //         let errorMessage = '';
    //         if (err.status === 0) {
    //             errorMessage = "Você não tem acesso ou o servidor está offline";
    //         }
    //         else if (err.error instanceof ErrorEvent || 'error' in err) {
    //             if (err.error instanceof Array){
    //                 errorMessage = 'Verifique os erros e tente novamente\n'
    //                 err.error.forEach(element => {
    //                     errorMessage += `- ${element.Message}\n`;
    //                 });
    //             }
    //             else {
    //                 errorMessage = 'message' in err.error ? `Error: ${err.error.message}` : `${err.error.Message}`;
    //             }
    //         } else {
    //             errorMessage = `Error Status: ${err.status}\nMessage: ${err.message}`;
    //         }

    //         this.snack.openSnackBar(errorMessage, '', SnackType.Error)
    //         console.log(err);
    //         return throwError(errorMessage);
    //     }))
    // }
}