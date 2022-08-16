using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp1.Context;
using TodoApp1.Models;

namespace TodoApp1.Controllers
{
    public class TodoController : Controller
    {
        private readonly TodoContext context;
        public TodoController(TodoContext context)
        {
            this.context = context;
        }
        // Get
        public async Task<ActionResult> Index()
        {
            IQueryable<TodoTask> items = from i in context.todoTasks orderby i.id select i;

            List<TodoTask> todos = await items.ToListAsync();

            return View(todos);
        }
        // Get todo create
        public IActionResult Create() => View();

        // Post Create page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TodoTask item)
        {
            if (ModelState.IsValid)
            {
                context.Add(item);
                await context.SaveChangesAsync();

                TempData["Success"] = "The Item has been added";
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // Edit Get
        public async Task<IActionResult> Edit(int id)
        {
            TodoTask item = await context.todoTasks.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        // Edit Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TodoTask item)
        {
            if (ModelState.IsValid)
            {
                context.Update(item);
                await context.SaveChangesAsync();

                TempData["Success"] = "The Item has been updated";
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // Delete post
        public async Task<IActionResult> Delete(int id)
        {
            TodoTask item = await context.todoTasks.FindAsync(id);
            if (item == null)
            {
                TempData["Error"] = "Item its not exit";
            }
            else
            {
                context.todoTasks.Remove(item);
                await context.SaveChangesAsync();
                TempData["Success"] = " Item has been deleted";

            }

            return RedirectToAction("Index");
        }
        // Delete All
        public async Task<IActionResult> DeleteAll(TodoTask items)
        {
            if (items == null)
            {
                TempData["Error"] = "No items in Database";
            }
            else
            {
                context.todoTasks.RemoveRange(context.todoTasks);
                await context.SaveChangesAsync();
                TempData["Success"] = "All items in Database deleted";
            }

            return RedirectToAction("Index");
        }

        // find get
       
        public async Task<IActionResult> Search(int id)
        {
            TodoTask DbEntry = await context.todoTasks.FirstOrDefaultAsync(items => items.Content == id.ToString());
            await context.SaveChangesAsync();
            TempData["Found"] = "Item has been found";
            return RedirectToAction("Index");
        }
   
    }

}

