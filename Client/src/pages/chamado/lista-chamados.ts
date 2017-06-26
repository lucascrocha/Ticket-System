import { Component } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';
import { FeedbackPage } from '../feedback/feedback';
import { Http, Response } from '@angular/http';
import { Server } from '../../app/type';
import { VisualizarResponderChamadoPage } from '../chamado/visualizar-responder-chamado';

@Component({
    templateUrl: 'lista-chamados.html'
})
export class ListaChamadosPage {
    chamados: Server.Chamado[];

    pessoaTipo: Server.PessoaTipo;

    feedbacks: Server.Feedback[];

    chamadoArquivados: boolean;

    constructor(
        private navController: NavController,
        private http: Http,
        private navParams: NavParams) { }

    ionViewWillLoad() {
        this.chamadoArquivados = this.navParams.get('chamadoArquivados');
        this.pessoaTipo = this.navParams.get('pessoaTipo');

        this.carregarChamados();
        this.carregarFeedbacks();
    }

    abrirChamado(chamado: Server.Chamado) {
        let feedback = this.feedbacks.find(e => e.chamadoId == chamado.id);

        if (feedback && !this.chamadoArquivados)
            this.navController.push(FeedbackPage, { chamadoId: chamado.id, feedbackId: feedback.id, pessoaTipo: this.pessoaTipo });
        else if (this.chamadoArquivados)
            this.navController.push(VisualizarResponderChamadoPage, { chamadoId: chamado.id, pessoaTipo: this.pessoaTipo });
        else if (chamado.situacao == Server.SituacaoChamado.Concluido)
            this.navController.push(FeedbackPage, { chamadoId: chamado.id, pessoaTipo: this.pessoaTipo });
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

    carregarFeedbacks() {
        this.http.get('http://localhost:61234/api/Feedback')
            .map((res: Response) => {
                return res.json();
            })
            .subscribe(f => this.feedbacks = f);
    }
}