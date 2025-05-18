import { Component, inject } from '@angular/core';
import { Cart } from '../../models/cart.module';
import { JwtService } from '../../services/jwt.service';
import { CartService } from '../../services/cart.service';
import { UserServiceService } from '../../services/user-service.service';
import { MatDialogRef } from '@angular/material/dialog';
import { LoginServiceService } from '../../services/login-service.service';
import { CommonModule } from '@angular/common';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import { FormsModule, NgForm, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';


@Component({
  selector: 'app-payment',
  imports: [ ReactiveFormsModule,
      MatFormFieldModule,
      MatInputModule,
      MatButtonModule,
      MatIconModule,
      CommonModule,
      FormsModule
    ],
  templateUrl: './payment.component.html',
  styleUrl: './payment.component.css'
})
export class PaymentComponent {
  //injects//

  jwt: JwtService = inject(JwtService)
  cartSrv: CartService = inject(CartService)
  logSrv: LoginServiceService = inject(LoginServiceService);
  dialogRef:MatDialogRef<PaymentComponent>=inject(MatDialogRef<PaymentComponent>)

  //variable//

  token = localStorage.getItem('token')
  cart: Cart = { id: 0, userId: 0, final: false, purchasesList: [] };
  credentials = { username: '', password: '' }
  currentStep: number = 0; // משתנה עבור השלב הנוכחי
  steps = [
    // { label: 'פרטי עסקה' },
    // { label: 'פרטי לקוח' },
    { label: 'פרטי תשלום' }
  ];
  purchaseComplete: boolean = false;
  creditCardNumber= '';
  expiryDate= '';
  cvv= '';
  isProcessing: boolean = false;
  creditCardMask = ['0', '0', '0', '0', ' ', '0', '0', ' ', '0', '0', '0', '0', ' ', '0', '0', '0', '0', ' '];
  num=0
  sum=0
  userid=0

  //punction//

  formValid() {
    return this.creditCardNumber && this.expiryDate && this.cvv && this.creditCardNumber.length === 16;
  }

  // פעולה שיכולה להפעיל את האנימציה
  submitPayment() {
    if (this.formValid()) {
      this.isProcessing = true;
      // כאן תוסיף את קוד הסליקה שלך (לשלוח את הנתונים לשרת)
      setTimeout(() => {
        this.purchaseComplete = true;
        this.isProcessing = false;
      }, 3000); // הדמיה של זמן עיבוד
    }
  }
ngOnInit(){
  this.num=this.cart.purchasesList.length
  this.sum=50
  this.userid=this.cart.userId
}
  completePurchase() {
    this.isProcessing = true;
    // כאן יכול להיבצע הליך עיבוד נתוני הסליקה
    this.cart.id = this.jwt.getCartIdFromToken(this.token!);
    this.cart.final = true
    this.num=this.cart.purchasesList.length
    this.sum=50
    this.cartSrv.update(this.cart).subscribe(() => {
    })
    setTimeout(() => {
      this.showSuccessMessage();
      this.isProcessing = false;
    }, 4000); // זמן עיבוד לדמיון של סליקה (נניח 3 שניות)
  }
  showSuccessMessage() {
    this.purchaseComplete = true;
    setTimeout(() => {
      this.closeDialog();
    }, 3000); // אחרי 3 שניות הדיאלוג ייסגר
  }

  toPayment() {
    this.cart.id = this.jwt.getCartIdFromToken(this.token!);
    this.cart.final = true
    this.cartSrv.update(this.cart).subscribe(() => {
    })
    this.credentials.username=localStorage.getItem('username') || ''
    this.credentials.password=localStorage.getItem('password') || ''
    this.logSrv.login(this.credentials).subscribe({
    })
  }

  closeDialog() {
    if (!this.purchaseComplete) {
      this.purchaseComplete = false; // אם לא הסתיימה הרכישה, הסתר את ההודעה
    }
    this.dialogRef.close()
  }

  goToNextStep() {
    if (this.currentStep < this.steps.length - 1) {
      this.currentStep++;
    }
  }

  // חזרה לשלב קודם של הדיאלוג
  goToStep(i: number) {
    this.currentStep = i;
  }
}


