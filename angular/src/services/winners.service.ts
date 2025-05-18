import { HttpClient, HttpRequest } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Winners } from '../models/winners.module';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WinnersService {
   BASE_URL='http://localhost:5187/api/Winners'
   http:HttpClient=inject(HttpClient)
  constructor() { }

    get(): Observable<Winners[]> {
       return this.http.get<any>(this.BASE_URL)
     }
     
   
     getById(id: number): Observable<Winners>{
       return this.http.get<any>(this.BASE_URL + '/' + id).pipe(
         map(response => response.$value || []) // חילוץ המערך מתוך `$values`
       );;
     }
    add(winners: Winners){
        return this.http.post<Winners>(this.BASE_URL,winners);     
      }
}
