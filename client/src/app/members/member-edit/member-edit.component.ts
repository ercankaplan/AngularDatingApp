import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { AccountService } from 'src/app/_services/account.service';
import { Member } from 'src/app/_model/member';
import { MembersService } from 'src/app/_services/members.service';
import { User } from 'src/app/_model/user';
import { take } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {

  @ViewChild('editForm') editForm: NgForm | undefined;
  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    if (this.editForm?.dirty) {
      $event.returnValue = true;
    }
  }
  member: Member | undefined;
  user: User | null = null;

  constructor(private accountService: AccountService,
    private memberService: MembersService,
    private toasterService: ToastrService
  ) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => this.user = user
    });
  }

  ngOnInit(): void {

    this.loadMember();


  }

  loadMember() {

    if (this.user) {
      this.memberService.getMember(this.user.username).subscribe({
        next: (member) => {
          this.member = member;
        }
      });
    }

  }

  updateMember() {
    console.log(this.member);

 
    this.memberService.updateMember(this.editForm?.value).subscribe({
      next: () => {
        this.toasterService.success('Profile updated successfully');
        this.editForm?.reset(this.member);
      },
      error: (error) => this.toasterService.error(error.error)
    });

  }



}

