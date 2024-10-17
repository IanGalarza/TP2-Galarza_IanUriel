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
    public class InteractionTypesQuery : IInteractionTypesQuery
    {
        private readonly CRMDbContext _context;

        public InteractionTypesQuery(CRMDbContext context)
        {
            _context = context;
        }
        public async Task<List<InteractionTypes>> GetAllInteractionTypes()
        {
            var lista = await _context.InteractionTypes
                .AsNoTracking()
                .OrderBy(i => i.Id)
                .ToListAsync();

            return lista;
        }

        public async Task<bool> InteractionTypeExist(int id)
        {
            var resultado = await _context.InteractionTypes
                .AnyAsync(p => p.Id == id);

            return resultado;
        }
    }
}
