import { Component } from '@angular/core';
import { Server } from '../../app/type';
import { NavParams, ToastController, NavController } from 'ionic-angular';
import { ChamadoService } from './chamado.service';
import * as moment from 'moment';

@Component({
    templateUrl: 'chamado.html'
})
export class ChamadoPage {
    chamadoTipos: Server.TipoChamado[];

    urgenciaTipo = Server.UrgenciaTipo;

    chamado: Server.Chamado;

    desabilitarBotao: boolean = true;

    constructor(
        private navParams: NavParams,
        private chamadoService: ChamadoService,
        private toastController: ToastController,
        private navController: NavController) { }

    ionViewWillLoad() {
        this.chamado = {
            titulo: '',
            mensagem: '',
            clienteId: 1,
            respondido: false,
            tipo: null,
            urgencia: null,
            respostaAtendente: '',
            respostaCliente: '',
            dataChamadoAbertura: <any>moment().format(),
            situacao: Server.SituacaoChamado.Novo
        };
    }

    confirmar() {
        this.chamadoService.postChamado(this.chamado);

        let toast = this.toastController.create({
            position: 'bottom',
            message: 'Chamado criado com sucesso',
            duration: 1000
        });

        toast.present();

        toast.onDidDismiss(e => {
            this.navController.pop();
        });
    }

    habilitarBotao() {
        if (this.chamado.mensagem.length > 1
            && this.chamado.titulo.length > 1
            && this.chamado.tipo != null
            && this.chamado.urgencia != null)
            this.desabilitarBotao = false;
        else
            this.desabilitarBotao = true;
    }
}