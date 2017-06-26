namespace Projeto.Pessoa.Model
{
    public class PessoaTelefone
    {
        public int Id { get; set; }

        public int PessoaId { get; set; }

        public Pessoa Pessoa { get; set; }

        public string TelefoneResidencial { get; set; }

        public string TelefoneCelular { get; set; }

        public string TelefoneTrabalho { get; set; }

        public string Ramal { get; set; }
    }
}