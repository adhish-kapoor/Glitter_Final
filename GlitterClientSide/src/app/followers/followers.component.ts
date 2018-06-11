import { Component, OnInit } from '@angular/core';
import { AuthUserService } from '../services/auth-user.service';
import { SearchComponent } from '../search/search.component';
import { SearchService } from '../services/search.service';
import { Router } from '@angular/router'
@Component({
  selector: 'app-followers',
  templateUrl: './followers.component.html',
  styleUrls: ['./followers.component.css']
})
export class FollowersComponent implements OnInit {
  followers: object;
  currentUser: string;
  imageSource: string;
  constructor(private api: AuthUserService, private searchApi:SearchService,private router:Router) {
    this.currentUser = localStorage.getItem("uid");
    this.imageSource = "assets/images/default.png";
  }

  getFollowersDetails() {
    this.api.getFollowersDetails(this.currentUser)
      .subscribe(response => {
        this.followers = response;
        console.log(this.followers);
        
      }, err => {
        console.log(err);
      })
  }

  addFollowing(email){  
    if(this.currentUser){
      this.searchApi.addFollowing(email, this.currentUser)
      .subscribe(response =>{
        //this.getFollowersDetails();
        console.log("follow");
      })
    }
  }
    
  ngOnInit() {
    this.getFollowersDetails();
  }

}
