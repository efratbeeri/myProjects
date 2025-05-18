import { Component, OnInit, inject } from '@angular/core';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { Present } from '../../../models/present.model';
import { AdminServiceService } from '../../../services/admin-service.service';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { Donors } from '../../../models/donors.module';
import { AdminDonorsService } from '../../../services/admin-donors.service';
import { UpdateDonorsComponent } from '../updates/update/update-donors/update-donors.component';
import { AddComponent } from '../add/add/add.component';
import { AddDonorsComponent } from '../add/add-donors/add-donors.component';
@Component({
  selector: 'app-admin-donors',
  imports: [ 
    MatButtonModule,
    MatTableModule, 
    CommonModule, 
    MatDividerModule, 
    MatIconModule],
  templateUrl: './admin-donors.component.html',
  styleUrl: './admin-donors.component.css'
})
export class AdminDonorsComponent {
  crudSrv: AdminDonorsService = inject(AdminDonorsService);
  dialog = inject(MatDialog);

  // עמודות לתצוגה בטבלה
  displayedColumns: string[] = ['id', 'name', 'email', 'phone','delete'];
  // מקור נתונים לטבלה
  dataSource = new MatTableDataSource<Donors>();

  ngOnInit(): void {
    this.loadData(); // טוען נתונים מה-API בעת טעינת הקומפוננטה
  }

  loadData(): void {
    this.crudSrv.getAll().subscribe({
      next: (data) => {
        this.dataSource.data = data; // מעדכן את מקור הנתונים בטבלה
      },
      error: (err) => {
        console.error('Error loading data:', err); // טיפול בשגיאות
      },
    });
  }

  delete(id: number): void {
    this.crudSrv.delete(id).subscribe({
      next: () => {
        this.loadData(); // טוען מחדש את הנתונים לאחר מחיקה
      },
      error: (err) => {
        console.error('Error deleting item:', err); // טיפול בשגיאה
      },
    });
  }

  openAddDonorsDialog(): void {
    const dialogRef = this.dialog.open(AddDonorsComponent, {
      width: '400px',
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        // טוען מחדש את הנתונים לאחר סגירת הדיאלוג
        this.loadData();
      }
    });
}
openUpdateDialog(donors:Donors) {
  console.log('Opening dialog with data:', donors); // בדיקה
  const dialogRef = this.dialog.open(UpdateDonorsComponent, {
    width: '400px',
    data: { donors },
  });

  dialogRef.afterClosed().subscribe((result) => {
    if (result) {
      // טוען מחדש את הנתונים לאחר סגירת הדיאלוג
      this.loadData();
    }
  });
}
}
