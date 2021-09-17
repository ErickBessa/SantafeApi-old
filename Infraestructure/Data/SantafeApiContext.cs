using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SantafeApi.Entities;
using SantafeApi.Infraestrucutre.Data;

namespace SantafeApi.Infraestrucutre.Data
{
    public partial class SantafeApiContext : IdentityDbContext<SantafeApiUser>
    {
        //TODO: mapear connection string do appsettings.json 
        public SantafeApiContext(DbContextOptions<SantafeApiContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<ControleOs> ControleOs { get; set; }
        public virtual DbSet<ItensVistoria> ItensVistorias { get; set; }
        public virtual DbSet<Item> Itens { get; set; }
        public virtual DbSet<Local> Locals { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Vistoria> Vistorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");



            modelBuilder.Entity<Bloco>(entity =>
            {
                entity.HasKey(e => e.CodBloco);

                entity.ToTable("tbBloco");

                entity.Property(e => e.NomeBloco)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodClienteNavigation)
                    .WithMany(p => p.Blocos)
                    .HasForeignKey(d => d.CodCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbBloco_tbCliente");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.CodCliente);

                entity.ToTable("tbCliente");

                entity.Property(e => e.CnpjCliente)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DataCad)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EnderecoCliente)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NomeCliente)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TecResponsavel)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telefone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TipoDoLocal)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne<SantafeApiUser>(c => c.SantafeApiUser)
                    .WithOne(s => s.ClienteNavigation)
                    .HasForeignKey<SantafeApiUser>(s => s.CodCliente)
                    .OnDelete(DeleteBehavior.SetNull);

            });

            modelBuilder.Entity<ControleOs>(entity =>
            {
                entity.HasKey(e => e.Cod);

                entity.ToTable("tbControleOs");

                entity.Property(e => e.Data)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DataVistoria)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodClienteNavigation)
                    .WithMany(p => p.ControleOs)
                    .HasForeignKey(d => d.CodCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbControleOs_tbCliente1");
            });

            modelBuilder.Entity<ItensVistoria>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tbItemsVistoria");

                entity.Property(e => e.CodItemVis).ValueGeneratedOnAdd();

                entity.Property(e => e.NomeItemVis)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NomeLocal)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ParamItem)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodItemNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.CodItem)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbItemsVistoria_tbItens");

                entity.HasOne(d => d.CodLocalNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.CodLocal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbItemsVistoria_tbLocal");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.CodItem);

                entity.ToTable("tbItens");

                entity.Property(e => e.NomeItem)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Norma)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Local>(entity =>
            {
                entity.HasKey(e => e.CodLocal);

                entity.ToTable("tbLocal");

                entity.Property(e => e.NomeBloco)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NomeLocal)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodBlocoNavigation)
                    .WithMany(p => p.Locals)
                    .HasForeignKey(d => d.CodBloco)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbLocal_tbBloco");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tbStatus");

                entity.Property(e => e.CodStatus).ValueGeneratedOnAdd();

                entity.Property(e => e.Gravidade)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.NomeStatus)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodItemNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.CodItem)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbStatus_tbItens");
            });

            modelBuilder.Entity<Vistoria>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tbVistoria");

                entity.Property(e => e.Cod).ValueGeneratedOnAdd();

                entity.Property(e => e.Conformidade)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Medidas)
                    .IsRequired()
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.NomeBloco)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NomeCliente)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NomeImg)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.NomeItem)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NomeLocal)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NomeStatus)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Param)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TipoLocal)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodControleNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.CodControle)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbVistoria_tbControleOs");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost, 1401;Database=DbSantaHelena2;User Id=SA;Password=rootAdmin123;");
            }
        }


    }
}

