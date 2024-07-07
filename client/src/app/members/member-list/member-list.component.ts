import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Member } from 'src/app/_model/member';
import { Pagination } from 'src/app/_model/pagination';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {

  members: Member[] = [];
  //members$: Observable<Member[]> | undefined;
  pagination: Pagination | undefined;
  pageNumber = 3;
  pageSize = 12;

  constructor(private memberService: MembersService) { }

  ngOnInit(): void {

    this.loadMembers();
    //this.members$ = this.memberService.getMembers();
    //this.getMembers();  

  }

  loadMembers() {

    this.memberService.getMembers(this.pageNumber, this.pageSize).subscribe(response => {

      if (response.result && response.pagination) {
        this.members = response.result;
        this.pagination = response.pagination;
      }
    });


  }

  pageChanged(event: any) {
    if (this.pageNumber !== event.page) {
      this.pageNumber = event.page;
      this.loadMembers();
    }
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
