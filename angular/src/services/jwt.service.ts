import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class JwtService {
  decodeToken(token: string): any {
    try {
      const base64Url = token.split('.')[1];
      const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
      const jsonPayload = decodeURIComponent(
        atob(base64)
          .split('')
          .map((c) => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
          .join('')
      );
      return JSON.parse(jsonPayload);
    } catch (e) {
      console.error('Invalid token:', e);
      return null;
    }
  }

  getUsernameFromToken(token: string): string | null {
    const decoded = this.decodeToken(token);
    return decoded?.unique_name || null;
  }

  getUserIdFromToken(token: string): string | null {
    const decoded = this.decodeToken(token);
    return decoded?.nameid || null;
  }

  getCartIdFromToken(token: string): number {
    const decoded = this.decodeToken(token);
    return decoded?.CartId || null;
  }
  getRolesFromToken(token: string): string | null {
    const decoded = this.decodeToken(token);
    return decoded?.role || null;
  }
}
