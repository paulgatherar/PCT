import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public people: Person[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Person[]>(baseUrl + 'api/people').subscribe(result => {
      this.people = result;
    }, error => console.error(error));
  }
}

interface Person {
  id: number;
  name: string;
  age: number;
}
