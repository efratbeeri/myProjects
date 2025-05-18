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
import { AdminServiceService } from '../../../../services/admin-service.service';
import { Present } from '../../../../models/present.model';

@Component({
    selector: 'app-update',
    imports: [
      ReactiveFormsModule, 
      FormsModule, 
      MatFormFieldModule, 
      MatInputModule, 
      MatButtonModule, 
      MatDividerModule, 
      MatIconModule,
      CommonModule
    ],
    templateUrl: './update.component.html',
    styleUrl: './update.component.css'
})
export class UpdateComponent {
  presentService: AdminServiceService = inject(AdminServiceService);
  dialogRef = inject(MatDialogRef<UpdateComponent>);
    fb = inject(FormBuilder);
    frmPresent: FormGroup;
 
  constructor( @Inject(MAT_DIALOG_DATA) public data: { present: Present })
  {
    if (!data || !data.present) {
      throw new Error('Data is missing'); // בדוק אם הנתונים חסרים
    }
     {
    this.frmPresent = this.fb.group({
      name: [data.present.name, Validators.required],
      price: [data.present.price, [Validators.required, Validators.min(10)]],
      kategory: [data.present.kategory, Validators.required],
      image: [data.present.image, Validators.required],
      donorsId: [data.present.donorsId, Validators.required],
    });    
  }
  }

  save(): void {
    if (this.frmPresent.valid) {
      const updatedPresent: Present = {
        ...this.data.present, // שמירה על ה-ID ושדות נוספים
        name: this.frmPresent.get('name')?.value,
        price: this.frmPresent.get('price')?.value,
        kategory: this.frmPresent.get('kategory')?.value,
        image: this.frmPresent.get('image')?.value,
        donorsId: this.frmPresent.get('donorsId')?.value,
      };

      this.presentService.update(updatedPresent).subscribe({
        next: () => {
          console.log('Present updated successfully');
          this.dialogRef.close(true); // סוגר את הדיאלוג עם חיווי הצלחה
        },
        error: (err) => {
          console.error('Error updating present:', err);
        },
      });
    } else {
      console.warn('Form is invalid. Cannot save.');
    }
  }
}
