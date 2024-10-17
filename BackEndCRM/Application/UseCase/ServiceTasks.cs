using Application.DTOs.Response;
using Application.Interfaces.Command;
using Application.Interfaces.Query;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Application.Exceptions;
using Application.DTOs.Request;
using Application.Interfaces.Service;

namespace Application.UseCase
{
    public class ServiceTasks : IServiceTasks
    {
        private readonly ITasksCommand _command;
        private readonly ITasksQuery _query;
        private readonly IMapper _mapper;
        private readonly IServiceUsers _usersService;
        private readonly IServiceTaskStatus _taskStatusService;

        public ServiceTasks(ITasksCommand command, ITasksQuery query, IMapper mapper, IServiceUsers usersService, IServiceTaskStatus taskStatusService)
        {
            _command = command;
            _query = query;
            _mapper = mapper;
            _usersService = usersService;
            _taskStatusService = taskStatusService;
        }

        public async Task<TasksResponse> CreateTasks(Guid id, TasksRequest request)
        {
            await ValidarTask(request);

            var task = _mapper.Map<Tasks>(request);

            task.ProjectID = id;

            task.CreateDate = DateTime.Now;

            await _command.Insert(task);

            var result = await _query.GetByIdTasks(task.TaskID);

            return _mapper.Map<TasksResponse>(result);
        }

        public async Task<TasksResponse> UpdateTasks(Guid id, TasksRequest request)
        {
            var task = await _query.GetByIdTasks(id);

            if (task == null)
            {
                throw new InvalidValueException("No se encontro ninguna Task con la ID " + id);
            }

            await ValidarTask(request);

            _mapper.Map(request, task);

            task.UpdateDate = DateTime.Now;

            await _command.Update(task);

            var result = await _query.GetByIdTasks(id);

            return _mapper.Map<TasksResponse>(result);
        }

        //Metodo para verificar si los argumentos son correctos
        private async Task ValidarTask(TasksRequest request)
        {
            var validarUser = await _usersService.UserExist(request.User);

            var validarStatus = await _taskStatusService.TaskStatusExist(request.Status);

            if (string.IsNullOrWhiteSpace(request.Name)) { throw new InvalidArgumentsException("El nombre ingresado no es valido."); }

            if (!int.TryParse(request.User.ToString(), out _) || !validarUser) { throw new InvalidArgumentsException("El usuario ingresado no es valido."); }

            if (!int.TryParse(request.Status.ToString(), out _) || !validarStatus) { throw new InvalidArgumentsException("El estado ingresado no es valido."); }

            if (request.DueDate == default) { throw new InvalidArgumentsException("La fecha de vencimiento de la tarea ingresada no es valida."); }
        }
    }
}
