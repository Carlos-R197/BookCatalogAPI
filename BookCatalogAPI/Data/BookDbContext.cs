using Microsoft.EntityFrameworkCore;
using BookCatalogAPI.Entities;
using BookCatalogAPI.Classes;

namespace BookCatalogAPI.Data;

public class BookDbContext : DbContext
{
    private readonly IConfiguration _config;
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<BookAuthor> BookAuthors => Set<BookAuthor>();
    public DbSet<BookGenre> BookGenres => Set<BookGenre>();

    public BookDbContext(DbContextOptions<BookDbContext> options, IConfiguration config) : base(options)
    {
        _config = config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_config.GetConnectionString("BookCatalogConnection"));
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<DateOnly>()
            .HaveConversion<DateOnlyConverter>()
            .HaveColumnType("date");

        base.ConfigureConventions(builder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Fluent Api Configurations
        modelBuilder.Entity<Book>()
            .Property(b => b.Title)
            .HasMaxLength(255)
            .IsRequired();
        modelBuilder.Entity<Book>()
            .Property(b => b.PublicationDate)
            .IsRequired();
        modelBuilder.Entity<Book>()
            .Property(b => b.ISBN)
            .HasMaxLength(13)
            .IsRequired();

        modelBuilder.Entity<Author>()
            .Property(a => a.Name)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<Genre>()
            .Property(g => g.Name)
            .HasMaxLength(50)
            .IsRequired();
        modelBuilder.Entity<Genre>()
            .Property(g => g.Description)
            .HasMaxLength(255);

        modelBuilder.Entity<BookAuthor>()
            .HasKey(ba => new { ba.BookId, ba.AuthorId });
        modelBuilder.Entity<BookAuthor>()
            .HasOne<Book>(ba => ba.Book)
            .WithMany(b => b.BookAuthors)
            .HasForeignKey(ba => ba.BookId);
        modelBuilder.Entity<BookAuthor>()
            .HasOne<Author>(ba => ba.Author)
            .WithMany(a => a.BookAuthors)
            .HasForeignKey(ba => ba.AuthorId);

        modelBuilder.Entity<BookGenre>()
            .HasKey(bg => new { bg.BookId, bg.GenreId });
        modelBuilder.Entity<BookGenre>()
            .HasOne<Book>(bg => bg.Book)
            .WithMany(b => b.BookGenres)
            .HasForeignKey(bg => bg.BookId);
        modelBuilder.Entity<BookGenre>()
            .HasOne<Genre>(bg => bg.Genre)
            .WithMany(g => g.BookGenres)
            .HasForeignKey(bg => bg.GenreId);

        base.OnModelCreating(modelBuilder);
    }
}