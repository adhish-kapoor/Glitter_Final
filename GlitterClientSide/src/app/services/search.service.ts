import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { IfObservable } from 'rxjs/observable/IfObservable';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';

let PEOPLE_SEARCH_URL = "http://localhost:52905/api/users?search=";
let POST_SEARCH_URL = "http://localhost:52905/api/tweets/gettweets?search=";

@Injectable()
export class SearchService {
  peopleSearchedResult: object;
  postSearchedResult: object;

  constructor(private http: HttpClient, private router: Router) { }

  searchPeople(searchText) {
    // search people
    return this.http.get<any>(PEOPLE_SEARCH_URL + searchText);          
  }

  searchPost(searchText) {
       //  search post 
    return this.http.get<any>(POST_SEARCH_URL + searchText)
      
  }

  // getSearchedPeople() {
  //   return this.peopleSearchedResult;
  // }

  // getSearchedPost() {
  //   return this.postSearchedResult;
  // }

  addFollowing(email, currentUserEmail) {
    let data = JSON.stringify({
      "FollowingId": email,
      "Email": currentUserEmail
    });
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post("http://localhost:52905/api/follows/follow", data, httpOptions)
  }

  private handleError(err: HttpErrorResponse) {
    console.error(err.message);
    return Observable.throw(err.message);
  }
}
