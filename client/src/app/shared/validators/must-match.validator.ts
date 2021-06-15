import { FormGroup } from '@angular/forms';

export function MustMatch(name: string, matchingName: string) {
  return function (formGroup: FormGroup): void {
    if (formGroup.controls[name].value != formGroup.controls[matchingName].value) {
      formGroup.controls[matchingName].setErrors({ mustMatch: true });
    } else {
      formGroup.controls[matchingName].setErrors(null);
    }
  };
}
