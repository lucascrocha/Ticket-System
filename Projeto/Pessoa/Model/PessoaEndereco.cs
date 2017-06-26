namespace Projeto.Pessoa.Model
{
    public class PessoaEndereco
    {
        public int Id { get; set; }

        public int PessoaId { get; set; }

        public Pessoa Pessoa { get; set; }

        public PessoaEnderecoTipo EnderecoTipo { get; set; }

        public string Logradouro { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string Referencia { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public string Cep { get; set; }
    }
}