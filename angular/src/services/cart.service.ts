import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { BehaviorSubject, Observable, map, tap } from 'rxjs';
import { Cart } from '../models/cart.module';
import { Purchases } from '../models/purchases.module';
interface CartItem {  // הצהרת סוג לאובייקט שמכיל את cart
  cart: {
    purchasesList: Purchases[];
  };
}
@Injectable({
  providedIn: 'root'
})

export class CartService {
  private cartSubject = new BehaviorSubject<Cart | null>(null);
  cart$ = this.cartSubject.asObservable();

  BASE_URL = 'http://localhost:5187/api/Cart';
  http: HttpClient = inject(HttpClient)
  constructor() { }
 
  getAll(): Observable<Cart[]> {
    return this.http.get<any>(this.BASE_URL).pipe(
      map(response => response.$values || []) // חילוץ המערך מתוך `$values`
    );
  }

  getById(id: number): Observable<Cart> {
    return this.http.get<any>(this.BASE_URL + '/' + id).pipe(
      map(response => {
        // מחזירים את התשובה, כולל רכישת פרטי purchasesList
        const cart = response;
        cart.purchasesList = cart.purchasesList?.$values || []; // חילוץ המערך מתוך $values
        return cart;
      })
    );
  }
  
  update(cart: Cart): Observable<Cart> {  
    return this.http.put<Cart>(`${this.BASE_URL}/${cart.id}`, cart); 
  }
  

    delete(id: number){
    return this.http.delete(this.BASE_URL +'/' + id);
  }
 
  
  // קריאת רכישות מהשרת
  getAllPurchases(): Observable<Purchases[]> {
    return this.http.get<any>(this.BASE_URL).pipe(
      map(response => {
        return response.$values?.map((cartItem: any) => cartItem.cart.purchasesList).flat() || [];
      })
    );
  }
}
