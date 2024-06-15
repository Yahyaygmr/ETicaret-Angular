import { TestBed } from '@angular/core/testing';

import { CustomTostrService } from './custom-tostr.service';

describe('CustomTostrService', () => {
  let service: CustomTostrService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CustomTostrService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
