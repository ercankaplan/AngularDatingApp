import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { User } from '../_model/user';
import { Observable } from 'rxjs';
import { of } from 'rxjs';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit{

  model : any = { };
  //currentUser$ : Observable<User | null> = of(null);

  constructor(public accountService:AccountService) { }

  ngOnInit(): void {

    //this.currentUser$ = this.accountService.currentUser$;
  }


  login(){

    console.log(this.model);

    this.accountService.login(this.model).subscribe({
    next: response => console.log(response),
    error:error => console.log(error),
    complete:() => console.log('Request has completed')
    }
  )

  }

  logout(){
    this.accountService.logout();
  }

}
