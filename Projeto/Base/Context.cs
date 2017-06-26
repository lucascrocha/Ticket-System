namespace Projeto.Base
{
    using Microsoft.EntityFrameworkCore;
    using Chamado.Model;
    using Feedback.Model;
    using Pessoa.Model;

    public class Context : DbContext
    { 
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<Chamado> Chamado { get; set; }

        public DbSet<Feedback> Feedback { get; set; }

        public DbSet<Pessoa> Pessoa { get; set; }
    }
}