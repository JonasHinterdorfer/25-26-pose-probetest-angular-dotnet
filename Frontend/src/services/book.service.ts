import {HttpClient, HttpParams} from '@angular/common/http';
import {inject, Injectable} from '@angular/core';
import {BookCreateDto, BookDto} from '../app/book.model';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  httpClient: HttpClient = inject(HttpClient);
  readonly baseUrl: string = 'http://localhost:5124/api/books';

  getBooks(): Observable<BookDto[]> {
    return this.httpClient.get<BookDto[]>(this.baseUrl);
  };

  getBookById(id : number): Observable<BookDto> {
    return this.httpClient.get<BookDto>(this.baseUrl + '/' + id);
  };

  postBook(book: BookCreateDto): Observable<BookDto> {
    return this.httpClient.post<BookDto>(this.baseUrl, book);
  }
  putBook(id:number, book: BookCreateDto): Observable<BookDto> {
    return this.httpClient.post<BookDto>(this.baseUrl + '/' + id, book);
  }
  exitsTitile(title:string): Observable<boolean> {
    const params = new HttpParams().set('name', title);
    return this.httpClient.get<boolean>(this.baseUrl + '/titleExists', {params: params});
  }
  deleteBook(id:number): Observable<void> {
    return this.httpClient.delete<void>(this.baseUrl + '/' + id);
  }
}
