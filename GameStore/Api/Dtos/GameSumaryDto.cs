namespace GameStore.Api.Dtos;

public record class GameSumaryDto (
    int Id, 
    string Name, 
    string Genre, 
    decimal Price,
    DateOnly ReleaseDate
);