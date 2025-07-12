import { Component } from '@angular/core';
import { LoginForm } from "../../components/login-form/login-form";
import { AuthWrapper } from "../../../layout/components/auth-wrapper/auth-wrapper";

@Component({
  selector: 'app-login',
  imports: [LoginForm, AuthWrapper],
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class Login {

}
