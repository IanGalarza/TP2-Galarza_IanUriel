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
    public class UsersQuery : IUsersQuery
    {
        private readonly CRMDbContext _context;

        public UsersQuery(CRMDbContext context)
        {
            _context = context;
        }
        public async Task<List<Users>> GetAllUsers()
        {
            var lista = await _context.Users
                .AsNoTracking()
                .OrderBy(u => u.UserID)
                .ToListAsync();

            return lista;
        }

        public async Task<bool> UserExist(int id)
        {
            var resultado = await _context.Users
                .AnyAsync(u => u.UserID == id);

            return resultado;
        }
    }
}
