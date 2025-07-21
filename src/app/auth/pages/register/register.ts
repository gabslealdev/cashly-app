import { Component } from '@angular/core';
import { AuthWrapper } from "../../../layout/components/auth-wrapper/auth-wrapper";
import { RegisterForm } from "../../components/register-form/register-form";

@Component({
  selector: 'app-register',
  imports: [AuthWrapper, RegisterForm],
  templateUrl: './register.html',
  styleUrl: './register.scss'
})
export class Register {

}
