using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infra.DataBase;

public partial class STC_Context : DbContext
{
    public STC_Context()
    {
    }

    public STC_Context(DbContextOptions<STC_Context> options)
        : base(options)
    {
    }

    public virtual DbSet<OrdensServicos> OrdensServicos { get; set; }

    public virtual DbSet<RoteiroDetalhes> RoteiroDetalhes { get; set; }

    public virtual DbSet<Roteiros> Roteiros { get; set; }

    public virtual DbSet<TabAgentes> TabAgentes { get; set; }

    public virtual DbSet<TabBairros> TabBairros { get; set; }

    public virtual DbSet<TabClientes> TabClientes { get; set; }

    public virtual DbSet<TabMunicipios> TabMunicipios { get; set; }

    public virtual DbSet<TabServicos> TabServicos { get; set; }

    public virtual DbSet<TabUsuarios> TabUsuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=vixmobile.com.br;Initial Catalog=Darwin_STC_2025;Integrated Security=false;User ID=sa;Password=@And#Siller;Persist Security Info=True;Encrypt=True;TrustServerCertificate=yes");


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrdensServicos>(entity =>
        {
            entity.HasKey(e => e.IdOrdemServico).HasName("PK_OrdensServicoes");

            entity.HasIndex(e => e.IdTabMunicipio, "IX_OrdensServicoes");

            entity.HasIndex(e => e.IdTabBairro, "IX_OrdensServicoes_1");

            entity.HasIndex(e => e.Endereco, "IX_OrdensServicoes_2");

            entity.HasIndex(e => e.Matricula, "IX_OrdensServicoes_3");

           

            entity.Property(e => e.CNPJ)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CPF)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Ciclo)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Classificacao)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Complemento)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DataBaixa).HasColumnType("datetime");
            entity.Property(e => e.DataImportacao).HasColumnType("datetime");
            entity.Property(e => e.DataLimite).HasColumnType("datetime");
            entity.Property(e => e.DataRecepcao).HasColumnType("datetime");
            entity.Property(e => e.DataRegistro).HasColumnType("datetime");
            entity.Property(e => e.Endereco)
                .HasMaxLength(100)
                .IsUnicode(false);
           
            entity.Property(e => e.Informacoes)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.LocalizacaoPadrao)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Matricula)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.NomeCliente)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.NomeSolicitante)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.NumHD)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.NumeroCasa)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.NumeroOS)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Referencia)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TelCliente)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.TelSolicitante)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.TipoPadrao)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.UltimaLeitura)
                .HasMaxLength(6)
                .IsUnicode(false);

            entity.HasOne(d => d.IdTabBairroNavigation).WithMany(p => p.OrdensServicos)
                .HasForeignKey(d => d.IdTabBairro)
                .HasConstraintName("FK_OrdensServicos_TabBairros");

            entity.HasOne(d => d.IdTabMunicipioNavigation).WithMany(p => p.OrdensServicos)
                .HasForeignKey(d => d.IdTabMunicipio)
                .HasConstraintName("FK_OrdensServicos_TabMunicipios");

            entity.HasOne(d => d.IdTabServicoNavigation).WithMany(p => p.OrdensServicos)
                .HasForeignKey(d => d.IdTabServico)
                .HasConstraintName("FK_OrdensServicos_TabServicos");
        });

        modelBuilder.Entity<RoteiroDetalhes>(entity =>
        {
            entity.HasKey(e => e.IdRoteiroDetalhe);

            entity.HasIndex(e => e.IdOrdemServico, "IX_RoteiroDetalhes");

            entity.HasIndex(e => e.IdRoteiro, "IX_RoteiroDetalhes_1");

            entity.HasIndex(e => e.DataIniAtendimento, "IX_RoteiroDetalhes_2");

            entity.Property(e => e.DataFimAtendimento).HasColumnType("datetime");
            entity.Property(e => e.DataIniAtendimento).HasColumnType("datetime");
            entity.Property(e => e.HoraDownload).HasColumnType("datetime");
            entity.Property(e => e.HoraUpload).HasColumnType("datetime");
            entity.Property(e => e.Latitude)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Longitude)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdOrdemServicoNavigation).WithMany(p => p.RoteiroDetalhes)
                .HasForeignKey(d => d.IdOrdemServico)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_RoteiroDetalhes_OrdensServicos");

            entity.HasOne(d => d.IdRoteiroNavigation).WithMany(p => p.RoteiroDetalhes)
                .HasForeignKey(d => d.IdRoteiro)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_RoteiroDetalhes_Roteiros");
        });

        modelBuilder.Entity<Roteiros>(entity =>
        {
            entity.HasKey(e => e.IdRoteiro);

            entity.HasIndex(e => e.Data, "IX_Roteiros");

            entity.HasIndex(e => e.IdTabAgente, "IX_Roteiros_1");

        

            entity.Property(e => e.DataUltComunicacao).HasColumnType("datetime");
            

            entity.HasOne(d => d.IdTabAgenteNavigation).WithMany(p => p.Roteiros)
                .HasForeignKey(d => d.IdTabAgente)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Roteiros_TabAgentes");
        });

        modelBuilder.Entity<TabAgentes>(entity =>
        {
            entity.HasKey(e => e.IdTabAgente);

            entity.HasIndex(e => e.Nome, "IX_TabAgentes");

            entity.HasIndex(e => e.Matricula, "IX_TabAgentes_1");

     
            entity.Property(e => e.Matricula)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TabBairros>(entity =>
        {
            entity.HasKey(e => e.IdTabBairro);

            entity.HasIndex(e => e.Descricao, "IX_TabBairros");

            entity.HasIndex(e => e.IdTabMunicipio, "IX_TabBairros_1");

            entity.Property(e => e.Descricao)
                .HasMaxLength(60)
                .IsUnicode(false);

            entity.HasOne(d => d.IdTabMunicipioNavigation).WithMany(p => p.TabBairros)
                .HasForeignKey(d => d.IdTabMunicipio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TabBairros_TabMunicipios");
        });

        modelBuilder.Entity<TabClientes>(entity =>
        {
            entity.HasKey(e => e.IdTableCliente);

            entity.Property(e => e.IdTableCliente).ValueGeneratedNever();
            entity.Property(e => e.CNPJ)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CPF)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefone)
                .HasMaxLength(15)
                .IsUnicode(false);
        });


        modelBuilder.Entity<TabMunicipios>(entity =>
        {
            entity.HasKey(e => e.IdTabMunicipio);

            entity.HasIndex(e => e.Descricao, "IX_TabMunicipios");

            entity.Property(e => e.Descricao)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TabServicos>(entity =>
        {
            entity.HasKey(e => e.IdTabServico);

            entity.HasIndex(e => e.Descricao, "IX_TabServicos");

            entity.HasIndex(e => e.CodServico, "IX_TabServicos_1");

            entity.Property(e => e.CodServico)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Descricao)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TabUsuarios>(entity =>
        {
            entity.HasKey(e => e.IdTabUsuarios);

            entity.Property(e => e.Cargo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Matricula)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Senha)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
