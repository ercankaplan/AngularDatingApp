import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { Member } from '../_model/member';
import { map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MembersService {

  baseUrl = environment.apiUrl;
  
  members: Member[] = [];
  constructor(private http: HttpClient) { }

 

  getMembers() {

    if(this.members.length > 0) 
      return of(this.members);

     return this.http.get<Member[]>(this.baseUrl + 'members').pipe(
       map(members => {this.members = members; 
        return members;})
     )//as of now, jwt.interceptor.ts is gonna do this


  }

  getMember(username: string) {
    const member = this.members.find(x => x.userName === username);
    console.log(this.members);
    if(member) return of(member);
    return this.http.get<Member>(this.baseUrl + 'members/' + username);
  }

  updateMember(member: Member) {
        return this.http.put(this.baseUrl + 'users', member).pipe(
          map(()=> {
            const index = this.members.indexOf(member);
            //this.members[index] = member; This line of code replaces the object at this.members[index] with member
            this.members[index] = {...this.members[index],...member};
            /*
            This line of code creates a new object that combines the 
            properties of this.members[index] and member. 
            If a property exists in both objects, the value from member will be used. 
            This is known as object spread syntax. 
            It's a way of creating a new object that includes properties from other objects.
            */
          })

        )
  }

  setMainPhoto(photoId: number) {
    return this.http.put(this.baseUrl + 'users/set-main-photo/' + photoId, {});
  }

  deletePhoto(photoId: number) {
    return this.http.delete(this.baseUrl + 'users/delete-photo/' + photoId);
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

