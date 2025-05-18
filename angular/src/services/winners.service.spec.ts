import { TestBed } from '@angular/core/testing';

import { WinnersService } from './winners.service';

describe('WinnersService', () => {
  let service: WinnersService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WinnersService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
