import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ChangeUserInfo } from 'src/app/shared/models/change-user-info.model';
import { User } from 'src/app/shared/models/user.model';
import { UserDataService } from 'src/app/shared/services/user.data.service';
import { UserService } from 'src/app/shared/services/user.service';
import { UserFormService } from 'src/app/user/user-info/user-form.service';

@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.scss'],
})
export class UserInfoComponent implements OnInit {
  public buttonDisable: boolean;
  public globalSuccess: string;

  constructor(
    private readonly userService: UserService,
    private readonly userDataService: UserDataService,
    public userFormValidation: UserFormService
  ) {}

  ngOnInit(): void {
    this.buttonDisable = false;
  }

  onSubmit(): void {
    const newUser: ChangeUserInfo = this.userFormValidation.formGroup.value;
    this.userDataService.changeUserInfo(newUser).subscribe(
      (answer) => {
        this.userFormValidation.changeUserInfo = {
          email: answer.email,
          userName: answer.userName,
          oldPassword: '',
          confirmNewPassword: '',
          newPassword: '',
        };
        this.userService.saveUser(answer);
        this.globalSuccess = 'Changed!';
        this.buttonDisable = false;
      },
      (httpErrorResponse) => {
        this.globalSuccess = '';
        this.userFormValidation.handleErrors(httpErrorResponse);
        this.buttonDisable = false;
      }
    );
  }
}
