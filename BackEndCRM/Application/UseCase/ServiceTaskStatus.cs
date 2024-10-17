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
    public class ServiceTaskStatus : IServiceTaskStatus
    {
        private readonly ITaskStatusQuery _query;
        private readonly IMapper _mapper;

        public ServiceTaskStatus(ITaskStatusQuery query, IMapper mapper)
        {
            _query = query;
            _mapper = mapper;
        }
        public async Task<List<GenericResponse>> GetAllTaskStatus()
        {
            var list = await _query.GetAllTaskStatus();

            return _mapper.Map<List<GenericResponse>>(list);
        }

        public async Task<bool> TaskStatusExist(int id)
        {
            var result = await _query.TaskStatusExist(id);

            return result;
        }
    }
}



