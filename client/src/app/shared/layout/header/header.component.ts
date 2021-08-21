import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'shared-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {
  public userName: string | undefined;

  constructor(
    public readonly userService: UserService,
    public readonly authService: AuthService,
    public readonly router: Router,
    public readonly activatedRouter: ActivatedRoute,
  ) {
  }

  public ngOnInit(): void {
    if (this.authService.isAuthorize()) {
      this.userName = this.userService.getUser()?.userName;
    }
  }
}
