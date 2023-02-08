import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { myUser } from '../dto/myUser';
import { AuthenticationService } from '../services/authentication.service';
import { ValidatorMessages } from '../../../shared/helpers/validators/validators-messages'
import { ValidatorsCustom } from 'src/app/core/shared/helpers/validators/validators-custom';
import { ToastrService } from 'ngx-toastr';
import { ForgotPassword } from '../dto/forgot-password';


@Component({
  selector: 'forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent implements OnInit {

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


  recovery() {

    console.log(this.formMain)

    if (this.formMain.value) {

      const forgotMyPassword: ForgotPassword = this.formMain.value;
      this.Auth.forgotMyPassword(forgotMyPassword);
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
