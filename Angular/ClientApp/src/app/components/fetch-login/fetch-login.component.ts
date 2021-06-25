import { Component } from '@angular/core';
import { LoginData, LoginService } from '../../services/login.service';
import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'app-fetch-login',
    templateUrl: './fetch-login.component.html',
    styleUrls: ['./fetch-login.component.css']
})
/** fetch-login component*/
export class FetchLoginComponent {
  logList: LoginData[]
    /** fetch-login ctor */
  constructor(public http: HttpClient, private _loginService: LoginService) {
    this.getLogins();
  }

  getLogins() {
    this._loginService.getLogins().subscribe(data => this.logList = data);
  }

  lockOrUnlock(loginID) {
    this._loginService.lockOrUnlock(loginID).subscribe((data) => {
      this.getLogins();
    }, error => console.error(error));
  }
}
