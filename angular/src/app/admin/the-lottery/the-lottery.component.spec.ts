import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TheLotteryComponent } from './the-lottery.component';

describe('TheLotteryComponent', () => {
  let component: TheLotteryComponent;
  let fixture: ComponentFixture<TheLotteryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TheLotteryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TheLotteryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
