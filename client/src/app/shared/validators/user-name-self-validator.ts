import { HttpErrorResponse } from '@angular/common/http';
import { FormGroup } from '@angular/forms';
import { take } from 'rxjs/internal/operators/take';
import { ChangeUserInfo } from '../models/change-user-info.model';
import { AuthDataService } from '../services/auth.data.service';

export function IsUniqueSelfUserName(
  changeUserInfo: ChangeUserInfo | null,
  authDataService: AuthDataService,
  handleErrors: (httpErrorResponse: HttpErrorResponse) => void
) {
  return function (formGroup: FormGroup): void {
    const formGroupUserName = formGroup.controls['userName'];
    const userName = formGroupUserName.value;
    if (!formGroupUserName.pristine && !formGroupUserName.invalid && userName != changeUserInfo?.userName) {
      authDataService.checkUniqueUserName(userName).subscribe(
        (answer) => {

        },
        (httpErrorResponse) => {
          handleErrors(httpErrorResponse);
        }
      );
    }
  };
}
