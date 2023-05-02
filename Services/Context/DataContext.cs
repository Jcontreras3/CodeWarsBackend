using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CodeWarsBackend.Models;

namespace CodeWarsBackend.Services.Context
{
    public class DataContext : DbContext // do i need DbContext?
    {
        public DbSet<UserModel> UserInfo { get; set; }
        public DbSet<AdminModel> AdminInfo {get; set;}


        public DataContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}