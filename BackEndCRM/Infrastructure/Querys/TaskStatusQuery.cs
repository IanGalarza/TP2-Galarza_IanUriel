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
    public class TaskStatusQuery : ITaskStatusQuery
    {
        private readonly CRMDbContext _context;
        public TaskStatusQuery(CRMDbContext context)
        {
            _context = context;
        }
        public async Task<List<Domain.Models.TaskStatus>> GetAllTaskStatus()
        {
            var lista = await _context.TaskStatus
                .AsNoTracking()
                .OrderBy(t => t.Id)
                .ToListAsync();

            return lista;
        }

        public async Task<bool> TaskStatusExist(int id)
        {
            var resultado = await _context.TaskStatus
                .AnyAsync(t => t.Id == id);

            return resultado;
        }
    }
}
