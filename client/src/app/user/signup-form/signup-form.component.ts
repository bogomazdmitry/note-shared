import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SignUpModel } from '../../shared/models/signup.model';
import { AuthDataService } from '../../shared/services/auth.data.service';
import { SignUpFormService } from './signup-form.service';

@Component({
  selector: 'user-signup-form',
  templateUrl: './signup-form.component.html',
  styleUrls: ['./signup-form.component.scss'],
})
export class SignUpFormComponent implements OnInit {
  public buttonDisable: boolean;

  constructor(
    private readonly signUpDataService: AuthDataService,
    public readonly signUpFormValidator: SignUpFormService,
    private readonly router: Router
  ) {}

  ngOnInit(): void {
    this.buttonDisable = false;
  }

  onSubmit(): void {
    this.buttonDisable = true;
    this.signUpFormValidator.globalError = '';
    let signUpModel: SignUpModel = this.signUpFormValidator.formGroup
      .value;
    this.signUpDataService.signUp(signUpModel).subscribe(
      (answer) => {
        this.router.navigate(['/']);
      },
      (httpErrorResponse) => {
        console.log(httpErrorResponse);
        console.log("httpErrorResponse");
        this.signUpFormValidator.handleErrors(httpErrorResponse);
        this.buttonDisable = false;
      }
    );
  }
}
