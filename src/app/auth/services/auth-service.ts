import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { LoginRequest } from '../models/requests/login-request.model';
import { Observable, tap } from 'rxjs';
import { LoginResponse } from '../models/responses/login-response.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly httpClient = inject(HttpClient)
  private apiUrl = environment.apiUrl;
  
  login(credentials: LoginRequest): Observable<LoginResponse>{
    return this.httpClient.post<LoginResponse>(`${this.apiUrl}/login`, credentials).pipe(
      tap(response => {
        localStorage.setItem('auth_token', response.token);
      })
    );
  }

}
