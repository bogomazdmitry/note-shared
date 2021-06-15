import { HttpErrorResponse } from '@angular/common/http';
import { FormGroup } from '@angular/forms';
import { take } from 'rxjs/internal/operators/take';
import { ChangeUserInfo } from '../models/change-user-info.model';
import { AuthDataService } from '../services/auth.data.service';

export function FormIsChanged(changeUserInfo: ChangeUserInfo | null): (formGroup: FormGroup) => void {
  return (formGroup: FormGroup): void => {
    const newUser: ChangeUserInfo = formGroup.value;
    if (
      newUser.confirmNewPassword === changeUserInfo?.confirmNewPassword &&
      newUser.newPassword === changeUserInfo?.newPassword &&
      newUser.email === changeUserInfo?.email &&
      newUser.userName === changeUserInfo?.userName
    ) {
      formGroup.controls.globalErrors.setErrors({ noChanges: true });
      console.log(formGroup);
    }
    else {
      formGroup.controls.globalError.setErrors(null);
    }
  };
}
