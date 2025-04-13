using GameStore.Api.Dtos;
using Microsoft.AspNetCore.Builder;

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

        group.MapPost("/", (CreateGameDto newGame) => {
        GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
        ); 

        games.Add(game);

        return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
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
