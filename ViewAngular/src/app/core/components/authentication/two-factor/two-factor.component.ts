import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { myUser } from '../dto/myUser';
import { AuthenticationService } from '../services/authentication.service';
import { ValidatorMessages } from '../../../shared/helpers/validators/validators-messages'
import { ValidatorsCustom } from 'src/app/core/shared/helpers/validators/validators-custom';
import { ToastrService } from 'ngx-toastr';
import { ForgotPassword } from '../dto/forgot-password';
import { T2Factor } from '../dto/t2-factor';


@Component({
  selector: 'two-factor',
  templateUrl: './two-factor.component.html',
  styleUrls: ['./two-factor.component.scss']
})
export class TwoFactorComponent implements OnInit {

  formMain: FormGroup;

  constructor(
    private Auth: AuthenticationService,

  ) { }



  private _validatorMessages = ValidatorMessages;

  get validatorMessages() {
    return this._validatorMessages
  }

  private _validatorCustom = ValidatorsCustom;

  get validatorCustom() {
    return this._validatorCustom
  }


  confirmTwoFactor() {

    if (this.formMain.value) {

      const t2factor: T2Factor = this.formMain.value;
      t2factor.userName = sessionStorage.getItem("userName");
      this.Auth.twoFactor(t2factor);
    }

  }

  formLoad() {

    return this.formMain = new FormGroup({
      token: new FormControl('', [Validators.required]),
    });
  }

  ngOnInit(): void {

    this.formLoad();
  }

}
