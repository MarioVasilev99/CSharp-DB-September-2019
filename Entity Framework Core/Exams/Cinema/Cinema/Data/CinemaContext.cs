﻿namespace Cinema.Data
{
    using Microsoft.EntityFrameworkCore;

    using Cinema.Data.Models;

    public class CinemaContext : DbContext
    {
        public CinemaContext()  { }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Hall> Halls { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Projection> Projections { get; set; }

        public DbSet<Seat> Seats { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public CinemaContext(DbContextOptions options)
            : base(options)   { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }
    }
}