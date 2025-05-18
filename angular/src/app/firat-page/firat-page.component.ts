import { RouterOutlet } from '@angular/router';
import { Component, inject } from '@angular/core';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatButtonModule } from '@angular/material/button';
import { AdminServiceService } from '../../services/admin-service.service';
import { AdminPresentComponent } from '../admin/admin-present/admin-present.component';
import { Present } from '../../models/present.model';
import { RouterModule } from '@angular/router';
import { Router } from '@angular/router';
import { LoginServiceService } from '../../services/login-service.service';
import { MatDialogRef } from '@angular/material/dialog';
import { AddComponent } from '../admin/add/add/add.component';
import { UserServiceService } from '../../services/user-service.service';
import { AppComponent } from '../app.component';


@Component({
  selector: 'app-firat-page',
  imports: [
    ReactiveFormsModule, 
    FormsModule, 
    MatFormFieldModule, 
    MatInputModule, 
    MatButtonModule, 
    MatDividerModule, 
    MatIconModule,
    RouterModule],
  templateUrl: './firat-page.component.html',
  styleUrl: './firat-page.component.css'
})
export class FiratPageComponent {
  logSrv:  LoginServiceService= inject(LoginServiceService);
  router=inject(Router)
  fb = inject(FormBuilder);
  app:AppComponent=inject(AppComponent)

  username: string = '';
  password: string = '';
  frmLogin: FormGroup;

  constructor() {
    this.frmLogin = this.fb.group({
          Username: ['', [Validators.required]],
          Password: [null, [Validators.required]],
        });
  }

  onSubmit() {
    localStorage.setItem('username',this.username)
    localStorage.setItem('password',this.password)
    this.app.isAdmin=false
    this.logSrv.login(this.frmLogin.value).subscribe({
      next: (response) => {
     
        if(response.token=="שם משתמש או סיסמה לא נכונים!")  {
          alert("שם משתמש או סיסמה לא נכונים!")
          return
        }        
        localStorage.setItem('token', response.token); // שמירת הטוקן ב-localStorage
        this.router.navigate(['/home']); // ניתוב לעמוד הבא
        console.log(localStorage.getItem('token'));
        this.app.LoadData()
        
      },
      error: (err) => {
        alert('התחברות נכשלה!');
        console.error(err);
      }
    });
  }
 
  }

