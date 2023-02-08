import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { ConfirmEmail } from '../dto/confirm-email';
import { ForgotPassword } from '../dto/forgot-password';
import { myUser } from '../dto/myUser';
import { ResetPassword } from '../dto/reset-password';
import { RetryConfirmPassword } from '../dto/retry-confirm-password';
import { T2Factor } from '../dto/t2-factor';



@Injectable({
  providedIn: 'root'
})

export class AuthenticationService {

  registerUrl: string = 'http://localhost:5000/api/auth/register';
  loginUrl: string = 'http://localhost:5000/api/auth/login';
  forgotPasswordUrl: string = 'http://localhost:5000/api/auth/forgotpassword';
  resetPasswordUrl: string = 'http://localhost:5000/api/auth/reset';
  confirmEmailUrl: string = 'http://localhost:5000/api/auth/confirmEmailAddress';
  retryConfirmEmailUrl: string = 'http://localhost:5000/api/auth/RetryConfirmEmailGenerateNewToken';
  twoFactorUrl: string = 'http://localhost:5000/api/auth/twoFactor';

  constructor(
    private _http: HttpClient,
    private _toastr: ToastrService,
    private _router: Router
  ) { }

  register(user: myUser) {
    return this._http.post<myUser>(this.registerUrl, user).pipe(take(1))
      .subscribe({
        next: (user: myUser) => {
          console.log(user)
          this._toastr.success('Parabéns, usuário cadastrado.', 'Sucesso.');
          this._router.navigateByUrl('/login');
        }, error: (err: any) => {
          console.log(err)
          this._toastr.error('Verifique os dados e tente novamente. Obrigado!', 'Falha');
        }
      })
  }

  login(user: myUser) {
    return this._http.post<myUser>(this.loginUrl, user).pipe(take(1)).subscribe({
      next: (user: myUser) => {

        if (user.action == "TwoFactor") {

          this._router.navigateByUrl('two-factor')
          sessionStorage.setItem("userName", user.userName);
          sessionStorage.setItem("token", user.token);
          //console.log("TwoFactor")

        }

        this._toastr.success('Seja bem vindo!', 'Sucesso.');
      }, error: (err: any) => {
        this._toastr.error('Verifique os dados e tente novamente. Obrigado!', 'Falha');
      }
    })
  }

  forgotMyPassword(forgotPassword: ForgotPassword) {
    return this._http.post<ForgotPassword>(this.forgotPasswordUrl, forgotPassword).pipe(take(1)).subscribe({
      next: () => {
        this._toastr.success('Recuperação de senha.', 'Solicitação enviada...');
      }, error: (err: any) => {
        console.log(err)
        this._toastr.error('Usuário não encontrado.', 'Falha');
      }
    })



  }

  twoFactor(t2factor: T2Factor) {

    return this._http.post<T2Factor>(this.twoFactorUrl, t2factor).pipe(take(1)).subscribe({
      next: () => {
        this._toastr.success('Autenticação de dois fatores.', 'Sucesso!');
      }, error: (err: any) => {
        console.log(err)
        this._toastr.error('Token Inválido ou expirado.', 'Falha');
      }
    })



  }

  confirmEmail(confirmEmail: ConfirmEmail) {
    return this._http.post<ConfirmEmail>(this.confirmEmailUrl, confirmEmail).pipe(take(1)).subscribe({
      next: () => {
        this._toastr.success('Recuperação de senha.', 'Solicitação enviada...');
      }, error: (err: any) => {
        console.log(err)
        this._toastr.error('Usuário não encontrado.', 'Falha');
      }
    })

  }
  // confirmEmail(token:string, email:string) {
  //  // console.log(`${this.confirmEmailUrl}/?token=${token}/&email=${email}`);
  //   this._http.get(`${this.confirmEmailUrl}/?token=${token}&email=${email}`).pipe(take(1)).subscribe({
  //     next: (result: boolean) => {
  //       if(result){
  //         this._toastr.success('Email confirmado.', 'Com Sucesso!!!');
  //       }
  //     }, error: (err: any) => {
  //       console.log(err)
  //       this._toastr.error('Tente novamente mais tarde, obrigado!', 'Falha');
  //     }
  //   })
  // }

  retryConfirmEmailGenerateNewToken(retryConfirmPassword: RetryConfirmPassword) {
    return this._http.post<RetryConfirmPassword>(this.retryConfirmEmailUrl, retryConfirmPassword).pipe(take(1)).subscribe({
      next: () => {
        this._toastr.success('Confirmação de email...', 'Solicitação enviada...');
      }, error: (err: any) => {
        console.log(err)
        this._toastr.error('Usuário não encontrado.', 'Falha');
      }
    })
  }


  reset(resetPassword: ResetPassword) {
    return this._http.post<ResetPassword>(this.resetPasswordUrl, resetPassword).pipe(take(1)).subscribe({
      next: () => {
        this._toastr.success('Recuperação de senha.', 'Com Sucesso!!!');
      }, error: (err: any) => {
        console.log(err)
        this._toastr.error('Tente novamente mais tarde, obrigado!', 'Falha');
      }
    })
  }



}
