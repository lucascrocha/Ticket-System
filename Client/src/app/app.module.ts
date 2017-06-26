import { NgModule, ErrorHandler } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { IonicApp, IonicModule, IonicErrorHandler } from 'ionic-angular';
import { MyApp } from './app.component';
import { HttpModule } from '@angular/http';
import { HomePage } from '../pages/home/home';
import { TabsPage } from '../pages/tabs/tabs';
import { SelecionarLoginPage } from '../pages/login/selecionar-login';
import { LogarPage } from '../pages/login/logar';
import { LoginService } from '../pages/login/login.service';
import { LoginCriarPessoaPage } from '../pages/login/criar-login-pessoa';
import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';
import { ChamadoPage } from '../pages/chamado/chamado';
import { ListaChamadosPage } from '../pages/chamado/lista-chamados';
import { VisualizarResponderChamadoPage } from '../pages/chamado/visualizar-responder-chamado';
import { FeedbackPage } from '../pages/feedback/feedback';
import { ChamadoService } from '../pages/chamado/chamado.service';
import { RelatorioPage } from '../pages/relatorio/relatorio';
import { FeedbackService } from '../pages/feedback/feedback.service';

@NgModule({
    declarations: [
        MyApp,
        HomePage,
        SelecionarLoginPage,
        LogarPage,
        LoginCriarPessoaPage,
        ChamadoPage,
        ListaChamadosPage,
        VisualizarResponderChamadoPage,
        TabsPage,
        RelatorioPage,
        FeedbackPage
    ],
    imports: [
        BrowserModule,
        IonicModule.forRoot(MyApp),
        HttpModule
    ],
    bootstrap: [IonicApp],
    entryComponents: [
        MyApp,
        HomePage,
        SelecionarLoginPage,
        LoginCriarPessoaPage,
        ChamadoPage,
        ListaChamadosPage,
        LogarPage,
        RelatorioPage,
        TabsPage,
        VisualizarResponderChamadoPage,
        FeedbackPage
    ],
    providers: [
        StatusBar,
        SplashScreen,
        { provide: ErrorHandler, useClass: IonicErrorHandler },
        LoginService,
        FeedbackService,
        ChamadoService
    ]
})
export class AppModule { }
