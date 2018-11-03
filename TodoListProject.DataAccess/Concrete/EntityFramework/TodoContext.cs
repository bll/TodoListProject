using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TodoListProject.Entities.Concrete;

namespace TodoListProject.DataAccess.Concrete.EntityFramework
{
   public class TodoContext : DbContext
    {
        public static string ConnectionString { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
