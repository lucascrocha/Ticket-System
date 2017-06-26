import { Component } from '@angular/core';
import { Server } from '../../app/type';
import { FeedbackService } from './feedback.service';
import { NavParams, ToastController, NavController } from 'ionic-angular';
import { Http, Response } from '@angular/http';

@Component({
    templateUrl: 'feedback.html'
})
export class FeedbackPage {
    feedback: Server.Feedback;

    feedbackId: number;

    chamadoId: number;

    constructor(
        private feedbackService: FeedbackService,
        private navParams: NavParams,
        private toastController: ToastController,
        private navController: NavController,
        private http: Http) { }

    ionViewWillLoad() {
        this.feedbackId = this.navParams.get('feedbackId');
        this.chamadoId = this.navParams.get('chamadoId');

        if (!this.feedbackId)
            this.feedback = {
                mensagem: '',
                chamadoId: this.chamadoId,
                nota: null,
                resolvido: false
            };
        else
            this.obterFeedback();
    }

    confirmar() {
        this.feedbackService.postFeedback(this.feedback);

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

    obterFeedback() {
        this.http.get(`http://localhost:61234/api/Feedback/${this.feedbackId}`)
            .map((res: Response) => {
                return res.json();
            })
            .subscribe(f => this.feedback = f);
    }
}