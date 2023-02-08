import { NgModule } from '@angular/core';

import { LoginComponent } from '../../login/login.component';
import { RegisterComponent } from '../../register/register.component';
import { RouterModule, Routes } from '@angular/router';
import { ForgotPasswordComponent } from '../../forgot-password/forgot-password.component';
import { ResetPasswordComponent } from '../../reset-password/reset-password.component';
import { ConfirmEmailComponent } from '../../confirm-email/confirm-email.component';
import { RetryConfirmEmailComponent } from '../../retry-confirm-email/retry-confirm-email.component';
import { TwoFactorComponent } from '../../two-factor/two-factor.component';
import { ProfileComponent } from '../../../profile/profile.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'forgot-password', component: ForgotPasswordComponent },
  { path: 'reset-password', component: ResetPasswordComponent },
  { path: 'confirm-email', component: ConfirmEmailComponent },
  { path: 'retry-confirm-email', component: RetryConfirmEmailComponent },
  { path: 'two-factor', component: TwoFactorComponent },
  { path: 'profile', component: ProfileComponent },
];
//http://localhost:4200/reset-password/api/Auth?token=CfDJ8F4bSEFGPlJBl77W2lFDb6IubJ2oZbQOnkMIWWLESrFxGh4SD9MFBlwHCPnAnpRwYuP17%2Fqj0mgzwXFAUEm4cWpOh2KQZF7A6oWxDRVX%2Fr3XITo1fp8S8zTkH5fsd7K3F8p7iR0qMleKM5cvyXw3%2FpaxmZ%2BQu98w6w1LrSbkosUb3tDxJQzCLDBQAf2u21551Q%3D%3D&email=marcus@nostopti.com.br
@NgModule({
  imports: [
    RouterModule.forChild(routes),
  ],
  exports: [
    RouterModule
  ]
})
export class AuthRoutingModule { }
