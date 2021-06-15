import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { share } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import {
  actionRoutes,
  controllerRoutes,
  authTokenRequestNames,
  authTokenRequestValues,
} from '../constants/url.constants';
import { SignInModel } from '../models/sigin.model';
import { SignUpModel } from '../models/signup.model';
import { AuthService } from './auth.service';
import { BaseDataService } from './base.data.service';
import { UserService } from './user.service';

@Injectable({ providedIn: 'root' })
export class AuthDataService extends BaseDataService {
  constructor(
    httpClient: HttpClient,
    private readonly authService: AuthService,
    private readonly userService: UserService
  ) {
    super(httpClient, controllerRoutes.auth);
  }

  public signUp(signUpModel: SignUpModel): Observable<any> {
    const subSignUp = this.sendPostRequest(JSON.stringify(signUpModel), actionRoutes.authSignup).pipe(share());
    subSignUp.subscribe(answer => {
      const signInModel: SignInModel = {email:  answer.email, password: answer.password};
      console.log(signInModel);
      this.sigIn(signInModel);
    });
    console.log(subSignUp);
    return subSignUp;
  }

  public sigIn(signInModel: SignInModel): Observable<any> {
    const param = new URLSearchParams();
    param.set(authTokenRequestNames.grantType, authTokenRequestValues.password);
    param.set(authTokenRequestNames.clientId,  environment.clientId);
    param.set(authTokenRequestNames.scope,     authTokenRequestValues.scope);
    param.set(authTokenRequestNames.username,  signInModel.email);
    param.set(authTokenRequestNames.password,  signInModel.password);
    const sub = this.sendPostRequest(
      `${param.toString()}`,
      actionRoutes.authToken,
      new HttpHeaders().set(
        'Content-Type',
        'application/x-www-form-urlencoded'
      ),
      false
    ).pipe(share());
    console.log(signInModel);
    sub.subscribe((answer) => {
      console.log(signInModel);
      this.authService.saveAccessToken(answer);
      this.userService.getUserInfoAndSave();
    });
    return sub;
  }

  public checkUniqueUserName(userName: string) {
    return this.sendGetRequest(
      { userName },
      actionRoutes.authCheckUniqueUserName
    );
  }

  public checkUniqueEmail(email: string) {
    return this.sendGetRequest({ email }, actionRoutes.authCheckUniqueEmail);
  }
}
