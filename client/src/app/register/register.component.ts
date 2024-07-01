import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { AbstractControl, Form, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  //@Input() userListFromHomeComponent: any;
  @Output() cancelRegister = new EventEmitter();
  //model: any = {};
  registerForm: FormGroup = new FormGroup({});
  maxDate: Date = new Date();
  validationErrors: string[] | undefined;
 

  constructor(private accountService: AccountService,
    private toaster: ToastrService,
    private formBuilder: FormBuilder,
    private router:Router
  ) { }

  ngOnInit(): void {
    this.initializeForm();
    this.maxDate.setFullYear(this.maxDate.getFullYear() - 18);
  }

  initializeForm() {

    this.registerForm = this.formBuilder.group({
      gender: ['male'],
      knownAs: ['', Validators.required],
      dateofBirth: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(4),Validators.maxLength(8)]],
      confirmPassword: ['', [Validators.required, this.matchValues('password')]]
    });

    /*
    this.registerForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', [Validators.required, Validators.minLength(4),
      Validators.maxLength(8)]),
      confirmPassword: new FormControl('', [Validators.required, this.matchValues('password')])
    });
    */
    this.registerForm.controls['password'].valueChanges.subscribe({
      next: () => {
        this.registerForm.controls['confirmPassword'].updateValueAndValidity();
      }
    });
  }

  register() {

    console.log(this.registerForm?.value);

    const dob = this.getDateOnly(this.registerForm.controls['dateofBirth'].value);
    const values = {...this.registerForm.value, dateofBirth: dob};

    console.log(values);
    
    this.accountService.register(values).subscribe({
      next: (response) => {
        this.router.navigateByUrl('/members');
        //console.log(response);
        //this.cancel();
      },
      error: (error) => {
        this.validationErrors = error;
        //this.toaster.error(error.error)
        //console.log(error);
      },
      complete: () => {
        console.log('has been registered successfully');
      }
    })
   
    
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

  private getDateOnly(dob: string | undefined) {
    if(!dob) return;
    let theDob = new Date(dob);

    return new Date(theDob.setMinutes(theDob.getMinutes() - theDob.getTimezoneOffset())).toISOString().slice(0, 10);

  }

}
