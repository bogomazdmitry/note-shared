import { Injectable } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ValidationErrors,
  Validators,
} from '@angular/forms';
import { ChangeUserInfo } from 'src/app/shared/models/change-user-info.model';
import { AuthDataService } from 'src/app/shared/services/auth.data.service';
import { UserService } from 'src/app/shared/services/user.service';
import { IsUniqueSelfEmail } from 'src/app/shared/validators/email-unique-self.validator';
import { FormIsChanged } from 'src/app/shared/validators/form-chaged.validator';
import { IsUniqueSelfUserName } from 'src/app/shared/validators/user-name-self-validator';
import { BaseFormService as BaseFormService } from '../../shared/services/base-form.service';
import { MustMatch } from '../../shared/validators/must-match.validator';
import { PatternValidator } from '../../shared/validators/regex.validator';

@Injectable({ providedIn: 'root' })
export class UserFormService extends BaseFormService {
  public changeUserInfo: ChangeUserInfo | null;

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly userService: UserService,
    private readonly authDataService: AuthDataService
  ) {
    super();
    const user = this.userService.getUser();
    if (user) {
      this.changeUserInfo = {
        email: user.email,
        userName: user.userName,
        oldPassword: '',
        confirmNewPassword: '',
        newPassword: '',
      };
    }
    this.validationErrors = this.createValidationErrors();
    this.formGroup = this.createForm();
  }

  protected createForm(): FormGroup {
    return this.formBuilder.group(
      {
        userName: [this.changeUserInfo?.userName, Validators.required],
        email: [
          this.changeUserInfo?.email,
          [Validators.required, Validators.email],
        ],
        oldPassword: [
          '',
          [
            Validators.required,
            PatternValidator(
              /.*[A-Z].*/,
              this.getError('oldPassword', 'hasUpperCase')
            ),
            PatternValidator(
              /.*[a-z].*/,
              this.getError('oldPassword', 'hasLowerCase')
            ),
            PatternValidator(/\d/, this.getError('oldPassword', 'hasNumber')),
            PatternValidator(
              /^.{8,64}$/,
              this.getError('oldPassword', 'hasLength')
            ),
          ],
        ],
        newPassword: [
          '',
          [
            PatternValidator(
              /.*[A-Z].*/,
              this.getError('newPassword', 'hasUpperCase')
            ),
            PatternValidator(
              /.*[a-z].*/,
              this.getError('newPassword', 'hasLowerCase')
            ),
            PatternValidator(/\d/, this.getError('newPassword', 'hasNumber')),
            PatternValidator(
              /^.{8,64}$/,
              this.getError('newPassword', 'hasLength')
            ),
          ],
        ],
        confirmNewPassword: [''],
        globalError: [''],
      },
      {
        validator: [
          MustMatch('newPassword', 'confirmNewPassword'),
          IsUniqueSelfUserName(
            this.changeUserInfo,
            this.authDataService,
            this.handleErrors.bind(this)
          ),
          IsUniqueSelfEmail(
            this.changeUserInfo,
            this.authDataService,
            this.handleErrors.bind(this)
          ),
          FormIsChanged(this.changeUserInfo),
        ],
      }
    );
  }

  protected createValidationErrors(): {
    [key: string]: { [key: string]: ValidationErrors };
  } {
    return {
      userName: {
        required: { required: true },
        notUnique: { notUnique: true },
      },
      email: {
        email: { email: true },
        required: { required: true },
        notUnique: { notUnique: true },
      },
      oldPassword: {
        hasUpperCase: { hasUpperCase: true },
        hasLowerCase: { hasLowerCase: true },
        hasNumber: { hasNumber: true },
        hasLength: { hasLength: true },
        mastMatch: { mastMatch: true },
      },
      newPassword: {
        hasUpperCase: { hasUpperCase: true },
        hasLowerCase: { hasLowerCase: true },
        hasNumber: { hasNumber: true },
        hasLength: { hasLength: true },
      },
      confirmPassword: {
        required: { required: true },
        mastMatch: { mastMatch: true },
      },
    };
  }
}
