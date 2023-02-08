import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ValidatorsCustom } from 'src/app/core/shared/helpers/validators/validators-custom';
import { ToastrService } from 'ngx-toastr';
import { ValidatorMessages } from '../../shared/helpers/validators/validators-messages';


@Component({
  selector: 'profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  formMain: FormGroup;

  constructor(
    // private Auth: AuthenticationService,

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

    // console.log(this.formMain)

    // if (this.formMain.value) {

    //   const forgotMyPassword: ForgotPassword = this.formMain.value;
    //   this.Auth.forgotMyPassword(forgotMyPassword);
    // }

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
