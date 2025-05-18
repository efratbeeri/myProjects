import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { User } from '../models/user.module';

@Injectable({
  providedIn: 'root'
})
export class UserServiceService {

  BASE_URL = 'http://localhost:5187/api/User';
 
   http: HttpClient = inject(HttpClient);
   constructor() { }
 
  
   getAll(): Observable<User[]> {
     return this.http.get<any>(this.BASE_URL).pipe(
       map(response => response.$values || []) // חילוץ המערך מתוך `$values`
     );
   }
   
 
   getById(id: number): Observable<User>{
     return this.http.get<any>(this.BASE_URL + '/' + id)
   }
 
   update(user: User): Observable<User> {
     return this.http.put<User>(`${this.BASE_URL}/${user.id}`, user); 
   }
   
   add(user: User): Observable<User>{
     return this.http.post<User>(this.BASE_URL, user);     
   }
  
     delete(id: number){
     return this.http.delete(this.BASE_URL +'/' + id);
   }
 }