import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { Member } from '../_model/member';

@Injectable({
  providedIn: 'root'
})
export class MembersService implements OnInit {

  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  ngOnInit(): void {

  }

  getMembers() {

    return this.http.get<Member[]>(this.baseUrl + 'members');//as of now, jwt.interceptor.ts is gonna do this
  }

  getMember(username: string) {
    return this.http.get<Member>(this.baseUrl + 'members/' + username);
  }

  updateMember(member: Member) {

    return this.http.put(this.baseUrl + 'users', member)
  }


  /*
  getMembers() {

    return this.http.get<Member[]>(this.baseUrl + 'members',this.getHttpOptions());
  }

  getMember(username: string) {
    return this.http.get<Member>(this.baseUrl + 'members/' + username,this.getHttpOptions());
  }


  getHttpOptions() {
    const userData = localStorage.getItem('user');
    if (!userData) return;

    const user = JSON.parse(userData);

    return {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + user.token
      })
    }
  }

  */


}