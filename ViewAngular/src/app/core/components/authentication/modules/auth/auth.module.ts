import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { LoginComponent } from '../../login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RegisterComponent } from '../../register/register.component';
import { ToastrModule } from 'ngx-toastr';
import { AuthRoutingModule } from './auth-routing.module';
import { ForgotPasswordComponent } from '../../forgot-password/forgot-password.component';
import { ResetPasswordComponent } from '../../reset-password/reset-password.component';
import { ConfirmEmailComponent } from '../../confirm-email/confirm-email.component';
import { RetryConfirmEmailComponent } from '../../retry-confirm-email/retry-confirm-email.component';
import { TwoFactorComponent } from '../../two-factor/two-factor.component';
import { ProfileComponent } from '../../../profile/profile.component';


@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    ForgotPasswordComponent,
    ResetPasswordComponent,
    ConfirmEmailComponent,
    RetryConfirmEmailComponent,
    TwoFactorComponent,
    ProfileComponent
  ],
  imports: [
    CommonModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    ReactiveFormsModule,
    FormsModule,
    //my imports
    AuthRoutingModule,
  ],
  exports:[
    LoginComponent,
    RegisterComponent,
    ForgotPasswordComponent,
    ResetPasswordComponent,
    ConfirmEmailComponent,
    RetryConfirmEmailComponent,
    TwoFactorComponent,
    ProfileComponent
  ]
})
export class AuthModule { }
