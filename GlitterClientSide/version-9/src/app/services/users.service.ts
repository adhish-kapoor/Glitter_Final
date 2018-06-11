import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { serializeObj } from '../utils/serialize';

const LOGIN_URL = "http://localhost:52905/token";
const USER_DETAILS_REGISTER_URL = "http://localhost:52905/api/users/register";
const USER_ACCOUNT_REGISTER_URL = "http://localhost:52905/api/account/register";

@Injectable()
export class UsersService {
  constructor(private http: HttpClient) { }

  login(data): Observable<any> {      
    let body = serializeObj(data);
    return this.http.post(LOGIN_URL, body);
  }

  // save data to users table
  register(userData): Observable<any> {
    const  httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'multipart/form-data'
      })
    }
    let body = userData;
    return this.http.post(USER_ACCOUNT_REGISTER_URL, body, httpOptions)
    .do(response => {
      console.log("response", response)
    })
    .catch(this.handleError)
  }

  // save data to users table
  saveUsersInformation(userDetails): Observable<any> {
    const  httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }
    console.log("in service", userDetails.ProfileImage);
    let body = userDetails;
    console.log("in service", body.ProfileImage);
    return this.http.post(USER_DETAILS_REGISTER_URL, body);
  }
  
  private handleError(err: HttpErrorResponse) {
    console.error(err.message);
    return Observable.throw(err.message);
  }
}