namespace Projeto.Pessoa
{
    using FluentValidation;
    using Microsoft.EntityFrameworkCore;
    using Projeto.Base;
    using Projeto.Pessoa.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class InserirEditar
    {
        public class Query
        {
            public int Id { get; set; }
        }

        public class Command
        {
            public int? Id { get; set; }

            public string Nome { get; set; }

            public string Email { get; set; }

            public string Cpf { get; set; }

            public PessoaTipo Tipo { get; set; }

            public string Usuario { get; set; }

            public string Senha { get; set; }

            public List<PessoaEndereco> Enderecos { get; set; }

            public List<PessoaTelefone> Telefones { get; set; }
        }

        public class QueryHandler
        {
            readonly Context _context;

            public QueryHandler(Context context)
            {
                _context = context;
            }

            public async Task<Command> Handle(Query message)
            {
                var pessoa = await _context.Set<Pessoa>()
                    .Include(e => e.Enderecos)
                    .Include(t => t.Telefones)
                    .FirstOrDefaultAsync(e => e.Id == message.Id);

                var command = new Command
                {
                    Id = pessoa.Id,
                    Usuario = pessoa.Usuario,
                    Cpf = pessoa.Cpf,
                    Email = pessoa.Email,
                    Tipo = pessoa.Tipo,
                    Nome = pessoa.Nome,
                    Senha = pessoa.Senha,
                    Enderecos = pessoa.Enderecos,
                    Telefones = pessoa.Telefones
                };

                return command;
            }
        }

        public class ValidarPessoa : AbstractValidator<Pessoa>
        {
            readonly Context _context;

            public ValidarPessoa(Context context)
            {
                _context = context;

                RuleFor(e => e.Email).EmailAddress().WithMessage("Deve ser fornecido um email válido");
            }
        }

        public class CommandHandler
        {
            readonly Context _context;
            readonly ValidarPessoa _validator;

            public CommandHandler(Context context, ValidarPessoa validator)
            {
                _context = context;
                _validator = validator;
            }

            public async Task<int> Handle(Command message)
            {
                Pessoa pessoa;

                if (message.Id == null)
                {
                    pessoa = new Pessoa();
                    pessoa.Tipo = message.Tipo;
                    pessoa.Cpf = message.Cpf;
                }
                else
                {
                    pessoa = await _context.Set<Pessoa>()
                        .FirstOrDefaultAsync(e => e.Id == message.Id);
                }

                pessoa.Email = message.Email;
                pessoa.Nome = message.Nome;
                pessoa.Usuario = message.Usuario;

                foreach (var messageEndereco in message.Enderecos)
                {
                    foreach (var pessoaEndereco in pessoa.Enderecos)
                    {
                        pessoaEndereco.Bairro = messageEndereco.Bairro;
                        pessoaEndereco.Cep = messageEndereco.Cep;
                        pessoaEndereco.Cidade = messageEndereco.Cidade;
                        pessoaEndereco.Complemento = messageEndereco.Complemento;
                        pessoaEndereco.EnderecoTipo = messageEndereco.EnderecoTipo;
                        pessoaEndereco.Estado = messageEndereco.Estado;
                        pessoaEndereco.Logradouro = messageEndereco.Logradouro;
                        pessoaEndereco.Numero = messageEndereco.Numero;
                        pessoaEndereco.Referencia = messageEndereco.Referencia;
                    }
                }

                await _validator.ValidateAndThrowAsync(pessoa);

                _context.Add(pessoa);

                await _context.SaveChangesAsync();

                return pessoa.Id;
            }
        }
    }
}