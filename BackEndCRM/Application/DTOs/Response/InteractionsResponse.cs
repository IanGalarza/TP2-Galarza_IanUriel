using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Response
{
    public class InteractionsResponse
    {
        public Guid Id { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
        public Guid ProjectId { get; set; }
        public GenericResponse InteractionType { get; set; }
    }
}
