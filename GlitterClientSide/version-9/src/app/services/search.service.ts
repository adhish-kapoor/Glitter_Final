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

  constructor(private http: HttpClient, private router:Router) { }

  searchPeopleAndPost(searchText) {
    // search people
    this.http.get<any>(PEOPLE_SEARCH_URL + searchText)
      .subscribe(response => {
        this.peopleSearchedResult = response;
        console.log(this.peopleSearchedResult);
      })
    //  search post now
    this.http.get<any>(POST_SEARCH_URL + searchText)
      .subscribe(response => {
        this.postSearchedResult = response;
        console.log(this.postSearchedResult);
      })
      this.router.navigate(['/dashboard/search/people']);
}

  getSearchedPeople() {
    return this.peopleSearchedResult;
  }

  getSearchedPost() {
    return this.postSearchedResult;
  }

  private handleError(err: HttpErrorResponse) {
    console.error(err.message);
    return Observable.throw(err.message);
  }
}
