import { ComponentFixture, TestBed } from '@angular/core/testing';
import { UpdateComponent } from '../updates/update/update.component';
import { AdminPresentComponent } from './admin-present.component';
import { AddComponent } from '../../add/add.component';
import { AppComponent } from '../../app.component';

describe('AdminPresentComponent', () => {
  let component: AdminPresentComponent;
  let fixture: ComponentFixture<AdminPresentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminPresentComponent,AddComponent,UpdateComponent,AppComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminPresentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
