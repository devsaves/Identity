import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { AuthenticationService } from '../services/authentication.service';
import { ActivatedRoute } from '@angular/router';
import { ConfirmEmail } from '../dto/confirm-email';



@Component({
  selector: 'confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.scss']
})
export class ConfirmEmailComponent implements OnInit {

  formMain: FormGroup;
  result: any;
  constructor(
    private _auth: AuthenticationService,
    private _activatedRoute: ActivatedRoute
  ) { }


  ngOnInit(): void {
    this._activatedRoute.queryParams.subscribe(param => {

      let confirmEmail: ConfirmEmail = new ConfirmEmail();
      confirmEmail.token = param['token']
      confirmEmail.email = param['email']

      this._auth.confirmEmail(confirmEmail);
    })
  }
}
