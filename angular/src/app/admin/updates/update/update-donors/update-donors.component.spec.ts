import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateDonorsComponent } from './update-donors.component';

describe('UpdateDonorsComponent', () => {
  let component: UpdateDonorsComponent;
  let fixture: ComponentFixture<UpdateDonorsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UpdateDonorsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateDonorsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
