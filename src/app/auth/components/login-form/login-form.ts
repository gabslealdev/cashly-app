import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-login-form',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login-form.html',
  styleUrls: ['./login-form.scss']
})
export class LoginForm {
  public loginForm: FormGroup
  constructor(private _formBuilder: FormBuilder) {
    this.loginForm = this._formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]]
    });
  }

  get email() {
    return this.loginForm.get('email')!;
  }

  get password(){
    return this.loginForm.get('password')!;
  }

  onSubmit(){
    console.log(this.loginForm.valid);
    if(this.loginForm.valid){
      console.log(this.loginForm.value)
    }
  }
}

