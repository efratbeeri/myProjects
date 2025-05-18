import { Component, inject } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { AdminDonorsService } from '../../../../services/admin-donors.service';
import { Donors } from '../../../../models/donors.module';

@Component({
  selector: 'app-add-donors',
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    CommonModule
  ],
  templateUrl: './add-donors.component.html',
  styleUrl: './add-donors.component.css'
})
export class AddDonorsComponent {

  adminDonorsService = inject(AdminDonorsService);
  dialogRef = inject(MatDialogRef<AddDonorsComponent>);
  fb = inject(FormBuilder);

  frmDonor: FormGroup;

  constructor() {
    this.frmDonor = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required]],
      phone: ['', Validators.required],
    });
  }

  save(): void {
    if (this.frmDonor.valid) {
      const donor: Donors = {
        id: 0, // ID יוגדר בצד השרת
        name: this.frmDonor.get('name')?.value,
        email: this.frmDonor.get('email')?.value,
        phone: this.frmDonor.get('phone')?.value,
      };
  
      this.adminDonorsService.add(donor).subscribe({
        next: () => {
          this.dialogRef.close(true); // סוגר את הדיאלוג עם חיווי של הצלחה
        },
        error: (err) => {
          console.error('Error adding donor:', err);
        },
      });
    }else {
      console.warn('Form is invalid. Cannot save.');
    }
  }
  
}

