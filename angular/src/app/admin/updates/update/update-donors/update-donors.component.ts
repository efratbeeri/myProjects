import { RouterOutlet } from '@angular/router';
import { Component, Inject, inject } from '@angular/core';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AdminDonorsService } from '../../../../../services/admin-donors.service';
import { Donors } from '../../../../../models/donors.module';

@Component({
  selector: 'app-update-donors',
  imports: [
    ReactiveFormsModule, 
    FormsModule, 
    MatFormFieldModule, 
    MatInputModule, 
    MatButtonModule, 
    MatDividerModule, 
    MatIconModule,
    CommonModule],
  templateUrl: './update-donors.component.html',
  styleUrl: './update-donors.component.css'
})
export class UpdateDonorsComponent {

  AdminDonorsService: AdminDonorsService = inject(AdminDonorsService);
  dialogRef = inject(MatDialogRef<UpdateDonorsComponent>);
    fb = inject(FormBuilder);
    frmPresent: FormGroup;
 
  constructor( @Inject(MAT_DIALOG_DATA) public data: { donors: Donors })
  {
    if (!data || !data.donors) {
      throw new Error('Data is missing'); // בדוק אם הנתונים חסרים
    }
     {
    this.frmPresent = this.fb.group({
      name: [data.donors.name, Validators.required],
      email: [data.donors.email, [Validators.required]],
      phone: [data.donors.phone, Validators.required],
    });    
  }
  }

  save(): void {
    if (this.frmPresent.valid) {
      const updateddonors: Donors = {
        ...this.data.donors, // שמירה על ה-ID ושדות נוספים
        name: this.frmPresent.get('name')?.value,
        email: this.frmPresent.get('email')?.value,
        phone: this.frmPresent.get('phone')?.value,
      };

      this.AdminDonorsService.update(updateddonors).subscribe({
        next: () => {
          console.log('donors updated successfully');
          this.dialogRef.close(true); // סוגר את הדיאלוג עם חיווי הצלחה
        },
        error: (err) => {
          console.error('Error updating donors:', err);
        },
      });
    } else {
      console.warn('Form is invalid. Cannot save.');
    }
  }
}

