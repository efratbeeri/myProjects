import { Component, inject } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { AdminServiceService } from '../../../../services/admin-service.service';
import { Present } from '../../../../models/present.model';

@Component({
  selector: 'app-add',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    CommonModule
  ],
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css'],
})
export class AddComponent {
  presentService:AdminServiceService = inject(AdminServiceService);
  dialogRef = inject(MatDialogRef<AddComponent>);
  fb = inject(FormBuilder);
  frmPresent: FormGroup;

  constructor() {
    this.frmPresent = this.fb.group({
      name: ['', Validators.required],
      price: [null, [Validators.required, Validators.min(10)]],
      kategory: ['', Validators.required],
      image: ['', Validators.required],
      donorsId: [null, Validators.required],
    });
  }

  save(): void {
    if (this.frmPresent.valid) {
      const present: Present = {
        id: 0, // ID יוגדר בצד השרת
        name: this.frmPresent.get('name')?.value,
        price: this.frmPresent.get('price')?.value,
        kategory: this.frmPresent.get('kategory')?.value,
        image: this.frmPresent.get('image')?.value,
        donorsId: this.frmPresent.get('donorsId')?.value,
        isDrawn:false,
        winnerName:''
      };
  
      this.presentService.add(present).subscribe({
        next: () => {
          this.dialogRef.close(true); // סוגר את הדיאלוג עם חיווי של הצלחה
          console.log("sent to sevice");
          
        },
        error: (err) => {
          console.error('Error adding present:', err);
        },
      });
    }else {
      console.warn('Form is invalid. Cannot save.');
    }
  }
  
}
