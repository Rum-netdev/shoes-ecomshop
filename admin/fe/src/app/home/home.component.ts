import { Component, OnInit } from '@angular/core';
import { AuthService } from '../shared/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {

  constructor(
    private _authService: AuthService,
    private router: Router
  ) {

  }

  ngOnInit(): void {
    if(!this._authService.isLoggedIn()) {
      this.router.navigate(['/login'])
    }
  }
}
