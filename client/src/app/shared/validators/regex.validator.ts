import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function PatternValidator(
  regexOrString: RegExp | string,
  error: ValidationErrors
): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    if (!control.value) {
      return null;
    }
    const regex: RegExp =
      typeof regexOrString === 'string'
        ? new RegExp(regexOrString)
        : regexOrString;

    const valid = regex.test(control.value);
    return valid ? null : error;
  };
}
