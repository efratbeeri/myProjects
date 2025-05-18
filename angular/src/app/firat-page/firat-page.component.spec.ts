import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FiratPageComponent } from './firat-page.component';

describe('FiratPageComponent', () => {
  let component: FiratPageComponent;
  let fixture: ComponentFixture<FiratPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FiratPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FiratPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
