import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddDonorsComponent } from './add-donors.component';

describe('AddDonorsComponent', () => {
  let component: AddDonorsComponent;
  let fixture: ComponentFixture<AddDonorsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddDonorsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddDonorsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
