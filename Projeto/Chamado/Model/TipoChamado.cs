namespace Projeto.Chamado.Model
{
    using System.ComponentModel.DataAnnotations;

    public enum TipoChamado : short
    {
        [Display(Name = "Dúvida")]
        Duvida = 1,

        [Display(Name = "Sugestão")]
        Sugestao = 2,

        [Display(Name = "Reclamação")]
        Reclamacao = 3
    }
}