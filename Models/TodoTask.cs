using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp1.Models
{
    public class TodoTask
    {
        public int id { get; set; }
        [Required]
        public string Content { get; set; }
        public string Date { get; set; }
    }
}
