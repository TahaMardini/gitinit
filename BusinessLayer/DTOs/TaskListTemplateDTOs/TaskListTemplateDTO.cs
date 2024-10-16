using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs.TaskListTemplateDTOs
{
    public class TaskListTemplateDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? TempName { get; set; }
    }
}
