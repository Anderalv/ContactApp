using System;
using Microsoft.EntityFrameworkCore;
using TestApp.Models;

namespace TestApp.Data
{
    public class  AppDbContent : DbContext
    {
        public AppDbContent(DbContextOptions<AppDbContent> options) : base(options)
        {
        }
        
        public AppDbContent()
        {
            Database.EnsureCreated();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;user=root;password=123456789;database=TestApp", 
                new MySqlServerVersion(new Version(8, 0, 11))
            );
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}