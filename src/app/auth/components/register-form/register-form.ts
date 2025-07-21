import { Component, inject } from '@angular/core';
import { AuthButton } from "../auth-button/auth-button";
import { CommonModule } from '@angular/common';
import { FormGroup, ReactiveFormsModule, FormBuilder, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
import { AuthService } from '../../services/auth-service';

@Component({
  selector: 'app-register-form',
  imports: [CommonModule, AuthButton, ReactiveFormsModule],
  templateUrl: './register-form.html',
  styleUrl: './register-form.scss'
})
export class RegisterForm {
  private authService = inject(AuthService);
  private formBuilder = inject(FormBuilder);
  public registerForm: FormGroup;
  public errorMessage: string | null = null;

  constructor() {
    this.registerForm = this.formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(30)]],
      email: ['', [Validators.required, Validators.email, Validators.maxLength(80)]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      confirmPassword: ['', Validators.required]
    },
      {
        validators: passwordMatchValidator
      });
  }

  get name() {
    return this.registerForm.get('name')!;
  }

  get email() {
    return this.registerForm.get('email')!;
  }

  get password() {
    return this.registerForm.get('password')!;
  }

  get confirmPassword() {
    return this.registerForm.get('confirmPassword')!;
  }

  onSubmit() {
    if (this.registerForm.invalid) return;

    this.authService.register(this.registerForm.value).subscribe({
      next: (res) => {
        console.log('Usuário cadastrado', res);
        alert(`Seja bem-vindo, ${res.name}`);
      },
      error: (err: Error) => {
        err.message
        this.errorMessage = err.message
      }
    })
  }
}

export function passwordMatchValidator(form: AbstractControl): ValidationErrors | null {
  const password = form.get('password')?.value;
  const confirmPassword = form.get('confirmPassword')?.value;

  return password !== confirmPassword ? { mismatch: true } : null;
}
