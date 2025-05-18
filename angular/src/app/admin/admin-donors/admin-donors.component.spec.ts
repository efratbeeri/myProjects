import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminDonorsComponent } from './admin-donors.component';

describe('AdminDonorsComponent', () => {
  let component: AdminDonorsComponent;
  let fixture: ComponentFixture<AdminDonorsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminDonorsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminDonorsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
