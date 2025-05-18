import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class FileService {
  http:HttpClient=inject(HttpClient)
  
  constructor() { }

  saveWinner(presentId: number, userId: number): Observable<string[]> {
    return this.http.post<string[]>(`/api/present/saveWinner`, { presentId, userId });
  }

  saveRevenue(): Observable<string> {
    return this.http.post<string>(`/api/present/saveRevenue`, {});
  }
}
