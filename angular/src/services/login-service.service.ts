import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginServiceService {

  BASE_URL = 'http://localhost:5187/api/Auth/login';
  http:HttpClient=inject(HttpClient)
  constructor() {}

  login(credentials: { username: string, password: string }) {
    return this.http.post<{ token: string }>(this.BASE_URL, credentials)
        .pipe(tap(response => {
            localStorage.setItem('token', response.token);
        }));
}


}
