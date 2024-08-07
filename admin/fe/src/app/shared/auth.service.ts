import { Injectable } from "@angular/core";
import { JwtService } from "./jwt.service";
import { Api } from "./services/api";
import { catchError, Observable, tap, throwError } from "rxjs";
import { HttpErrorResponse } from "@angular/common/http";
import { LoginCommand, LoginCommandResult } from "./services/models/authenticate";
import { Urls } from "./services/urls";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(
    private api: Api,
    private _jwtService: JwtService) {
  }

  // apiBaseUrl = environment.apiUrl
  tokenSectionName = '.jwtToken';

  login(user: LoginCommand): Observable<LoginCommandResult> {
    return this.api.postWithParams<LoginCommandResult>(Urls.authenticate.login, null, user)
      .pipe(
        tap(response => {
          if(response.tokenAuth)
            this.setAuth(response.tokenAuth);
        }),
        catchError(this.handleError)
      );
  }

  logout = () => {
    this._jwtService.destroyToken();
  }

  isLoggedIn = (): boolean => {
    return this._jwtService.getToken() != null;
  }

  private setAuth(token: string) {
    this._jwtService.saveToken(token);
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'An unknown error occurred!';
    if (error.error instanceof ErrorEvent) {
      // Client-side or network error
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // Server-side error
      if (error.status === 400) {
        errorMessage = error.error.message || 'Invalid credentials';
      }
    }
    return throwError(() => new Error(errorMessage));
  }
}