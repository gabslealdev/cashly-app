import { Component } from '@angular/core';
import { LoginForm } from "../../../auth/components/login-form/login-form";

@Component({
  selector: 'app-auth-wrapper',
  imports: [LoginForm],
  templateUrl: './auth-wrapper.html',
  styleUrl: './auth-wrapper.scss'
})
export class AuthWrapper {

}
