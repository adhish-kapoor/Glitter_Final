import { Component, OnInit } from '@angular/core';
import { UsersService } from '../services/users.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  username: string = "Ram@gmail.com";
  password: string = "Ram@123";

  constructor(private api: UsersService, private router: Router) { }

  doLogin() {
    let data = {
      "username": this.username,
      "password": this.password,
      "grant_type": "password"
    }

    this.api.login(data)
      .subscribe((response) => {
        console.log("getting access token", response);
        localStorage.setItem("access_token", response.access_token);
        localStorage.setItem("uid", response.userName);        
        alert("Hi " + response.userName);
        this.router.navigate(['/dashboard']);
      }, err=>{
        if(err.status == 0){
          alert("Server Down..")
        }
        else{
          alert("Invalid Username or Password")
        }        
      })      
  }

  redirectToRegiter() {
    console.log("redirect to register clicked");
    this.router.navigate(['register']);
  }
  
  ngOnInit() {
  }
}