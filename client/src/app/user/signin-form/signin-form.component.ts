import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/shared/services/auth.service';
import { UserService } from 'src/app/shared/services/user.service';
import { SignInModel } from '../../shared/models/sigin.model';
import { AuthDataService } from '../../shared/services/auth.data.service';
import { SignInFormService } from './signin-form.service';

@Component({
  selector: 'user-signin-form',
  templateUrl: './signin-form.component.html',
  styleUrls: ['./signin-form.component.scss'],
})
export class SignInFormComponent implements OnInit {
  @Input()
  public backUrl: string | undefined;
  public buttonDisable: boolean;

  constructor(
    private readonly signInService: AuthDataService,
    private readonly activatedRouter: ActivatedRoute,
    private readonly router: Router,
    public readonly signInFormValidator: SignInFormService,
  ) {}

  ngOnInit(): void {
    this.activatedRouter.queryParams.subscribe((queryParam: any) => {
      const backUrl = 'backUrl';
      this.backUrl = queryParam[backUrl];
    });
    this.buttonDisable = false;
  }

  onSubmit(): void {
    this.buttonDisable = true;
    this.signInFormValidator.globalError = '';
    const signinModel = this.signInFormValidator.formGroup
      .value;
    this.signInService.sigIn(signinModel).subscribe(
      (answer) => {
        this.router.navigate([this.backUrl ? this.backUrl : '/']);
      },
      (error) => {
        this.signInFormValidator.handleErrors(error);
        this.buttonDisable = false;
      }
    );
  }
}
