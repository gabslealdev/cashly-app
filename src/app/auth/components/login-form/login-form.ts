import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth-service';

@Component({
  selector: 'app-login-form',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login-form.html',
  styleUrls: ['./login-form.scss']
})
export class LoginForm {
  private formBuilder = inject(FormBuilder)
  private authService = inject(AuthService)
  public loginForm: FormGroup
  constructor() {
    this.loginForm = this.formBuilder.group({
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
    if(this.loginForm.invalid) return; 

    this.authService.login(this.loginForm.value).subscribe({
      next: (res) => {
        console.log('Login bem-sucedido!', res);
        alert('Parabéns você concluiu o login')
      },
      error: (err) => {
        console.error('Erro no login', err);
        alert('Credenciais inválidas');
      }
    })
  }
}

