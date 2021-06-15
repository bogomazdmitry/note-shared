import { HttpErrorResponse } from '@angular/common/http';
import { FormGroup } from '@angular/forms';
import { take } from 'rxjs/internal/operators/take';
import { AuthDataService } from '../services/auth.data.service';

export function IsUniqueUserName(
  authDataService: AuthDataService,
  handleErrors: (httpErrorResponse: HttpErrorResponse) => void
) {
  return function (formGroup: FormGroup): void {
    const formGroupUserName = formGroup.controls['userName'];
    const userName = formGroupUserName.value;
    if (!formGroupUserName.pristine && !formGroupUserName.invalid) {
      authDataService.checkUniqueUserName(userName).subscribe(
        (answer) => {},
        (httpErrorResponse) => {
          handleErrors(httpErrorResponse);
        }
      );
    }
  };
}
