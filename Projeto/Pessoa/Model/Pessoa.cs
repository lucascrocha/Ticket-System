namespace Projeto.Pessoa.Model
{
    using System.Collections.Generic;

    public class Pessoa
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Cpf { get; set; }

        public PessoaTipo Tipo { get; set; }

        public string Usuario { get; set; }

        public string Senha { get; set; }

        public List<PessoaTelefone> Telefones { get; set; }

        public List<PessoaEndereco> Enderecos { get; set; }
    }
}