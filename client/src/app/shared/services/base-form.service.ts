import { HttpErrorResponse } from '@angular/common/http';
import { FormGroup, ValidationErrors } from '@angular/forms';
import { somethingWentWrong } from 'src/app/shared/constants/errors.constants';

export abstract class BaseFormService {
  protected validationErrors: {
    [key: string]: { [key: string]: ValidationErrors };
  };

  public formGroup: FormGroup;

  public globalError: string;

  constructor(private globalErrors?: { [key: string]: string }) {}

  public setError(field: string, error: string): boolean {
    let result: boolean =
      this.formGroup.controls[field] != undefined ||
      this.validationErrors[field][error] != undefined;
    if (result) {
      this.formGroup.controls[field].setErrors(
        this.validationErrors[field][error]
      );
    }
    return result;
  }

  public getError(field: string, error: string): ValidationErrors {
    return this.validationErrors[field][error];
  }

  public hasError(field: string, error: string): boolean {
    return this.formGroup.controls[field].hasError(error);
  }

  protected abstract createForm(): Promise<FormGroup> | FormGroup;

  protected abstract createValidationErrors(): {
    [key: string]: { [key: string]: ValidationErrors };
  };

  public getFormField(nameField: string) {
    return this.formGroup.controls[nameField];
  }

  public handleErrors(httpErrorResponse: HttpErrorResponse): void {
    try {
      let errorMessage: string[] = httpErrorResponse.error?.split(' ');
      if (!this.setError(errorMessage[0], errorMessage[1])) {
        this.handleGlobalErrors(httpErrorResponse);
      }
    } catch {
      this.showError(somethingWentWrong);
    }
  }

  public handleGlobalErrors(httpErrorResponse: HttpErrorResponse) {
    if (
      !this.globalErrors ||
      this.globalErrors[httpErrorResponse.error.error_description] == undefined
    ) {
      this.showError(somethingWentWrong);
    } else {
      this.showError(
        this.globalErrors[httpErrorResponse.error.error_description]
      );
    }
  }

  public showError(error: string): void {
    this.globalError = error;
  }
}
