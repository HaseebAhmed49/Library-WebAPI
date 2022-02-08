﻿using System;
using Microsoft.EntityFrameworkCore;
using my_books_V1._0.Data.Models;

namespace my_books_V1._0.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book_Author>()
                .HasOne(b => b.Book)
                .WithMany(ba => ba.Book_Authors)
                .HasForeignKey(bi => bi.BookId);

            modelBuilder.Entity<Book_Author>()
    .HasOne(b => b.Author)
    .WithMany(ba => ba.Book_Authors)
    .HasForeignKey(bi => bi.AuthorId);

            modelBuilder.Entity<Log>()
                .HasKey(n => n.Id);
                

        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Book_Author> Books_Authors { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Log> Logs { get; set; }

    }
}