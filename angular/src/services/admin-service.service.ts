import { Injectable, inject } from '@angular/core';
import { Present } from '../models/present.model';
import { AdminPresentComponent } from '../app/admin/admin-present/admin-present.component';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map, Observable, tap } from 'rxjs';
import { Winners } from '../models/winners.module';

@Injectable({
  providedIn: 'root'
})
export class AdminServiceService {
   BASE_URL = 'http://localhost:5187/api/Present';

  http: HttpClient = inject(HttpClient);
  constructor() { }

 
  getAll(): Observable<Present[]> {
    return this.http.get<any>(this.BASE_URL).pipe(
      map(response => response.$values || []) // חילוץ המערך מתוך `$values`
    );
  }

  getDrawnPresents(): Observable<Present[]> {
    return this.http.get<any>(`${this.BASE_URL}/GetDrawnPresents`).pipe(
      map(response => response.$values || []) // חילוץ המערך מתוך `$values`
    );
  }
  
  getById(id: number): Observable<Present> {
    return this.http.get<Present>(`${this.BASE_URL}/present/${id}`);
  }
  
 toDraw(id:number):Observable<Winners>{
  return this.http.get<Winners>(`${this.BASE_URL}/toDraw/${id}`);
 }
  searchPresent(name: string): Observable<Present[]> {
    return this.http.get<any>(`${this.BASE_URL}/present/search/${name}`).pipe(
      map(response => response.$values) // שליפת הנתונים מתוך $values
    );
  }

  update(present: Present): Observable<Present> {
    return this.http.put<Present>(`${this.BASE_URL}/${present.id}`, present); 
  }
  
  add(present: Present): Observable<Present>{
    console.log("i in service");
    console.log(present);
    
    return this.http.post<Present>(this.BASE_URL, present);
  }
 
    delete(id: number){
    return this.http.delete(this.BASE_URL +'/' + id);
  }
  doch(): Observable<string>{
      return this.http.get<string>(this.BASE_URL+'/doch')
  }
}

 
  
