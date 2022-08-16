using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp1.Models;

namespace TodoApp1.Context
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext>options) :base(options)
        {
                
        }
        public DbSet<TodoTask> todoTasks { get; set; }
        

        

    }
}
