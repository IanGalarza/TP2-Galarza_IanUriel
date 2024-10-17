using Application.DTOs.Response;
using Application.Interfaces.Query;
using Application.Interfaces.Service;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase
{
    public class ServiceUsers : IServiceUsers
    {
        private readonly IUsersQuery _query;
        private readonly IMapper _mapper;

        public ServiceUsers(IUsersQuery query, IMapper mapper)
        {
            _query = query;
            _mapper = mapper;
        }
        public async Task<List<UsersResponse>> GetAllUsers()
        {
            var list = await _query.GetAllUsers();

            return _mapper.Map<List<UsersResponse>>(list);
        }

        public async Task<bool> UserExist(int id)
        {
            var result = await _query.UserExist(id);

            return result;
        }
    }
}
