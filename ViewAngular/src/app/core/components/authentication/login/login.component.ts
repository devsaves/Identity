import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { myUser } from '../dto/myUser';
import { AuthenticationService } from '../services/authentication.service';
import { ValidatorMessages } from '../../../shared/helpers/validators/validators-messages'
import { ValidatorsCustom } from 'src/app/core/shared/helpers/validators/validators-custom';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, AfterViewInit {

  formMain: FormGroup;

  constructor(
    private Auth: AuthenticationService,

  ) { }


  ngAfterViewInit(): void {
    this.formLoad();
  }

  private _validatorMessages = ValidatorMessages;

  get validatorMessages() {
    return this._validatorMessages
  }

  private _validatorCustom = ValidatorsCustom;

  get validatorCustom() {
    return this._validatorCustom
  }


  login() {

    console.log(this.formMain)
    const user: myUser = this.formMain.value;

    if (this.formMain.value) {
      const user: myUser = this.formMain.value;
      this.Auth.login(user);
    }

  }

  formLoad() {

    const form = new FormGroup({
      //email: new FormControl('', [Validators.required]),
      userName: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required]),
    })
    // },this._validatorCustom.fieldCompare('password','confirmPassword'))

    this.formMain = form;

    return this.formMain;
  }

  ngOnInit(): void {
    this.formLoad();
  }

}
