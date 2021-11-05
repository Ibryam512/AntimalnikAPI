
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using AntimalnikAPI.Models;

namespace AntimalnikAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
