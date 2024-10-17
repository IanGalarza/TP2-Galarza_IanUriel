using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Response
{
    public class TasksResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public Guid ProjectId { get; set; }
        public GenericResponse Status { get; set; }
        public UsersResponse UserAssigned { get; set; }
    }
}
