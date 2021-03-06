import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AttachOnePlotComponent } from './attach-one-plot.component';

describe('AttachOnePlotComponent', () => {
  let component: AttachOnePlotComponent;
  let fixture: ComponentFixture<AttachOnePlotComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AttachOnePlotComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AttachOnePlotComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
