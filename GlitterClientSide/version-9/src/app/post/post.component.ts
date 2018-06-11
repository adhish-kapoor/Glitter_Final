import { Component, OnInit } from '@angular/core';
import { SearchService } from '../services/search.service';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {
  searchedPost:object;
  
  constructor(private api: SearchService) { }

  getSearchedResult(){
    this.searchedPost = this.api.getSearchedPost();
  }  

  ngOnInit() {
    this.getSearchedResult();
  }
}
