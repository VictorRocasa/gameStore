using System;
using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
            new {Id = 1, Name = "Luta"},
            new {Id = 2, Name = "RPG"},
            new {Id = 3, Name = "Esporte"},
            new {Id = 4, Name = "Corrida"},
            new {Id = 5, Name = "Simulação"}
        );
    }
}
