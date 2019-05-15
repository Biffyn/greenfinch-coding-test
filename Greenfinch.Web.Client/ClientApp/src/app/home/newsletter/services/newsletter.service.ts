import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subcription } from '../models/subscrption.model';

@Injectable({
  providedIn: 'root'
})
export class NewsletterService {
  constructor(private http: HttpClient) {}

  public subscribe(formdata: Subcription) {
    return this.http.post('newsletter/subscribe', JSON.stringify(formdata));
  }
}
