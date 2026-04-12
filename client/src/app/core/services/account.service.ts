import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { User } from '../../shared/models/user';
import { map } from 'rxjs';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class AccountService {

  baseUrl = environment.apiUrl;
  private http = inject(HttpClient);
  currentUser = signal<User | null>(null);

  constructor() {
    const savedUser = localStorage.getItem('user');
    if (savedUser) {
      this.currentUser.set(JSON.parse(savedUser));
    }
  }

  login(values: any) {
    const credentials = btoa(`${values.email}:${values.password}`);
    const headers = new HttpHeaders({
      Authorization: `Basic ${credentials}`,
    });

    return this.http.get<User>(this.baseUrl + 'account/login', { headers }).pipe(
      map((user) => {
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          localStorage.setItem('credentials', credentials);
          
          this.currentUser.set(user);
        }
        return user;
      })
    );
  }

  register(model: any) {
    return this.http.post(this.baseUrl + 'account/register', model);
  }

  logout() {
    localStorage.removeItem('user');
    localStorage.removeItem('credentials');
    this.currentUser.set(null);
  }
}
