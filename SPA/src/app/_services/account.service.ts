import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { IUser } from '../_models/user';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUsersource = new BehaviorSubject<IUser | null>(null);
  currentUser$ = this.currentUsersource.asObservable();
  constructor(private http: HttpClient) { }

  login(model: IUser) {
    return this.http.post<IUser>(this.baseUrl + "account/login", model).pipe(
      map((response: IUser) => {
        const user = response;
        if (user) {
          localStorage.setItem("user", JSON.stringify(user));
          this.currentUsersource.next(user);
        }
      })
    );
  }

  register(model: any) {
    return this.http.post<IUser>(this.baseUrl+"account/register", model).pipe(
      map(user => {
        if (user) {
          localStorage.setItem("user", JSON.stringify(user))
          this.currentUsersource.next(user);
        }
      })
    );
  }


  setCurrentUser(user: IUser) {
    this.currentUsersource.next(user);

  }

  logout() {
    localStorage.removeItem("user");
    this.currentUsersource.next(null);
  }
}
