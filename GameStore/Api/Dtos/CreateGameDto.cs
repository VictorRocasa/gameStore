using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record class CreateGameDto(
    [Required][StringLength(50)] string Name, 
    [Required] int GenreId, 
    [Range(1,100000000)] decimal Price,
    DateOnly ReleaseDate
);