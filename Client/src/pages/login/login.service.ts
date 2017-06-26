import { Injectable } from '@angular/core';
import { Http, Response, Headers } from '@angular/http';
import 'rxjs/Rx';
import { Server } from '../../app/type';

@Injectable()
export class LoginService {
    constructor(private http: Http) { }

    getPessoas() {
        this.http.get('http://localhost:61234/api/Pessoa')
            .map((res: Response) => {
                return res.json();
            })
            .subscribe();
    }

    postPessoa(pessoa: Server.Pessoa) {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        this.http.post('http://localhost:61234/api/Pessoa', JSON.stringify(pessoa), { headers: headers }).subscribe();
    }
}