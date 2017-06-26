namespace Projeto.Chamado.Model
{
    using System.ComponentModel.DataAnnotations;

    public enum UrgenciaTipo : short
    {
        [Display(Name = "Baixa")]
        Baixa = 1,

        [Display(Name = "Média")]
        Media = 2,

        [Display(Name = "Alta")]
        Alta = 3
    }
}