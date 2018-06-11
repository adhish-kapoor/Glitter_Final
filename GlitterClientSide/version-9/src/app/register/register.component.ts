import { Component, OnInit } from '@angular/core';
import { UsersService } from '../services/users.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent implements OnInit {
  email: string;
  password: string;
  confirmPassword: string;
  contactNumber: string ;
  country: string;
  profileImage:File;
  name: string;
  
  constructor(private api: UsersService, private router: Router) { }

  doRegister() {
    let userData = {
      "email": this.email,
      "password": this.password,
      "confirmpassword": this.confirmPassword
    };

    let userDetails = {
      "Email": this.email,
      "Name": this.name,
      "Country": this.country,
      "ProfileImage": this.profileImage,
      "ContactNumber": this.contactNumber
    };


    this.api.register(userData)
      .subscribe((response) => {
        console.log("loginregister", response);
      },
        err => {
          console.log("Error");
        });

    this.api.saveUsersInformation(userDetails)
      .subscribe((response) => {
        console.log("users info", response);
      },
        err => {
          console.log("Error");
        });
  }

  public uploadFile($event) {
    console.log("upload file");
     this.profileImage = $event.target.files[0];     
  }


  redirectToLogin() {
    console.log("clicked")
    this.router.navigate(['login']);
  }
  ngOnInit() {
  }
}