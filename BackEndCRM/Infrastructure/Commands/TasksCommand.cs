using Application.Interfaces.Command;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Commands
{
    public class TasksCommand : ITasksCommand
    {
        private readonly CRMDbContext _context;

        public TasksCommand(CRMDbContext context)
        {
            _context = context;
        }

        public async Task Insert(Tasks task)
        {
            await _context.Tasks.AddAsync(task);

            await _context.SaveChangesAsync();
        }
        public async Task Update(Tasks task)
        {
            _context.Tasks.Update(task);

            await _context.SaveChangesAsync();
        }
    }
}
