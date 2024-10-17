using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Query
{
    public interface IClientsQuery
    {
        Task<List<Clients>> GetAllClients();
        Task<bool> ClientExist(int id);
    }
}
