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
  contactNumber: string;
  country: string;
  profileImage: File;
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
      "ProfileImage": "demo.jpeg",
      "ContactNumber": this.contactNumber
    };


    if (!this.isValidated()) {
      alert("Please, fill all fields!");
      return;
    }
    else {
      this.api.saveUsersInformation(userDetails)
        .subscribe((response) => {
          alert("Registered successfully");

          this.clear();
          this.router.navigate(['login']); //redirect to login after registration
        },
          err => {
            // alert("Validation Error! Fill all fields carefully.");
            // return;
          });

      this.api.register(userData)
        .subscribe((response) => {
          console.log("register", response);
        },
          err => {
            alert("Validation Error! You are already registered");
            return;
          });
    }
  }
  clear() {
    this.email  =  "";
    this.password  =  "";
    this.confirmPassword  =  "";
    this.contactNumber  =  "";
    this.country  =  "";
    this.profileImage  =  null;
    this.name  =  "";
  }

  isValidated(): boolean {
    if (this.name && this.email && this.contactNumber && this.country && this.password && this.confirmPassword) {
      return true;
    }
    else {
      return false;
    }
  }

  redirectToLogin() {
    console.log("clicked")
    this.router.navigate(['login']);
  }
  ngOnInit() {
  }
}