import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from '../services/authentication.service';
import { ValidatorMessages } from '../../../shared/helpers/validators/validators-messages'
//import { ValidatorsCustom } from 'src/app/core/shared/helpers/validators/validators-custom';
import { RetryConfirmPassword } from '../dto/retry-confirm-password';



@Component({
  selector: 'retry-confirm-email',
  templateUrl: './retry-confirm-email.component.html',
  styleUrls: ['./retry-confirm-email.component.scss']
})
export class RetryConfirmEmailComponent implements OnInit {

  formMain: FormGroup;

  constructor(
    private Auth: AuthenticationService,

  ) { }



  private _validatorMessages = ValidatorMessages;

  get validatorMessages() {
    return this._validatorMessages
  }

  // private _validatorCustom = ValidatorsCustom;

  // get validatorCustom() {
  //   return this._validatorCustom
  // }


  retryConfirmEmailGenerateNewToken() {
    if (this.formMain.value) {
      const retryConfirmPassword: RetryConfirmPassword = this.formMain.value;
      this.Auth.retryConfirmEmailGenerateNewToken(retryConfirmPassword);
    }
  }

  formLoad() {

    return this.formMain = new FormGroup({
      email: new FormControl('', [Validators.required]),
    });
  }

  ngOnInit(): void {
    this.formLoad();
  }

}
