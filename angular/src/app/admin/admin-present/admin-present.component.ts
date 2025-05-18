import { Component, OnInit, inject } from '@angular/core';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { Present } from '../../../models/present.model';
import { AdminServiceService } from '../../../services/admin-service.service';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { UpdateComponent } from '../updates/update/update.component';
import { AddComponent } from '../add/add/add.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { AdminDonorsService } from '../../../services/admin-donors.service';


@Component({
  selector: 'app-admin-present',
  standalone: true,
  imports: [
    MatButtonModule,
     MatTableModule, 
     CommonModule, 
     MatDividerModule, 
     MatIconModule,
     MatFormFieldModule,
     MatInputModule,
     ],
  templateUrl: './admin-present.component.html',
  styleUrls: ['./admin-present.component.css']
})
export class AdminPresentComponent implements OnInit {
  crudSrv: AdminServiceService = inject(AdminServiceService);
  donorSrv:AdminDonorsService=inject(AdminDonorsService);

  dialog = inject(MatDialog);
  placeholderText: string = 'search';
  inputValue: string = '';
  // עמודות לתצוגה בטבלה
  displayedColumns: string[] = ['id', 'name', 'price', 'kategory','donor','delete'];
  // מקור נתונים לטבלה
  dataSource = new MatTableDataSource<Present>();
  

  ngOnInit(): void {
    this.loadData(); // טוען נתונים מה-API בעת טעינת הקומפוננטה
  }

  loadData(): void {
    this.crudSrv.getAll().subscribe({
      next: (data) => {  
        this.dataSource.data = data; // מעדכן את מקור הנתונים בטבלה
        console.log(this.dataSource.data);
        
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

  openAddDialog(): void {
    const dialogRef = this.dialog.open(AddComponent, {
      width: '400px',
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        // טוען מחדש את הנתונים לאחר סגירת הדיאלוג
        this.loadData();
      }
    });
}
openUpdateDialog(present:Present) {
  console.log('Opening dialog with data:', present); // בדיקה
  const dialogRef = this.dialog.open(UpdateComponent, {
    width: '400px',
    data: { present },
  });

  dialogRef.afterClosed().subscribe((result) => {
    if (result) {
      // טוען מחדש את הנתונים לאחר סגירת הדיאלוג
      this.loadData();
    }
  });
}

onSearch(event: Event): void {
  const filterValue = (event.target as HTMLInputElement).value.trim(); // שליפת הערך מהאינפוט
  if (filterValue === '') {
    this.loadData(); // אם השדה ריק, טען את כל הנתונים
    return;
  }
  this.crudSrv.searchPresent(filterValue).subscribe({
    next: (data) => {
      this.dataSource.data = data; // עדכון מקור הנתונים
    },
    error: (err) => {
      if (err.status === 404) {
        this.dataSource.data = []; // במידה ולא נמצאו תוצאות, הטבלה תהיה ריקה
      } else {
        console.error('Error searching presents:', err); // טיפול בשגיאות אחרות
        this.dataSource.data = [];
      }
    },
  });
}

onFocus() {
  this.placeholderText = ''; // מסיר את ה-placeholder כשיש פוקוס
}

onBlur() {
  if (!this.inputValue) { // אם אין טקסט, מחזיר את ה-placeholder
    this.placeholderText = 'search';
  }}

}