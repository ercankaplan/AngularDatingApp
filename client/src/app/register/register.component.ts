import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  @Input() userListFromHomeComponent: any;
  @Output() cancelRegister = new EventEmitter();

  model: any = {};

  constructor(private accountService: AccountService,
    private toaster: ToastrService
  ) { }

  ngOnInit(): void {

  }

  register() {
    this.accountService.register(this.model).subscribe({
      next: (response) => {
        console.log(response);
        this.cancel();
      },
      error: (error) => {
        this.toaster.error(error.error)
        console.log(error);
      },
      complete: () => {
        console.log('has been registered successfully');
      }
    })
    console.log(this.model);
  }

  cancel() {
    console.log('cancelled');
    this.cancelRegister.emit(false)
  }

}
