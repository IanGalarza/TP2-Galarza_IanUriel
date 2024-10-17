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
    public class ServiceProjects : IServiceProjects
    {
        private readonly IProjectsCommand _command;
        private readonly IProjectsQuery _query;
        private readonly IMapper _mapper;
        private readonly IServiceInteractions _interactionService;
        private readonly IServiceTasks _tasksService;
        private readonly IServiceClients _clientsServices;
        private readonly IServiceCampaignTypes _campaignTypesService;

        public ServiceProjects(IProjectsCommand command, IProjectsQuery query, IMapper mapper, IServiceInteractions interactionsservice, IServiceTasks tasksService, IServiceClients serviceClients, IServiceCampaignTypes serviceCampaignTypes)
        {
            _command = command;
            _query = query;
            _mapper = mapper;
            _interactionService = interactionsservice;
            _tasksService = tasksService;
            _clientsServices = serviceClients;
            _campaignTypesService = serviceCampaignTypes;
        }

        public async Task<List<ProjectResponse>> GetAllProjects(string? name, int? campaign, int? client, int? offset, int? size)
        {
            ValidarPaginacion(offset, size);

            var list = await _query.GetAllProjects(name, campaign, client, offset, size);

            return _mapper.Map<List<ProjectResponse>>(list);
        }

        public async Task<ProjectDetails> CreateProject(ProjectRequest request)
        {
            await ValidarProject(request);

            await ValidarProjectName(request.Name);

            var project = _mapper.Map<Projects>(request);
            
            project.CreateDate = DateTime.Now;

            await _command.Insert(project);

            var result = await _query.GetByIdProject(project.ProjectID);

            return _mapper.Map<ProjectDetails>(result);
        }

        public async Task<ProjectDetails> GetByIdProject(Guid id)
        {
            var result = await _query.GetByIdProject(id);

            if (result == null)
            {
                throw new InvalidValueException("No se encontro ningun proyecto con la ID " + id);
            }

            return _mapper.Map<ProjectDetails>(result);
        }

        public async Task<InteractionsResponse> AddNewInteraction(Guid id, InteractionsRequest request)
        {
            var project = await _query.GetByIdProject(id);

            if (project == null)
            {
                throw new InvalidValueException("La ID " + id + " no se encuentra asociada a ningun proyecto.");
            }

            var result = await _interactionService.CreateInteractions(id, request);

            project.UpdateDate = DateTime.Now;

            await _command.Update(project);

            return result;
        }

        public async Task<TasksResponse> AddNewTask(Guid id, TasksRequest request)
        {
            var project = await _query.GetByIdProject(id);

            if (project == null)
            {
                throw new InvalidValueException("La ID " + id + " no se encuentra asociada a ningun proyecto.");
            }

            var result = await _tasksService.CreateTasks(id, request);

            project.UpdateDate = DateTime.Now; 

            await _command.Update(project);

            return result;
        }
        public async Task<TasksResponse> UpdateTasks(Guid id, TasksRequest request)
        {
            return await _tasksService.UpdateTasks(id,request);
        }

        //Metodo para verificar si los argumentos son correctos
        private async Task ValidarProject(ProjectRequest request)
        {
            var validarClient = await _clientsServices.ClientExist(request.Client);

            var validarCampaignType = await _campaignTypesService.CampaignTypeExist(request.CampaignType);

            if (string.IsNullOrWhiteSpace(request.Name)) { throw new InvalidArgumentsException("El nombre ingresado no es valido."); }

            if (!int.TryParse(request.Client.ToString(), out _) || !validarClient ) { throw new InvalidArgumentsException("El Cliente ingresado no es valido."); }

            if (!int.TryParse(request.CampaignType.ToString(), out _) || !validarCampaignType) { throw new InvalidArgumentsException("El tipo de campaña ingresado no es valido."); }

            if (request.Start == default) { throw new InvalidArgumentsException("La fecha de inicio ingresada no es valida."); }

            if (request.End <= request.Start) { throw new InvalidArgumentsException("La fecha de finalizacion ingresada no es valida."); }
        }

        //Metodo para verificar si existe el nombre del proyecto
        private async Task ValidarProjectName(string name)
        {
            bool nombreProyectoExiste = await _query.NameExist(name);

            if (nombreProyectoExiste)
            {
                throw new Conflict("El Nombre del Proyecto Ya Existe.");
            }
        }

        //Validar que no se haya ingresado un valor erroneo para la paginacion
        private void ValidarPaginacion(int? offset, int? size)
        {
            if (offset < 0) { throw new InvalidArgumentsException("El offset no puede ser negativo."); }

            if (size < 0) { throw new InvalidArgumentsException("El size no puede ser negativo."); }
        }
    }
}


