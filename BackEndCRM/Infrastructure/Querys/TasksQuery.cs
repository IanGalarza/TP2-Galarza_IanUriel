using Application.Interfaces.Query;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Querys
{
    public class TasksQuery : ITasksQuery
    {
        private readonly CRMDbContext _context;

        public TasksQuery(CRMDbContext context)
        {
            _context = context;
        }

        public async Task<Tasks> GetByIdTasks(Guid id)
        {
            var task = await _context.Tasks
                .Include(t => t.Users)
                .Include(t => t.TaskStatus)
                .FirstOrDefaultAsync(t => t.TaskID == id);

            return task;
        }
    }
}
