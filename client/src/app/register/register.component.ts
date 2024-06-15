import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { AbstractControl, Form, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  //@Input() userListFromHomeComponent: any;
  @Output() cancelRegister = new EventEmitter();
  registerForm: FormGroup = new FormGroup({});

  model: any = {};

  constructor(private accountService: AccountService,
    private toaster: ToastrService
  ) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {

    this.registerForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', [Validators.required, Validators.minLength(4),
      Validators.maxLength(8)]),
      confirmPassword: new FormControl('', [Validators.required, this.matchValues('password')])
    });

    this.registerForm.controls['password'].valueChanges.subscribe({
      next: () => {
        this.registerForm.controls['confirmPassword'].updateValueAndValidity();
      }
    });
  }

  register() {

    console.log(this.registerForm?.value);

    /*
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
    */
  }

  cancel() {
    console.log('cancelled');
    this.cancelRegister.emit(false)
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control.value === control.parent?.get(matchTo)?.value ? null : { notMatching: true }
    }
  }

}
