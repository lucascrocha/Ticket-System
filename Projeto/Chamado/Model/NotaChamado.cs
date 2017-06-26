namespace Projeto.Chamado.Model
{
    using System.ComponentModel.DataAnnotations;

    public enum NotaChamado : short
    {
        [Display(Name = "Excelente")]
        Excelente = 1,

        [Display(Name = "Muito bom")]
        MuitoBom = 2,

        [Display(Name = "Bom")]
        Bom = 3,

        [Display(Name = "Razoável")]
        Razoavel = 4,

        [Display(Name = "Ruim")]
        Ruim = 5,

        [Display(Name = "Péssimo")]
        Pessimo = 6
    }
}