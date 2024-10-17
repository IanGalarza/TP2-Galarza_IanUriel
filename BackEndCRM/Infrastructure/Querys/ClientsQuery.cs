
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
    public class ClientsQuery : IClientsQuery
    {
        private readonly CRMDbContext _context;
        public ClientsQuery(CRMDbContext context)
        {
            _context = context;
        }
        public async Task<List<Clients>> GetAllClients()
        {
            var lista = await _context.Clients
                .AsNoTracking()
                .OrderBy(c => c.ClientID)
                .ToListAsync();

            return lista;
        }
        public async Task<bool> ClientExist(int id)
        {
            var resultado = await _context.Clients
                .AnyAsync(c => c.ClientID == id);

            return resultado;
        }
    }
}
