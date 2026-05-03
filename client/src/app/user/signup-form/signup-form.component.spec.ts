import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { ReactiveFormsModule, FormGroup, FormControl } from '@angular/forms';
import { SignUpFormComponent } from './signup-form.component';
import { AuthService } from 'src/app/shared/services/auth.service';
import { SignUpFormService } from './signup-form.service';

describe('RegistrFormComponent', () => {
  let component: SignUpFormComponent;
  let fixture: ComponentFixture<SignUpFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SignUpFormComponent],
      imports: [ ReactiveFormsModule ],
      providers: [
        { provide: AuthService, useValue: { signUp: () => {} } },
        { provide: SignUpFormService, useValue: {
          formGroup: new FormGroup({
            userName: new FormControl(''),
            email: new FormControl(''),
            password: new FormControl(''),
            confirmPassword: new FormControl(''),
          }),
          globalError: '',
          getFormField: () => new FormControl(''),
          handleErrors: () => {},
        } },
      ],
      schemas: [ NO_ERRORS_SCHEMA ],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SignUpFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
