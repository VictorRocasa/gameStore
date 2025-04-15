using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints 
{

const string GetGameEndpointName = "GetGame";

private static readonly List<GameDto> games = [
    new (
        1,
        "Street Fighter II",
        "Fighting",
        19.99M,
        new DateOnly(1992, 7, 15)
    ),
    new (
        2,
        "Final Fantasy XIX",
        "RPG",
        59.99M,
        new DateOnly(2010, 9, 30)
    ),
    new (
        3,
        "FIFA 23",
        "Sports",
        69.99M,
        new DateOnly(2022, 9, 27)
    ),
];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app) {
        var group = app.MapGroup("games")
        .WithParameterValidation();

        group.MapGet("/", () => games);

        group.MapGet("/{id}", (int id) => {
            GameDto? game = games.Find(game => game.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);
            
        }).WithName(GetGameEndpointName);

        group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) => {
        Game game = new() 
        {
                Name = newGame.Name,
                Genre = dbContext.Genres.Find(newGame.GenreId),
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate
        }; 

        dbContext.Games.Add(game);
        dbContext.SaveChanges();

        GameDto gameDto = new GameDto (
            game.Id,
            game.Name,
            game.Genre!.Name,
            game.Price,
            game.ReleaseDate
        );  

        return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, gameDto);
        });

        group.MapPut("/{id}", (int id, UpdateGameDto updateGame) => {
            var index = games.FindIndex(game => game.Id == id);

            if (index == -1) {
                return Results.NotFound();
            }

            games[index] = new GameDto(
                id,
                updateGame.Name,
                updateGame.Genre,
                updateGame.Price,
                updateGame.ReleaseDate
            );

            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id) => {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });

        return group;
    }

}
