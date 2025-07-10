import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Login } from "./auth/pages/login/login";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Login],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected title = 'cashly-app';
}
