import { Component, OnInit } from '@angular/core';
import { SearchService } from '../services/search.service';

@Component({
  selector: 'app-people',
  templateUrl: './people.component.html',
  styleUrls: ['./people.component.css']
})
export class PeopleComponent implements OnInit {
  searchedPeople:object;

  constructor(private api: SearchService) { }

  getSearchedResult(){
    this.searchedPeople = this.api.getSearchedPeople();
    // location.reload();
  }  

  ngOnInit() {
    this.getSearchedResult();
  }
}