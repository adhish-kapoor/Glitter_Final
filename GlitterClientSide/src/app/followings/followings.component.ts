import { Component, OnInit } from '@angular/core';
import { AuthUserService } from '../services/auth-user.service';

@Component({
  selector: 'app-followings',
  templateUrl: './followings.component.html',
  styleUrls: ['./followings.component.css']
})
export class FollowingsComponent implements OnInit {
  followings:object;
  currentUser:string;  
  imageSource:string;
  constructor(private api:AuthUserService) {
    this.currentUser = localStorage.getItem("uid");
    this.imageSource = "assets/images/default.png";
   }
  getFollowingsDetails(){
    this.api.getFollowingsDetails(this.currentUser)
    .subscribe(response=>{
      this.followings = response;      
      console.log(this.followings);
    },err=>{
      console.log(err);
    })
  }

  // getFollowings() {
  //   if(this.currentUser){
  //     this.api.getUserFollowings(this.currentUser)
  //     .subscribe(data => {
  //       this.followings = data;
  //     })
  //   }
  //   else{
  //     alert("You are not logged in...")
  //   }    
  // }
  unFollowUser(userId){
    this.api.unFollow(this.currentUser, userId)
    .subscribe(response=>{
      console.log(response)
      this.getFollowingsDetails(); //to remove user after unfollow
    })
  }

  ngOnInit() {
    this.getFollowingsDetails();
  }

}
