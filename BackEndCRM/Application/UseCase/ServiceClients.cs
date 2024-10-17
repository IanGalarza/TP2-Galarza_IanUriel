using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Exceptions;
using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Interfaces.Service;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase
{
    public class ServiceClients : IServiceClients
    {
        private readonly IClientsCommand _command;
        private readonly IClientsQuery _query;
        private readonly IMapper _mapper;
        public ServiceClients(IClientsCommand command, IClientsQuery query, IMapper mapper)
        {
            _command = command;
            _query = query;
            _mapper = mapper;
        }

        public async Task<ClientsResponse> CreateClient(ClientsRequest request)
        {
            ValidarClient(request);

            var client = _mapper.Map<Clients>(request);

            client.CreateDate = DateTime.Now;

            await _command.Insert(client);

            return _mapper.Map<ClientsResponse>(client);
        }

        public async Task<List<ClientsResponse>> GetAllClients()
        {
            var list = await _query.GetAllClients();

            return _mapper.Map<List<ClientsResponse>>(list);
        }
        public async Task<bool> ClientExist(int id)
        {
            var result = await _query.ClientExist(id);

            return result;
        }

        //Metodo para verificar si los argumentos son correctos
        private void ValidarClient(ClientsRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name)) { throw new InvalidArgumentsException("El nombre ingresado no es valido."); }

            if (string.IsNullOrWhiteSpace(request.Email)) { throw new InvalidArgumentsException("El email ingresado no es valido."); }

            if (string.IsNullOrWhiteSpace(request.Company)) { throw new InvalidArgumentsException("La compañia ingresada no es valida."); }

            if (string.IsNullOrWhiteSpace(request.Phone)) { throw new InvalidArgumentsException("El telefono ingresado no es valido."); }

            if (string.IsNullOrWhiteSpace(request.Address)) { throw new InvalidArgumentsException("La direccion ingresada no es valida."); }
        }
    }
}

