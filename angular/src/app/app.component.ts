import { RouterModule, RouterOutlet } from '@angular/router';
import { AdminPresentComponent } from './admin/admin-present/admin-present.component';
import { Component, HostListener, inject } from '@angular/core';
import { HomeComponent } from "./home/home.component";
import { AdminDonorsComponent } from "./admin/admin-donors/admin-donors.component";
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { JwtService } from '../services/jwt.service';
import { CommonModule } from '@angular/common';



@Component({
    selector: 'app-root',
    standalone: true,
    imports: [RouterOutlet, RouterModule, MatIconModule, MatTooltipModule, CommonModule],
    templateUrl: './app.component.html',
    styleUrl: './app.component.css'
})
export class AppComponent {
    jwt: JwtService = inject(JwtService)
    role = ''
    isValid: boolean = false
    isAdmin=false


    ngOnInit(){
        console.log("i start");
        this.LoadData()
    }
    LoadData() {
      const token = localStorage.getItem('token')
        console.log("i load");
        if(!token)
            this.isAdmin=false
        else{
            const decodedToken = this.decodeJwtToken(token); 
            this.role = decodedToken['Role'] || ''; 
            console.log("role: "+this.role);         
            if(this.role=="admin") 
                this.isAdmin=true
            
        }
    }
 
    decodeJwtToken(token: string | null): any {
        if (!token || token.split('.').length < 3) {
            console.error("Invalid token format or missing token.");
            return null;
        }

        try {
            const base64Url = token.split('.')[1];
            const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
            const jsonPayload = decodeURIComponent(
                atob(base64)
                    .split('')
                    .map(c => `%${('00' + c.charCodeAt(0).toString(16)).slice(-2)}`)
                    .join('')
            );

            return JSON.parse(jsonPayload);
        } catch (error) {
            console.error("Error decoding token:", error);
            return null;
        }
    }

   
    logout() {
        localStorage.setItem('token', 'null')
        this.isAdmin=false
    }
}
