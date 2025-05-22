import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse  } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Book } from '../models/book.model';

@Injectable({ providedIn: 'root' })
export class BookService {
  private apiUrl = 'https://localhost:7284/api/Books';

  constructor(private http: HttpClient) {}

  getBooks(): Observable<Book[]> {
    return this.http.get<Book[]>(this.apiUrl)
    .pipe(
        catchError(this.handleError)
      );
  }

  addBook(book: Book): Observable<Book> {
    return this.http.post<Book>(this.apiUrl, book)
      .pipe(
        catchError(this.handleError)
      );
  }

  updateBook(book: Book): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${book.id}`, book)
    .pipe(
        catchError(this.handleError)
      );
  }

  deleteBook(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`)
    .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'An unknown error occurred!';
    
    if (error.error instanceof ErrorEvent) {
      errorMessage = `Error: ${error.error.message}`;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    
    console.error(errorMessage);
    return throwError(() => errorMessage);
  }
}