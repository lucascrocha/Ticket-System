import { Component } from '@angular/core';
import { Server } from '../../app/type';
import { LoginService } from './login.service';
import { ViewController, NavParams, NavController, ToastController } from 'ionic-angular';

@Component({
    templateUrl: 'criar-login-pessoa.html'
})
export class LoginCriarPessoaPage {
    pessoa: Server.Pessoa;

    desabilitarBotao: boolean = true;

    pessoaTipo: Server.PessoaTipo;

    tipoPessoa = Server.PessoaTipo;
    constructor(
        private loginService: LoginService,
        private navParams: NavParams,
        private viewController: ViewController,
        private navController: NavController,
        private toastController: ToastController) { }

    ionViewWillLoad() {
        this.pessoaTipo = this.navParams.get('pessoaTipo');

        this.pessoa = {
            cpf: '',
            email: '',
            nome: '',
            tipo: this.pessoaTipo,
            senha: '',
            usuario: ''
        };
    }

    confirmar() {
        this.loginService.postPessoa(this.pessoa);

        let toast = this.toastController.create({
            position: 'bottom',
            message: 'Usuário cadastrado com sucesso',
            duration: 1000
        });

        toast.present();

        toast.onDidDismiss(e => {
            this.navController.pop();
        });
    }

    habilitarBotao() {
        if (this.pessoa.cpf.length > 1
            && this.pessoa.nome.length > 3
            && this.pessoa.email.length > 3
            && this.pessoa.usuario.length > 4
            && this.pessoa.senha.length > 4)
            this.desabilitarBotao = false;
        else
            this.desabilitarBotao = true;
    }
}