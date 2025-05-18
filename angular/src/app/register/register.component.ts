import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { UserServiceService } from '../../services/user-service.service';
import { User } from '../../models/user.module';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    CommonModule
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  UserService = inject(UserServiceService);
  fb = inject(FormBuilder);
  frmUser: FormGroup;


  constructor(private router: Router) {
    this.frmUser = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required,Validators.email]],
      phone: ['', [Validators.required, Validators.pattern('^[0-9]{10}$')]],
      userName: ['', Validators.required],
      password:[null,[Validators.required,Validators.minLength(4),Validators.maxLength(8)]]

    });
  }


  save() {
    if (this.frmUser.valid) {
      const user: User = {
        id: 0, // ID יוגדר בצד השרת
        name: this.frmUser.get('name')?.value,
        email: this.frmUser.get('email')?.value,
        phone: this.frmUser.get('phone')?.value,
        userName: this.frmUser.get('userName')?.value,
        password:this.frmUser.get('password')?.value,
        role:'user'
      };

      this.UserService.add(user).subscribe({
        next: () => {
            this.router.navigate(['/home']);
        }, error: (err) => {
          console.error('Error adding user:', err);
        },
      });

    }
  }
}
