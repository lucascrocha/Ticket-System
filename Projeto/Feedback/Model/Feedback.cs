namespace Projeto.Feedback.Model
{
    using Projeto.Chamado.Model;

    public class Feedback
    {
        public int Id { get; set; }

        public int ChamadoId { get; set; }

        public Chamado Chamado { get; set; }

        public bool Resolvido { get; set; }

        public NotaChamado Nota { get; set; }

        public string Mensagem { get; set; }
    }
}