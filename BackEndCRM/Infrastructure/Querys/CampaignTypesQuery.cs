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
    public class CampaignTypesQuery : ICampaignTypesQuery
    {
        private readonly CRMDbContext _context;
        public CampaignTypesQuery(CRMDbContext context)
        {
            _context = context;
        }
        public async Task<List<CampaignTypes>> GetAllCampaignTypes()
        {
            var lista = await _context.CampaignTypes
                .AsNoTracking()
                .OrderBy(c => c.Id)
                .ToListAsync();

            return lista;
        }
        public async Task<bool> CampaignTypeExist(int id)
        {
            var resultado = await _context.CampaignTypes
                .AnyAsync(c => c.Id == id);

            return resultado;
        }
    }
}
