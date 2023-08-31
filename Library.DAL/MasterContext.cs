using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Library.DAL;

public partial class MasterContext : DbContext
{
    public MasterContext()
    {
    }

    public MasterContext(DbContextOptions<MasterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autor> Autors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BooksAutorsRelation> BooksAutorsRelations { get; set; }

    public virtual DbSet<DocumentType> DocumentTypes { get; set; }

    public virtual DbSet<Librarian> Librarians { get; set; }

    public virtual DbSet<PublishingCodeType> PublishingCodeTypes { get; set; }

    public virtual DbSet<Reader> Readers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=MARS;Initial Catalog=master;Integrated Security=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Autors__3214EC2772B8F1C0");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Birthday).HasColumnType("date");
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.SecondName).HasMaxLength(255);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Books__3214EC27DC7A646B");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CountryPublish)
                .HasMaxLength(100)
                .HasColumnName("Country Publish");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.PublishingCode).HasColumnName("Publishing Code");
            entity.Property(e => e.PublishingCodeType).HasColumnName("Publishing Code Type");
            entity.Property(e => e.SityPublish)
                .HasMaxLength(100)
                .HasColumnName("Sity Publish");
            entity.Property(e => e.Year).HasColumnType("date");

            entity.HasOne(d => d.AutorsNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.Autors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Books__Autors__320C68B7");

            entity.HasOne(d => d.PublishingCodeTypeNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.PublishingCodeType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Books__Publishin__33008CF0");
        });

        modelBuilder.Entity<BooksAutorsRelation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BooksAut__3214EC2792EF1CF1");

            entity.ToTable("BooksAutorsRelation");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AutorId).HasColumnName("AutorID");
            entity.Property(e => e.BookId).HasColumnName("BookID");

            entity.HasOne(d => d.Autor).WithMany(p => p.BooksAutorsRelations)
                .HasForeignKey(d => d.AutorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BooksAuto__Autor__35DCF99B");

            entity.HasOne(d => d.Book).WithMany(p => p.BooksAutorsRelations)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BooksAuto__BookI__36D11DD4");
        });

        modelBuilder.Entity<DocumentType>(entity =>
        {
            entity.HasKey(e => e.Type).HasName("PK__Document__F9B8A48A25C39098");

            entity.Property(e => e.Type).HasMaxLength(100);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
        });

        modelBuilder.Entity<Librarian>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Libraria__3214EC275880F892");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Login).HasMaxLength(255);
            entity.Property(e => e.Mail).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
        });

        modelBuilder.Entity<PublishingCodeType>(entity =>
        {
            entity.HasKey(e => e.CodeType).HasName("PK__Publishi__D07465C11279A326");

            entity.Property(e => e.CodeType).ValueGeneratedNever();
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
        });

        modelBuilder.Entity<Reader>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Readers__3214EC27DEC054CC");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DocumentType).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Login).HasMaxLength(255);
            entity.Property(e => e.Mail).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(255);

            entity.HasOne(d => d.DocumentTypeNavigation).WithMany(p => p.Readers)
                .HasForeignKey(d => d.DocumentType)
                .HasConstraintName("FK__Readers__Documen__39AD8A7F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
