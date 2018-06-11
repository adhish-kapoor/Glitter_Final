import { Component, OnInit } from '@angular/core';
import { UsersService } from '../services/users.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  username: string = "danish@gmail.com";
  password: string = "Danish@123";

  constructor(private api: UsersService, private router: Router) { }

  doLogin() {
    let data = {
      "username": this.username,
      "password": this.password,
      "grant_type": "password"
    }
    if(!this.isValidated()){
      alert("Please, fill all fields!");
      return;      
    }
    else{
    this.api.login(data)
      .subscribe((response) => {
        console.log("getting access token", response);

        localStorage.setItem("access_token", response.access_token);
        localStorage.setItem("uid", response.userName);     
           
        alert("Hi " + response.userName);

        this.router.navigate(['/dashboard/playground']); //redirect to playground after login
      }, err=>{
        if(err.status == 0){
          alert("Server Down...")
        }
        else{
          alert("Invalid Username or Password")
        }        
      })      
  }
  }
  isValidated():boolean{
    if(this.username && this.password){
      return true;
    }
    else
    {
      return false;
    }
  }
  redirectToRegiter() {
    console.log("redirect to register clicked");
    this.router.navigate(['register']);
  }
  
  ngOnInit() {
  }
}