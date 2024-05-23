import { Component, OnInit } from '@angular/core';
import { AccountService } from './_services/account.service';
import { User } from './_model/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title : string = 'Dating App';
  Description = 'This is a simple dating application';

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {

  this.setCurrentUser();

  }

  setCurrentUser(){
    const userString = localStorage.getItem('user');
    if(userString){
      const user:User = JSON.parse(userString);    
    this.accountService.setCurrentUser(user);
  }
}


}
