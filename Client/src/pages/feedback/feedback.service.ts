import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import 'rxjs/Rx';
import { Server } from '../../app/type';

@Injectable()
export class FeedbackService {
    constructor(private http: Http) { }

    postFeedback(feedback: Server.Feedback) {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        this.http.post('http://localhost:61234/api/Feedback', JSON.stringify(feedback), { headers: headers }).subscribe();
    }
}