import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Observable, of } from 'rxjs';
import { IUser } from '../_models/user';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit{
  model: any = {};
  
  constructor(public accountService:AccountService) { }
  ngOnInit(): void {
    
  }
  login(): void {
    this.accountService.login(this.model).subscribe({
      next: response => {
        console.log(response);
      },
      error:error => console.log(error)
    })
  }
  //getCurrentUser() {
    //this.accountService.currentUser$.subscribe({
     // next: user => this.loggedin = !!user,
      //error: error => console.log(error)
    //});
  //}


  logout(): void {
    this.accountService.logout();
  }
}
