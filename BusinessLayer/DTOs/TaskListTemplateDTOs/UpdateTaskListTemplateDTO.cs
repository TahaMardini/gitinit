using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs.TaskListTemplateDTOs
{
    public class UpdateTaskListTemplateDTO
    {
        public int Id { get; set; }
        public required string TempName { get; set; }
    }
}
