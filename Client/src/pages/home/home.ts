import { Component } from '@angular/core';
import { NavController, NavParams, ModalController } from 'ionic-angular';
import { Server } from '../../app/type';
import { ChamadoPage } from '../chamado/chamado';
import { ListaChamadosPage } from '../chamado/lista-chamados';
import { VisualizarResponderChamadoPage } from '../chamado/visualizar-responder-chamado';
import { RelatorioPage } from '../relatorio/relatorio';
import { Http, Response } from '@angular/http';

@Component({
    selector: 'page-home',
    templateUrl: 'home.html'
})
export class HomePage {
    pessoaTipo: Server.PessoaTipo;

    pessoa: Server.Pessoa;

    chamados: Server.Chamado[];

    tipoPessoa = Server.PessoaTipo;

    constructor(
        public navCtrl: NavController,
        private modalController: ModalController,
        private navParams: NavParams,
        private http: Http) {
    }

    ionViewWillLoad() {
        this.pessoaTipo = this.navParams.get('pessoaTipo');

        this.pessoa = {
            id: null,
            cpf: '',
            email: '',
            nome: '',
            tipo: null,
            senha: '',
            usuario: ''
        };

        if (this.pessoaTipo == Server.PessoaTipo.Funcionario)
            this.carregarChamados();
    }

    responderChamado(chamado: Server.Chamado) {
        if (!chamado.respondido) {
            let modal = this.modalController.create(VisualizarResponderChamadoPage, { chamadoId: chamado.id, pessoaTipo: this.pessoaTipo });
            modal.present();
            modal.onDidDismiss(chamado => {
                if (chamado)
                    this.carregarChamados();
            })
        }
    }

    abrirNovoChamado() {
        this.navCtrl.push(ChamadoPage)
    }

    feedback() {
        this.navCtrl.push(ListaChamadosPage, { chamadoArquivados: false, pessoaTipo: this.pessoaTipo });
    }

    abrirChamadoArquivados() {
        this.navCtrl.push(ListaChamadosPage, { chamadoArquivados: true, pessoaTipo: this.pessoaTipo });
    }

    relatorio() {
        this.navCtrl.push(RelatorioPage);
    }

    carregarChamados() {
        this.http.get('http://localhost:61234/api/Chamado')
            .map((res: Response) => {
                return res.json();
            })
            .subscribe(c => {
                this.chamados = c;

                for (var chamado of this.chamados) {
                    switch (chamado.tipo) {
                        case Server.TipoChamado.Duvida:
                            chamado.tipoDescricao = "Dúvida";
                            break;
                        case Server.TipoChamado.Reclamacao:
                            chamado.tipoDescricao = "Reclamação";
                            break;
                        case Server.TipoChamado.Sugestao:
                            chamado.tipoDescricao = "Sugestão";
                            break;
                        default:
                            break;
                    }

                    switch (chamado.urgencia) {
                        case Server.UrgenciaTipo.Alta:
                            chamado.urgenciaDescricao = "Alta";
                            break;
                        case Server.UrgenciaTipo.Baixa:
                            chamado.urgenciaDescricao = "Baixa";
                            break;
                        case Server.UrgenciaTipo.Media:
                            chamado.urgenciaDescricao = "Média";
                            break;
                        default:
                            break;
                    }

                    switch (chamado.situacao) {
                        case Server.SituacaoChamado.Atendendo:
                            chamado.badgeCor = 'dark';
                            chamado.badgeDescricao = 'Atendendo';
                            break;
                        case Server.SituacaoChamado.Concluido:
                            chamado.badgeCor = 'secondary';
                            chamado.badgeDescricao = 'Concluído';
                            break;
                        case Server.SituacaoChamado.Novo:
                            chamado.badgeCor = 'primary';
                            chamado.badgeDescricao = 'Novo';
                            break;
                        default:
                            break;
                    }
                }
            });
    }
}