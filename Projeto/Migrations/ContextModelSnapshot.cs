using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Projeto.Base;
using Projeto.Chamado.Model;
using Projeto.Pessoa.Model;

namespace Projeto.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Projeto.Chamado.Model.Chamado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClienteId");

                    b.Property<DateTimeOffset>("DataChamadoAbertura");

                    b.Property<DateTimeOffset>("DataChamadoFechamento");

                    b.Property<string>("Mensagem");

                    b.Property<bool>("Respondido");

                    b.Property<string>("RespostaAtendente");

                    b.Property<string>("RespostaCliente");

                    b.Property<short>("Situacao");

                    b.Property<short>("Tipo");

                    b.Property<string>("Titulo");

                    b.Property<short>("Urgencia");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Chamado");
                });

            modelBuilder.Entity("Projeto.Feedback.Model.Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ChamadoId");

                    b.Property<string>("Mensagem");

                    b.Property<short>("Nota");

                    b.Property<bool>("Resolvido");

                    b.HasKey("Id");

                    b.HasIndex("ChamadoId");

                    b.ToTable("Feedback");
                });

            modelBuilder.Entity("Projeto.Pessoa.Model.Pessoa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Cpf");

                    b.Property<string>("Email");

                    b.Property<string>("Nome");

                    b.Property<string>("Senha");

                    b.Property<short>("Tipo");

                    b.Property<string>("Usuario");

                    b.HasKey("Id");

                    b.ToTable("Pessoa");
                });

            modelBuilder.Entity("Projeto.Chamado.Model.Chamado", b =>
                {
                    b.HasOne("Projeto.Pessoa.Model.Pessoa", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Projeto.Feedback.Model.Feedback", b =>
                {
                    b.HasOne("Projeto.Chamado.Model.Chamado", "Chamado")
                        .WithMany()
                        .HasForeignKey("ChamadoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
