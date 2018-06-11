import { Component, OnInit } from '@angular/core';
import { SearchService } from '../services/search.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
  searchText: string;
  

  constructor(private api: SearchService) { }

  doSearch() {
    this.api.searchPeopleAndPost(this.searchText);
  }
  ngOnInit() {
    
  }

}
