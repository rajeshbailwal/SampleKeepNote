import { TestBed, inject } from '@angular/core/testing';

import { ToasterCustomService } from './toaster.service';

describe('ToasterCustomService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ToasterCustomService]
    });
  });

  it('should be created', inject([ToasterCustomService], (service: ToasterCustomService) => {
    expect(service).toBeTruthy();
  }));
});
