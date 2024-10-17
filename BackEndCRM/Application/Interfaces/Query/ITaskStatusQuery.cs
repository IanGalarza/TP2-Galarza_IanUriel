using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Query
{
    public interface ITaskStatusQuery
    {
        Task<List<Domain.Models.TaskStatus>> GetAllTaskStatus();
        Task<bool> TaskStatusExist(int id);
    }
}
