using Application.DTOs.Response;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Service
{
    public interface IServiceUsers
    {
        Task<List<UsersResponse>> GetAllUsers();
        Task<bool> UserExist(int id);
    }
}
