import { Component } from '@angular/core';
import { Server } from '../../app/type';
import { ModalController, NavParams, NavController } from 'ionic-angular';
import { LoginCriarPessoaPage } from './criar-login-pessoa';
import { LoginService } from './login.service';
import { HomePage } from '../home/home';

@Component({
    selector: 'logar',
    templateUrl: 'logar.html'
})
export class LogarPage {
    desabilitarBotao: boolean = true;

    login: Server.Pessoa;

    resultado: boolean = false;

    pessoaTipo: Server.PessoaTipo;

    tipoPessoa = Server.PessoaTipo;
    constructor(
        private modalController: ModalController,
        private navParams: NavParams,
        private loginService: LoginService,
        private navController: NavController
    ) { }

    ionViewWillLoad() {
        this.pessoaTipo = this.navParams.get('pessoaTipo');
        this.login = {
            usuario: '',
            senha: ''
        };
    }

    novoUsuario() {
        this.navController.push(LoginCriarPessoaPage, { pessoaTipo: this.pessoaTipo });
    }

    logar() {
        //this.loginService.logar(this.login);
        this.resultado = true;
        if (this.resultado) {
            if (this.tipoPessoa.Cliente)
                this.navController.push(HomePage, { pessoaTipo: this.pessoaTipo });
            else
                this.navController.push(HomePage, { pessoaTipo: this.pessoaTipo });
        }
    }

    habilitarBotao() {
        if (this.login.usuario.length >= 4 && this.login.senha.length >= 4)
            this.desabilitarBotao = false;
        else
            this.desabilitarBotao = true;
    }
}