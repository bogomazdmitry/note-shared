import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  EmailValidator,
  FormBuilder,
  FormGroup,
  ValidationErrors,
  Validators,
} from '@angular/forms';
import { signInErrors } from 'src/app/shared/constants/errors.constants';
import { BaseFormService } from '../../shared/services/base-form.service';

@Injectable({ providedIn: 'root' })
export class SignInFormService extends BaseFormService {

  constructor(private readonly formBuilder: FormBuilder) {
    super(signInErrors);
    this.validationErrors = this.createValidationErrors();
    this.formGroup = this.createForm();
  }

  protected createForm(): FormGroup {
    return this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
  }

  protected createValidationErrors(): any {
    return {
      email: {
        required: { required: true },
      },
      password: {
        required: { required: true },
      },
    };
  }
}
