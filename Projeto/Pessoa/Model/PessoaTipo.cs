namespace Projeto.Pessoa.Model
{
    using System.ComponentModel.DataAnnotations;

    public enum PessoaTipo : short
    {
        [Display(Name = "Cliente")]
        Cliente = 1,

        [Display(Name = "Funcionário")]
        Funcionario = 2
    }
}