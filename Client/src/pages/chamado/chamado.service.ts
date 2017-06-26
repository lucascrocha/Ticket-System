import { Injectable } from '@angular/core';
import { Http, Response, Headers } from '@angular/http';
import 'rxjs/Rx';
import { Server } from '../../app/type';

@Injectable()
export class ChamadoService {
    constructor(private http: Http) { }

    getChamados() {
        this.http.get('http://localhost:61234/api/Chamado')
            .map((res: Response) => {
                return res.json();
            })
            .subscribe();
    }

    postChamado(chamado: Server.Chamado) {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        this.http.post('http://localhost:61234/api/Chamado', JSON.stringify(chamado), { headers: headers }).subscribe();
    }

    putChamado(chamado: Server.Chamado) {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        this.http.put(`http://localhost:61234/api/Chamado/${chamado.id}`, JSON.stringify(chamado), { headers: headers }).subscribe();
    }
}