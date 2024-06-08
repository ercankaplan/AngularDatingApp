import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Member } from 'src/app/_model/member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {

  //members : Member[] = [];
  members$: Observable<Member[]> | undefined;

  constructor(private memberService: MembersService) { }

  ngOnInit(): void {

    this.members$ = this.memberService.getMembers();
    //this.getMembers();  

  }
  /*
    getMembers(){
      this.memberService.getMembers().subscribe({
        next: (members) => this.members = members,
        error: (error) => console.log(error)
      });
    }
    */

}
