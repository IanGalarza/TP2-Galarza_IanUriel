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
    public class ServiceInteractions : IServiceInteractions
    {
        private readonly IInteractionsCommand _command;
        private readonly IInteractionsQuery _query;
        private readonly IMapper _mapper;
        private readonly IServiceInteractionTypes _interactionTypesService;

        public ServiceInteractions(IInteractionsCommand command, IInteractionsQuery query, IMapper mapper, IServiceInteractionTypes interactionTypesService)
        {
            _command = command;
            _query = query;
            _mapper = mapper;
            _interactionTypesService = interactionTypesService;
        }

        public async Task<InteractionsResponse> CreateInteractions(Guid id, InteractionsRequest request)
        {
            await ValidarInteraction(request);

            var interaction = _mapper.Map<Interactions>(request);

            interaction.ProjectID = id;

            await _command.Insert(interaction);

            var result = await _query.GetByIdInteractions(interaction.InteractionID);

            return _mapper.Map<InteractionsResponse>(result);
        }

        //Metodo para verificar si los argumentos son correctos
        private async Task ValidarInteraction(InteractionsRequest request)
        {
            bool validarInteractionType = await _interactionTypesService.InteractionTypeExist(request.InteractionType);

            if (string.IsNullOrWhiteSpace(request.Notes)) { throw new InvalidArgumentsException("La nota ingresada no es valida."); }

            if (!int.TryParse(request.InteractionType.ToString(), out _) || !validarInteractionType) { throw new InvalidArgumentsException("El tipo de interaccion ingresado no es valido."); }

            if (request.Date == default) { throw new InvalidArgumentsException("La fecha de interaccion ingresada no es valida."); }
        }
    }
}
