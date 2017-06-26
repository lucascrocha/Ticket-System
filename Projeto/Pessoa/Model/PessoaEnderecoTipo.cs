namespace Projeto.Pessoa.Model
{
    using System.ComponentModel.DataAnnotations;

    public enum PessoaEnderecoTipo : short
    {
        [Display(Name = "Residencial")]
        Residencial = 1,

        [Display(Name = "Cobrança")]
        Cobranca = 2,

        [Display(Name = "Trabalho")]
        Trabalho = 3
    }
}