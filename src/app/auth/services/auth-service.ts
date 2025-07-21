import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { LoginRequest } from '../models/requests/login/login-request.model';
import { catchError, Observable, Observer, tap, throwError } from 'rxjs';
import { LoginResponse } from '../models/responses/login/login-response.model';
import { RegisterRequest } from '../models/requests/register/register-request.models';
import { RegisterResponse } from '../models/responses/register/register-response.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly httpClient = inject(HttpClient)
  private apiUrl = environment.apiUrl;

  login(credentials: LoginRequest): Observable<LoginResponse> {
    return this.httpClient.post<LoginResponse>(`${this.apiUrl}/login`, credentials).pipe(
      tap(response => {
        localStorage.setItem('auth_token', response.token);
      }),
      catchError(error => {
        const errorMessage = error?.error?.errorMessages?.[0] || 'Por favor verifique e-mail e/ou senha e tente novamente.'
        return throwError(() => new Error(errorMessage));
      })
    );
  }

  register(request: RegisterRequest): Observable<RegisterResponse> {
    return this.httpClient.post<RegisterResponse>(`${this.apiUrl}/Users`, request).pipe(
      tap(response => {
        localStorage.setItem('auth_token', response.token);
      }),
      catchError(error => {
        const errorMessage = error?.error?.errorMessages?.[0] || 'Não foi possível cadastrar este usuário verifique os dados e tente novamente'
        return throwError(() => new Error(errorMessage));
      })
    );
  }

}
