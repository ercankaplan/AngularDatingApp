import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { GalleryModule, GalleryItem, ImageItem } from 'ng-gallery';
import { Member } from 'src/app/_model/member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-detail',
  standalone: true,
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css'],
  imports: [CommonModule, TabsModule, GalleryModule]
})
export class MemberDetailComponent implements OnInit {

  member: Member | undefined;
  images: GalleryItem[] = [];

  constructor(private memberService: MembersService, private route: ActivatedRoute) {

    var userName = this.route.snapshot.paramMap.get('username');

    if (!userName)
      return;
    else
      this.getMember(userName);

  }

  ngOnInit(): void {

  }

  getMember(username: string) {
    this.memberService.getMember(username).subscribe({
      next: (member) => {
        this.member = member;
        this.getPhotos();
      }
    });
  }

  getPhotos() {

    if (!this.member) return;
    for (let photo of this.member?.photos) {
      this.images.push(new ImageItem({ src: photo?.url, thumb: photo?.url, alt: photo?.url }));
      this.images.push(new ImageItem({ src: photo?.url, thumb: photo?.url, alt: photo?.url }));
      this.images.push(new ImageItem({ src: photo?.url, thumb: photo?.url, alt: photo?.url }));
      this.images.push(new ImageItem({ src: photo?.url, thumb: photo?.url, alt: photo?.url }));
    }
  }

}