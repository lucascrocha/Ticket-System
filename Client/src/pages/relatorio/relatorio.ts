import { Component, ViewChild } from '@angular/core';
import { Chart } from 'chart.js';
import { Http, Response } from '@angular/http';
import { Server } from '../../app/type';

@Component({
    selector : 'relatorio-page',
    templateUrl: 'relatorio.html'
})
export class RelatorioPage {
    relatorio: Server.Relatorio;

    @ViewChild('barCanvas') barCanvas;
    @ViewChild('doughnutCanvas') doughnutCanvas;
    @ViewChild('lineCanvas') lineCanvas;

    barraGrafico: any;
    donutGrafico: any;
    linhaGrafico: any;

    constructor(private http: Http) { }

    ionViewDidLoad() {
        this.carregarRelatorio();
    }

    ionViewDidEnter() {
        this.chart();
    }

    carregarRelatorio() {
        this.http.get('http://localhost:61234/api/Relatorio')
            .map((res: Response) => {
                return res.json();
            })
            .subscribe(r => {
                this.relatorio = r;
            });
    }

    chart() {
        this.barraGrafico = new Chart(this.barCanvas.nativeElement, {

            type: 'bar',
            data: {
                labels: ["Reclamação", "Sugestão", "Dúvida"],
                datasets: [{
                    label: '# de Tipos',
                    data: [this.relatorio.quantidadeTipoReclamacao, this.relatorio.quantidadeTipoSugestao, this.relatorio.quantidadeTipoDuvida],
                    backgroundColor: [
                        'rgba(255, 99, 132, 0.2)',
                        'rgba(54, 162, 235, 0.2)',
                        'rgba(255, 206, 86, 0.2)',
                    ],
                    borderColor: [
                        'rgba(255,99,132,1)',
                        'rgba(54, 162, 235, 1)',
                        'rgba(255, 206, 86, 1)',
                    ],
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }
            }

        });

        this.donutGrafico = new Chart(this.doughnutCanvas.nativeElement, {

            type: 'doughnut',
            data: {
                labels: ["Alto", "Médio", "Baixo"],
                datasets: [{
                    label: '# of Votes',
                    data: [this.relatorio.quantidadeUrgenciaAlta, this.relatorio.quantidadeUrgenciaMedia, this.relatorio.quantidadeUrgenciaBaixa],
                    backgroundColor: [
                        'rgba(255, 99, 132, 0.2)',
                        'rgba(54, 162, 235, 0.2)',
                        'rgba(255, 206, 86, 0.2)',
                    ],
                    hoverBackgroundColor: [
                        "#FF6384",
                        "#36A2EB",
                        "#FFCE56",
                    ]
                }]
            }

        });

        this.linhaGrafico = new Chart(this.lineCanvas.nativeElement, {

            type: 'line',
            data: {
                labels: ["Excelente", "Muito bom", "Bom", "Razoável", "Ruim", "Péssimo"],
                datasets: [
                    {
                        label: "Notas resultantes do feedback",
                        fill: false,
                        lineTension: 0.1,
                        backgroundColor: "rgba(75,192,192,0.4)",
                        borderColor: "rgba(75,192,192,1)",
                        borderCapStyle: 'butt',
                        borderDash: [],
                        borderDashOffset: 0.0,
                        borderJoinStyle: 'miter',
                        pointBorderColor: "rgba(75,192,192,1)",
                        pointBackgroundColor: "#fff",
                        pointBorderWidth: 1,
                        pointHoverRadius: 5,
                        pointHoverBackgroundColor: "rgba(75,192,192,1)",
                        pointHoverBorderColor: "rgba(220,220,220,1)",
                        pointHoverBorderWidth: 2,
                        pointRadius: 1,
                        pointHitRadius: 10,
                        data: [this.relatorio.quantidadeNotaExcelente,
                        this.relatorio.quantidadeNotaMuitoBom,
                        this.relatorio.quantidadeNotaBom,
                        this.relatorio.quantidadeNotaRazoavel,
                        this.relatorio.quantidadeNotaRuim,
                        this.relatorio.quantidadeNotaPessimo],
                        spanGaps: false,
                    }
                ]
            }

        });
    }
}