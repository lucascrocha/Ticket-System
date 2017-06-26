namespace Projeto.Relatorio
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Projeto.Base;
    using Projeto.Chamado.Model;
    using Projeto.Feedback.Model;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Globalization;
    using System.Collections.Generic;

    [Produces("application/json")]
    [Route("api/Relatorio")]
    public class RelatorioController : Controller
    {
        public class Command
        {
            public int QuantidadeChamados { get; set; }

            public int QuantidadeTipoSugestao { get; set; }

            public int QuantidadeTipoDuvida { get; set; }

            public int QuantidadeTipoReclamacao { get; set; }

            public int QuantidadeUrgenciaAlta { get; set; }

            public int QuantidadeUrgenciaMedia { get; set; }

            public int QuantidadeUrgenciaBaixa { get; set; }

            public string MediaTempoResposta { get; set; }

            public int QuantidadeChamadosRespondidos { get; set; }

            public string MaiorQuantidadeTipo { get; set; }

            public int QuantidadeNotaExcelente { get; set; }

            public int QuantidadeNotaMuitoBom { get; set; }

            public int QuantidadeNotaBom { get; set; }

            public int QuantidadeNotaRazoavel { get; set; }

            public int QuantidadeNotaRuim { get; set; }

            public int QuantidadeNotaPessimo { get; set; }
        }

        readonly Context _context;

        public RelatorioController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        [ResponseCache(NoStore = true, Duration = 0)]
        public async Task<Command> Get()
        {
            var chamados = await _context.Set<Chamado>()
                .Select(e => new
                {
                    e.Urgencia,
                    e.Tipo,
                    e.DataChamadoAbertura,
                    e.DataChamadoFechamento,
                    e.Respondido
                })
                .ToListAsync();

            var feedbacks = await _context.Set<Feedback>()
                .Select(e => new
                {
                    e.Nota
                })
                .ToListAsync();

            var quantidadeChamados = chamados.Count;

            var quantidadeChamadosRespondidos = chamados.Where(e => e.Respondido).Count();

            var quantidadeDuvidas = 0;

            var quantidadeSugestoes = 0;

            var quantidadeReclamacoes = 0;

            var quantidadeUrgenciaBaixa = 0;

            var quantidadeUrgenciaMedia = 0;

            var quantidadeUrgenciaAlta = 0;

            foreach (var chamado in chamados)
            {
                switch (chamado.Tipo)
                {
                    case TipoChamado.Duvida:
                        quantidadeDuvidas++;
                        break;
                    case TipoChamado.Sugestao:
                        quantidadeSugestoes++;
                        break;
                    case TipoChamado.Reclamacao:
                        quantidadeReclamacoes++;
                        break;
                    default:
                        break;
                }

                switch (chamado.Urgencia)
                {
                    case UrgenciaTipo.Baixa:
                        quantidadeUrgenciaBaixa++;
                        break;
                    case UrgenciaTipo.Media:
                        quantidadeUrgenciaMedia++;
                        break;
                    case UrgenciaTipo.Alta:
                        quantidadeUrgenciaAlta++;
                        break;
                    default:
                        break;
                }
            }

            var quantidadeNotaExcelente = 0;
            var quantidadeNotaMuitoBom = 0;
            var quantidadeNotaBom = 0;
            var quantidadeNotaRazoavel = 0;
            var quantidadeNotaRuim = 0;
            var quantidadeNotaPessimo = 0;

            foreach (var feedback in feedbacks)
            {
                switch (feedback.Nota) 
                {
                    case NotaChamado.Excelente:
                        quantidadeNotaExcelente++;
                        break;
                    case NotaChamado.MuitoBom:
                        quantidadeNotaMuitoBom++;
                        break;
                    case NotaChamado.Bom:
                        quantidadeNotaBom++;
                        break;
                    case NotaChamado.Razoavel:
                        quantidadeNotaRazoavel++;
                        break;
                    case NotaChamado.Ruim:
                        quantidadeNotaRuim++;
                        break;
                    case NotaChamado.Péssimo:
                        quantidadeNotaPessimo++;
                        break;
                    default:
                        break;
                }
            }

            var maiorQuantidadeTipo = "";

            if (quantidadeReclamacoes > quantidadeDuvidas && quantidadeReclamacoes > quantidadeSugestoes)
                maiorQuantidadeTipo = "Reclamação";
            else if (quantidadeSugestoes > quantidadeReclamacoes && quantidadeSugestoes > quantidadeDuvidas)
                maiorQuantidadeTipo = "Sugestão";
            else if (quantidadeDuvidas > quantidadeReclamacoes && quantidadeDuvidas > quantidadeSugestoes)
                maiorQuantidadeTipo = "Dúvida";
            else if (quantidadeDuvidas == quantidadeReclamacoes && quantidadeReclamacoes == quantidadeSugestoes)
                maiorQuantidadeTipo = "Dados igualados";

            var dataAberturaChamados = chamados.Where(e => e.DataChamadoAbertura < e.DataChamadoFechamento).Select(e => e.DataChamadoAbertura).ToList();

            var dataFechamentoChamados = chamados.Where(e => e.DataChamadoFechamento > e.DataChamadoAbertura).Select(e => e.DataChamadoFechamento).ToList();

            var mediaTempoResposta = new TimeSpan();

            foreach (var abertura in dataAberturaChamados)
            {
                foreach (var fechamento in dataFechamentoChamados)
                {
                    var listaTimeSpan = new List<TimeSpan>();
                    var provider = CultureInfo.InvariantCulture;

                    listaTimeSpan.Add(new TimeSpan(
                        DateTimeOffset.ParseExact(fechamento.ToString("dd.MM.yyyy HH:mm:ss"), "dd.MM.yyyy HH:mm:ss", provider).Ticks -
                        DateTimeOffset.ParseExact(abertura.ToString("dd.MM.yyyy HH:mm:ss"), "dd.MM.yyyy HH:mm:ss", provider).Ticks
                        ));

                    var totalTicks = 0L;
                    foreach (var ts in listaTimeSpan)
                    {
                        totalTicks += ts.Ticks;
                    }
                    var ticks = totalTicks / listaTimeSpan.Count;
                    mediaTempoResposta = new TimeSpan(ticks);
                }
            }

            var command = new Command
            {
                QuantidadeChamados = quantidadeChamados,
                QuantidadeTipoDuvida = quantidadeDuvidas,
                QuantidadeTipoReclamacao = quantidadeReclamacoes,
                QuantidadeTipoSugestao = quantidadeSugestoes,
                QuantidadeUrgenciaAlta = quantidadeUrgenciaAlta,
                QuantidadeUrgenciaBaixa = quantidadeUrgenciaBaixa,
                QuantidadeUrgenciaMedia = quantidadeUrgenciaMedia,
                MediaTempoResposta = mediaTempoResposta.ToString(),
                QuantidadeChamadosRespondidos = quantidadeChamadosRespondidos,
                MaiorQuantidadeTipo = maiorQuantidadeTipo,
                QuantidadeNotaBom = quantidadeNotaBom,
                QuantidadeNotaExcelente = quantidadeNotaExcelente,
                QuantidadeNotaMuitoBom = quantidadeNotaMuitoBom,
                QuantidadeNotaRazoavel = quantidadeNotaRazoavel,
                QuantidadeNotaPessimo = quantidadeNotaPessimo,
                QuantidadeNotaRuim = quantidadeNotaRuim
            };

            return command;
        }
    }
}