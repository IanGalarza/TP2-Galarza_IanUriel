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
    public class ServiceInteractionTypes : IServiceInteractionTypes
    {
        private readonly IInteractionTypesQuery _query;
        private readonly IMapper _mapper;

        public ServiceInteractionTypes(IInteractionTypesQuery query, IMapper mapper)
        {
            _query = query;
            _mapper = mapper;
        }
        public async Task<List<GenericResponse>> GetAllInteractionTypes()
        {
            var list = await _query.GetAllInteractionTypes();

            return _mapper.Map<List<GenericResponse>>(list);
        }

        public async Task<bool> InteractionTypeExist(int id)
        {
            var result = await _query.InteractionTypeExist(id);

            return result;
        }
    }
}
