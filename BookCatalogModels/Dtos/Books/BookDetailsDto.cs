namespace BookCatalogModels.Dtos;

public record BookDetailsDto
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public DateOnly PublicationDate { get; set; }
    public string ISBN { get; set; } = "";
    public IList<AuthorDto> Authors { get; set; } = new List<AuthorDto>();
    public IList<GenreDto> Genres { get; set; } = new List<GenreDto>();
}