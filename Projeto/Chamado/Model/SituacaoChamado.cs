namespace Projeto.Chamado.Model
{
    using System.ComponentModel.DataAnnotations;

    public enum SituacaoChamado : short
    {
        [Display(Name = "Novo")]
        Novo = 1,

        [Display(Name = "Atendendo")]
        Atendendo = 2,

        [Display(Name = "Concluído")]
        Concluido = 3
    }
}
