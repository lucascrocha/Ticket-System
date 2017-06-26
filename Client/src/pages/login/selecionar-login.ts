import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import { LogarPage } from './logar';
import { Server } from '../../app/type';

@Component({
    selector: 'selecionar-login',
    templateUrl: 'selecionar-login.html'
})
export class SelecionarLoginPage {
    constructor(private navCtrl: NavController) { }

    ionViewWillLoad() {

    }

    logarCliente() {
        this.navCtrl.push(LogarPage, { pessoaTipo: Server.PessoaTipo.Cliente });
    }

    logarFuncionario() {
        this.navCtrl.push(LogarPage, { pessoaTipo: Server.PessoaTipo.Funcionario });
    }
}