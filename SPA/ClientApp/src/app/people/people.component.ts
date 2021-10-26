import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Person } from './Person';

@Component({
  selector: 'app-people',
  templateUrl: './people.component.html'
})
export class PeopleComponent implements OnInit {
  public people: Person[];
  public sortByAge: boolean = false;
  public reverseNames: boolean = false;
  public selectByAge: boolean = false;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  ngOnInit(): void {
    this.getPeople();
  }

  onSortByAgeChange() {
    this.sortByAge = !this.sortByAge;
    this.getPeople();
  }

  onReverseNamesChange() {
    this.reverseNames = !this.reverseNames;
    this.getPeople();
  }

  onSelectByAgeChange() {
    this.selectByAge = !this.selectByAge;
    this.getPeople();
  }

  getPeople() {
    this.http.get<Person[]>(this.baseUrl + `api/people?sortByAge=${this.sortByAge}&reverseNames=${this.reverseNames}&selectByAge=${this.selectByAge}`).subscribe(result => {
      this.people = result;
    }, error => console.error(error));
  }
}
