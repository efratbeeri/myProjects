import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Donors } from '../models/donors.module';


@Injectable({
  providedIn: 'root'
})
export class AdminDonorsService {
BASE_URL='http://localhost:5187/api/Donor'
  http: HttpClient = inject(HttpClient);

  constructor() { }
 getAll(): Observable<Donors[]> {
    return this.http.get<any>(this.BASE_URL).pipe(
      map(response => response.$values || []) // חילוץ המערך מתוך `$values`
    );
  }

  getById(id: number): Observable<Donors>{
    return this.http.get<any>(this.BASE_URL + '/' + id).pipe(
      map(response => response.$value || []) // חילוץ המערך מתוך `$values`
    );;
  }

  update(Donors: Donors): Observable<Donors> {
    return this.http.put<Donors>(`${this.BASE_URL}/${Donors.id}`, Donors); 
  }
  
  add(Donors: Donors): Observable<Donors>{
    console.log("i add");
    
    return this.http.post<Donors>(this.BASE_URL, Donors);
  }
 
    delete(id: number){
    return this.http.delete(this.BASE_URL +'/' + id);
  }
}

 
  

