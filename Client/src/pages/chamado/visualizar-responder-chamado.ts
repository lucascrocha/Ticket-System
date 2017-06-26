import { Component } from '@angular/core';
import { Server } from '../../app/type';
import { NavParams, ToastController, ViewController } from 'ionic-angular';
import { Http, Response } from '@angular/http';
import { ChamadoService } from './chamado.service';
import * as moment from 'moment';

@Component({
    templateUrl: 'visualizar-responder-chamado.html'
})
export class VisualizarResponderChamadoPage {
    chamado: Server.Chamado;

    chamadoId: number;

    pessoaTipo: Server.PessoaTipo;

    tipoPessoa = Server.PessoaTipo;

    situacao = Server.SituacaoChamado;

    constructor(
        private navParams: NavParams,
        private http: Http,
        private chamadoService: ChamadoService,
        private toastController: ToastController,
        private viewController: ViewController) { }

    ionViewWillLoad() {
        this.chamadoId = this.navParams.get('chamadoId');
        this.pessoaTipo = this.navParams.get('pessoaTipo');

        this.obterChamado();
    }

    confirmar() {
        this.chamado.dataChamadoFechamento = <any>moment().format();

        if (!this.chamado.respondido)
            this.chamado.situacao = Server.SituacaoChamado.Atendendo;
        else
            this.chamado.situacao = Server.SituacaoChamado.Concluido;

        this.chamadoService.putChamado(this.chamado);

        let toast = this.toastController.create({
            position: 'bottom',
            message: 'Chamado respondido com sucesso',
            duration: 1000
        });

        toast.present();

        toast.onDidDismiss(e => {
            this.viewController.dismiss(this.chamado);
        });
    }

    obterChamado() {
        this.http.get(`http://localhost:61234/api/Chamado/${this.chamadoId}`)
            .map((res: Response) => {
                return res.json();
            })
            .subscribe(c => this.chamado = c);
    }
}