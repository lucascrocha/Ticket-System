namespace Projeto.Chamado.Model
{
    using System;
    using Projeto.Pessoa.Model;

    public class Chamado
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }

        public Pessoa Cliente { get; set; }

        public string Titulo { get; set; }

        public TipoChamado Tipo { get; set; }

        public string Mensagem { get; set; }

        public bool Respondido { get; set; }

        public UrgenciaTipo Urgencia { get; set; }

        public DateTimeOffset DataChamadoAbertura { get; set; }

        public DateTimeOffset DataChamadoFechamento { get; set; }

        public string RespostaAtendente { get; set; }

        public string RespostaCliente { get; set; }

        public SituacaoChamado Situacao { get; set; }
    }
}