import { TestBed } from '@angular/core/testing';

import { AdminDonorsService } from './admin-donors.service';

describe('AdminDonorsService', () => {
  let service: AdminDonorsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AdminDonorsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
