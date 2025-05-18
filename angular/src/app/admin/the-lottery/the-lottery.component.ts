import { Component, inject } from '@angular/core';
import { FileService } from '../../../services/file-service.service';

@Component({
  selector: 'app-the-lottery',
  imports: [],
  templateUrl: './the-lottery.component.html',
  styleUrl: './the-lottery.component.css'
})
export class TheLotteryComponent {
  file: FileService = inject(FileService)

  to() {
    this.file.saveRevenue().subscribe((data)=>{})
  }
}
