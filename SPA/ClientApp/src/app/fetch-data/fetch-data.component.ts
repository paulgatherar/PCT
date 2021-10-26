import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent implements OnInit {
  public people: Person[];
  private sortByAge: boolean = false;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  ngOnInit(): void {
    this.getPeople();
  }

  onSortByAgeClick() {
    this.sortByAge = !this.sortByAge;
    this.getPeople();
  }

  getPeople() {
    this.http.get<Person[]>(this.baseUrl + `api/people?sortByAge=${this.sortByAge}`).subscribe(result => {
      this.people = result;
    }, error => console.error(error));
  }
}

interface Person {
  id: number;
  name: string;
  age: number;
}
