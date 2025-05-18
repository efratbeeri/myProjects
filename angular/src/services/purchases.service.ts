import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable, map, tap } from 'rxjs';
import { Purchases } from '../models/purchases.module';

@Injectable({
  providedIn: 'root'
})
export class PurchasesService {
  BASE_URL='http://localhost:5187/api/Purchases'
  http:HttpClient=inject(HttpClient)
  constructor() { }

  getAll(): Observable<Purchases[]> {
    return this.http.get<any>(this.BASE_URL).pipe(
      map(response => response.$values || []) // חילוץ המערך מתוך `$values`
    );
  }

  getById(id: number): Observable<Purchases>{
    return this.http.get<any>(this.BASE_URL + '/' + id).pipe(
      map(response => response.$value || []) // חילוץ המערך מתוך `$values`
    );;
  }

  update(Purchases: Purchases): Observable<Purchases> {
    return this.http.put<Purchases>(`${this.BASE_URL}/${Purchases.id}`, Purchases); 
  }
  
  add(Purchases: Purchases): Observable<Purchases>{  
    return this.http.post<Purchases>(this.BASE_URL, Purchases);
  }
 
 
    delete(id: number){
    return this.http.delete(this.BASE_URL +'/' + id);
  }
  
}
