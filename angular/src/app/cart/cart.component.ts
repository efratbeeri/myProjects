import { Component, inject, numberAttribute, TemplateRef, ViewChild } from '@angular/core';
import { CartService } from '../../services/cart.service';
import { Purchases } from '../../models/purchases.module';
import { CommonModule } from '@angular/common';
import { MatIcon } from '@angular/material/icon';
import { MatDialog, matDialogAnimations, MatDialogModule } from '@angular/material/dialog';
import { Cart } from '../../models/cart.module';
import { JwtService } from '../../services/jwt.service';
import { Present } from '../../models/present.model';
import { AdminServiceService } from '../../services/admin-service.service';
import { forkJoin, map } from 'rxjs';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { PaymentComponent } from '../payment/payment.component';

@Component({
  selector: 'app-cart',
  imports: [
    MatCardModule,
    MatButtonModule,
    CommonModule,
    MatDialogModule],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent {
  //injects//

  cartSrv: CartService = inject(CartService)
  jwt: JwtService = inject(JwtService)
  PresentSrv: AdminServiceService = inject(AdminServiceService)
  dialog: MatDialog = inject(MatDialog)
  @ViewChild('detailDialogTemplate') detailDialogTemplate!: TemplateRef<any>;
  @ViewChild('confirmDelete2') confirmDelete2!: TemplateRef<any>;

  //variable//

  txt: string = ''
  token = localStorage.getItem('token')
  presentList: Present[] = []
  cart: Cart = { id: 0, userId: 0, final: false, purchasesList: [] };
  flag=false
  
  //function//

  ngOnInit() {
    // if (this.presentList.length == 0)
    //   this.txt = 'your cart is empty'
    this.cart.id = this.jwt.getCartIdFromToken(this.token!);
    this.cartSrv.getById(this.cart.id).subscribe({
      next: (cart) => {
        this.cart = cart;
        if(this.cart.final==true){
          this.flag=true
          this.txt='your cart is empty'
        }
          
        const requests = this.cart.purchasesList.map((present: any) => {
          if (!present.presentID) {
            console.warn(`presentID לא נמצא עבור האובייקט:`, present);
            return null; // התעלמות מפריטים לא תקינים
          }
          else{
            
          return this.PresentSrv.getById(present.presentID);

        }
        });
        const validRequests = requests.filter((req) => req !== null);

        // חיבור כל הבקשות באמצעות forkJoin
        forkJoin(validRequests).subscribe({
          next: (presentList: any[]) => {
            this.presentList = presentList; // שמירת המתנות ששלפנו
          },
          error: (err) => {
            console.error('שגיאה בשליפת המתנות:', err);
          },
        });
      },
      error: (err) => {
        console.error('שגיאה בשליפת הסל:', err);
      },
    });
  }

  toPayment(){
    this.dialog.open(PaymentComponent, {
      width: '33vw', // רבע מרוחב המסך
      height: '100vh', // כל גובה המסך
      position: { right: '0' }, // מיקום בצד ימין
      panelClass: 'payment-dialog' // מחלקת CSS מותאמת לדיאלוג
    });
  }



  confirmDelete(element: any): void {
    if (element) {
      this.dialog.open(this.confirmDelete2, {
        data: element
      });
    } else {
      console.error('לא ניתן להעביר את הנתונים לדיאלוג המחיקה');
    }
  }

  deleteItem(element: any): void {
    this.cartSrv.delete(element.id).subscribe((data)=>{})
  }

  increaseQuantity(data: any): void {
    data.quantity++;
  }

  decreaseQuantity(data: any): void {
    if (data.quantity > 0) data.quantity--;
  }

}
