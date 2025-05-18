import { Routes } from '@angular/router';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import { AdminPresentComponent } from './admin/admin-present/admin-present.component';
import { AdminDonorsComponent } from './admin/admin-donors/admin-donors.component';
import { FiratPageComponent } from './firat-page/firat-page.component';
import { CartComponent } from './cart/cart.component';
import { PurchasesComponent } from './admin/purchases/purchases.component';
import { TheLotteryComponent } from './admin/the-lottery/the-lottery.component';
import { PaymentComponent } from './payment/payment.component';

export const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },    
    { path: 'register', component: RegisterComponent },
    { path: 'home', component: HomeComponent },
    { path: 'admin-present', component: AdminPresentComponent },
    { path: 'admin-donors', component: AdminDonorsComponent },
    { path: 'cart', component: CartComponent },
    { path: 'payment', component: PaymentComponent },
    { path: 'theLottery', component: TheLotteryComponent },
    { path: 'first-page', component: FiratPageComponent },
    { path: 'register', component: RegisterComponent },

    // { path: 'dashboard', component: DashboardComponent }, 





];
