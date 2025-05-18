import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { AdminServiceService } from '../../services/admin-service.service';
import { MatTableDataSource } from '@angular/material/table';
import { Present } from '../../models/present.model';
import { CommonModule } from '@angular/common';
import { MatIcon } from '@angular/material/icon';
import { CartService } from '../../services/cart.service';
import { Purchases } from '../../models/purchases.module';
import { JwtService } from '../../services/jwt.service';
import { PurchasesService } from '../../services/purchases.service';
import { AppComponent } from '../app.component';
import { Winners } from '../../models/winners.module';
import { UserServiceService } from '../../services/user-service.service';
import { User } from '../../models/user.module';
import { WinnersService } from '../../services/winners.service';
import { Cart } from '../../models/cart.module';
import { FileService } from '../../services/file-service.service';
@Component({
  selector: 'app-home',
  imports: [MatCardModule, MatButtonModule, CommonModule, MatIcon],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',

})
export class HomeComponent {
  //injects//

  crudSrv: AdminServiceService = inject(AdminServiceService);
  cartServ: CartService = inject(CartService)
  cardSrv: PurchasesService = inject(PurchasesService)
  userSrv: UserServiceService = inject(UserServiceService)
  winSrv: WinnersService = inject(WinnersService)
  app: AppComponent = inject(AppComponent)
  jwt: JwtService = inject(JwtService)
  fileService:FileService=inject(FileService)

  //variable//

  token = localStorage.getItem('token')
  presentList: Present[] = []
  showOverlay = false; // מצביע אם להציג את המסך השחור
  countdownValue = 3; // ערך הספירה לאחור
  winnerName = ''; // שם הזוכה
  card: Purchases = { id: '', amount: 0, presentID: 0, cartId: 0 }
  winner: Winners = { id: 0, userId: 0, presentId: 0 }
  cart: Cart | null= { id: 0, userId: 0, final: false, purchasesList: [] } 
  purch: Purchases = { id: '', cartId: 0, presentID: 0, amount: 0 }
  amount = 0
  winnerMap: Map<number, string> = new Map(); // מפה לשמירת מזהי המתנות ושמות הזוכים
  userId = 0
  sums=''
  //function//

  ngOnInit() {
    this.token = localStorage.getItem('token');
    if (!this.token || this.token === 'null') {
      alert('You need to log in.');
      return;
    }
  
    const cartId = this.jwt.getCartIdFromToken(this.token);
    this.loadCart(cartId); 
  }
  
  private loadCart(cartId: number): void {
    this.crudSrv.getAll().subscribe({
          next: (data) => {
            this.presentList = [...data];
            console.log(this.presentList);
          },
          error: (err) => {
            console.error('Error fetching presents:', err);
          }
        });
        // שליפת המתנות שהוגרלו
        this.crudSrv.getDrawnPresents().subscribe({
          next: (drawnPresents) => {
            drawnPresents.forEach(present => {
              this.winnerMap.set(present.id, ''); // עדכון המפה עם שמות הזוכים
            });
            console.log("WinnerMap updated:", this.winnerMap);
          },
          error: (err) => {
            console.error('Error fetching drawn presents:', err);
          }
        });
        this.cartServ.cart$.subscribe((updatedCart) => {
          this.cart = updatedCart;
        });
  }

  addToCart(presentId: number | string): void {
    // בדיקת טוקן למשתמש
    this.token = localStorage.getItem('token');
    if (!this.token || this.token === 'null') {
      alert('You need to log in.');
      return;
    }
  
    // חילוץ cartId מהטוקן
    const cartId = this.jwt.getCartIdFromToken(this.token);
  
    // שליפת הסל הנוכחי
    this.cartServ.getById(cartId).subscribe({
      next: (cart) => {
        this.cart = cart;
  
        // חיפוש רכישה קיימת עם אותו presentID
        const existingPurchase = this.cart.purchasesList.find(
          (purchase) => purchase.presentID === +presentId
        );
  
        if (existingPurchase) {
          // אם הרכישה כבר קיימת, עדכון הכמות שלה
          existingPurchase.amount += 1; // הגדלת הכמות ב-1
          this.cardSrv.update(existingPurchase).subscribe({
            next: (updatedPurchase) => {
              console.log('Purchase updated:', updatedPurchase);
              alert('Item quantity updated!');
            },
            error: (err) => {
              console.error('Error updating item quantity:', err);
              alert('Failed to update item quantity.');
            },
          });
        } else {
          // אם הרכישה לא קיימת, יצירת רכישה חדשה
          const newPurchase: Purchases = {
            id: '', // ID יווצר על ידי השרת
            amount: 1, // כמות התחלתית
            presentID: typeof presentId === 'string' ? Number(presentId) : presentId,
            cartId: cartId,
          };
  
          this.cardSrv.add(newPurchase).subscribe({
            next: (addedPurchase) => {
              console.log('Purchase added:', addedPurchase);
              alert('Item added to cart!');
            },
            error: (err) => {
              console.error('Error adding item to cart:', err);
              alert('Failed to add item to cart.');
            },
          });
        }
      },
      error: (err) => {
        console.error('Error retrieving cart:', err);
        alert('An error occurred while retrieving the cart.');
      },
    });
  }
  
  
  removeFromCart(presentId: number | string): void {
    this.token = localStorage.getItem('token');
    if (!this.token || this.token === 'null') {
      alert('You need to log in.');
      return;
    }
  
    const cartId = this.jwt.getCartIdFromToken(this.token);
  
    this.cartServ.getById(cartId).subscribe({
      next: (cart) => {
        this.cart = cart;
  
        const existingPurchase = this.cart.purchasesList.find(
          (purchase) => purchase.presentID === +presentId
        );
  
        if (existingPurchase) {
          if (existingPurchase.amount > 1) {
            // אם הכמות יותר מ-1, מורידים כמות ב-1
            existingPurchase.amount -= 1;
            this.cardSrv.update(existingPurchase).subscribe({
              next: () => {
                console.log('Quantity decreased by 1.');
                alert('Item quantity decreased.');
              },
              error: (err) => {
                console.error('Error decreasing item quantity:', err);
                alert('Failed to decrease item quantity.');
              },
            });
          } else {
            // אם הכמות היא 1, מוחקים את הרכישה
            this.cardSrv.delete(Number(existingPurchase.id)).subscribe({
              next: () => {
                console.log('Purchase removed successfully.');
                alert('Item removed from cart.');
              },
              error: (err) => {
                console.error('Error removing item from cart:', err);
                alert('Failed to remove item from cart.');
              },
            });
          }
        } else {
          alert('The selected item was not found in the cart.');
        }
      },
      error: (err) => {
        console.error('Error retrieving cart:', err);
        alert('An error occurred while retrieving the cart.');
      },
    });
  }

  
  
  // getAmountForPresent(presentId: number): number {
  //   if (!this.cart || !this.cart.purchasesList) return 0;

  //   const purchase = this.cart.purchasesList.find(
  //     (p) => p.presentID === presentId
  //   );
  //   return purchase ? purchase.amount : 0;
  // }
  

  toDraw(id: number) {
   console.log(id,"id");
   
    this.crudSrv.toDraw(id).subscribe((data: Winners) => {
      this.winner.presentId = data.presentId
      this.winner.userId = data.userId

      this.winner.userId = data.userId;
      this.winner = data;
      console.log(data,"data");

      this.userId = this.winner.userId;
      this.winSrv.add(this.winner).subscribe((data) => { })
      console.log(this.userId);

      // קריאה לקבלת שם המשתמש
      this.userSrv.getById(this.userId).subscribe((userData: User) => {

        this.winnerName = userData.name; // עדכון שם הזוכה
        console.log(this.winnerName);

        this.winnerMap.set(id, userData.name); // הוספת שם הזוכה למפה
        console.log("winnerMap updated:", this.winnerMap);
      });
       this.crudSrv.doch().subscribe((data)=>{
            this.sums=data
       }) 

      console.log("winner", this.winner);

    alert("gooooood")
    }
    );
  }

  isWinner(presentId: number): boolean {
    return this.winnerMap.has(presentId);
  }

  // getWinnerName(presentId: number): string {

  //   this.crudSrv.getById(presentId).subscribe((data) => {
  //     this.winnerName = data.winnerName || ''
  //   })
  //   this.winnerMap.set(presentId, this.winnerName)
  //   return this.winnerMap.get(presentId) || '';
  // }




  // onDraw(presentId: number, userId: number): void {
  //   this.fileService.saveWinner(presentId, userId).subscribe(
  //     response => {
  //       console.log('הזוכה נשמר בקובץ:', response);
  //     },
  //     error => {
  //       console.error('שגיאה בשמירה:', error);
  //     }
  //   );
    
  //   this.fileService.saveRevenue().subscribe(
  //     response => {
  //       console.log('הכנסות נשמרו בקובץ:', response);
  //     },
  //     error => {
  //       console.error('שגיאה בשמירה:', error);
  //     }
  //   );
  // }


}
