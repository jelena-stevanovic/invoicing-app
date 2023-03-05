import {Injectable} from '@angular/core';
import {LoginResponse} from "./models/login-response";

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  saveSession(tokenResponse: LoginResponse) {
    window.localStorage.setItem('AT', tokenResponse.token);
    if (tokenResponse.userId) {
      window.localStorage.setItem('ID', tokenResponse.userId.toString());
      window.localStorage.setItem('FN', tokenResponse.firstName);
    }
  }

  getSession(): LoginResponse | null {
    if (window.localStorage.getItem('AT')) {
      return {
        token: window.localStorage.getItem('AT') || '',
        firstName: window.localStorage.getItem('FN') || '',
        userId: +(window.localStorage.getItem('ID') || 0),
      };
    }
    return null;
  }

  logout() {
    window.localStorage.clear();
  }

  isLoggedIn(): boolean {
    let session = this.getSession();
    if (!session) {
      return false;
    }

    // check if token is expired
    const jwtToken = this.parseJwt(session.token);
    const tokenExpired = Date.now() > (jwtToken.exp * 1000);

    return !tokenExpired;
  }

  private parseJwt(token: string) {
    const base64Url = token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');

    const jsonPayload = decodeURIComponent(window
      .atob(base64)
      .split('')
      .map((c) => {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
      })
      .join(''));

    return JSON.parse(jsonPayload);
  };
}
