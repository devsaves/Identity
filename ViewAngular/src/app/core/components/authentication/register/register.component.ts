import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { myUser } from '../dto/myUser';
import { AuthenticationService } from '../services/authentication.service';
import { ValidatorMessages } from '../../../shared/helpers/validators/validators-messages'
import { ValidatorsCustom } from 'src/app/core/shared/helpers/validators/validators-custom';


@Component({
  selector: 'register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})

export class RegisterComponent implements OnInit, AfterViewInit {

  formMain: FormGroup;

  constructor(
    private _auth: AuthenticationService,
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


  register() {

    // console.log(this.formMain)
    // const user: myUser = this.formMain.value;

    if (this.formMain.value) {
      const user: myUser = this.formMain.value;
      this._auth.register(user);
    }

  }

  formLoad() {

    const form = new FormGroup({
      userName: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required]),
      confirmPassword: new FormControl('', [Validators.required]),
    })
    // },this._validatorCustom.fieldCompare('password','confirmPassword'))

    this.formMain = form;

    return this.formMain;
  }

  ngOnInit(): void {
    this.formLoad();
  }

}
