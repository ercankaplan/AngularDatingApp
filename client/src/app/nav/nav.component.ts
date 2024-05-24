import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { User } from '../_model/user';
import { Observable } from 'rxjs';
import { of } from 'rxjs';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit{

  model : any = { };
  currentUser? : User | null = null;

  constructor(public accountService:AccountService,
    private router:Router,
    private toaster:ToastrService) { }

  ngOnInit(): void {

    this.accountService.currentUser$.subscribe(s=> {this.currentUser = s});
  }


  login(){

    console.log(this.model);

    this.accountService.login(this.model).subscribe({
    next: _ => this.router.navigateByUrl('/members'),
    error:errorObj => {
      console.log(errorObj);
       this.toaster.error(errorObj.error)},
    complete:() => console.log('Request has completed')
    }
  )

  }

  logout(){
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }

}
