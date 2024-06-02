import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { Member } from 'src/app/_model/member';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css'],
  //encapsulation: ViewEncapsulation.None,
})
export class MemberCardComponent implements OnInit {

  @Input() member : Member | undefined;
  //@Input() member : Member = {} as Member;

  constructor() { }

  ngOnInit(): void {

  }

  getMember(){
  
  
  }

}
