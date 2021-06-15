import { HttpErrorResponse } from '@angular/common/http';
import { FormGroup } from '@angular/forms';
import { take } from 'rxjs/internal/operators/take';
import { AuthDataService } from '../services/auth.data.service';

export function IsUniqueEmail(
  authDataService: AuthDataService,
  handleErrors: (httpErrorResponse: HttpErrorResponse) => void
): (formGroup: FormGroup) => void {
  return (formGroup: FormGroup): void => {
    const formGroupEmail = formGroup.controls.email;
    const email = formGroupEmail.value;
    if (!formGroupEmail.pristine && !formGroupEmail.invalid) {
      authDataService.checkUniqueEmail(email).subscribe(
        (answer) => {},
        (httpErrorResponse) => {
          handleErrors(httpErrorResponse);
        }
      );
    }
  };
}
