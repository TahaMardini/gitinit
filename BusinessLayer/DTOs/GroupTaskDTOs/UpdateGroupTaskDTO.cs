using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs.GroupTaskDTOs
{
    public class UpdateGroupTaskDTO
    {
        public int Id { get; set; }

        public string? TaskName { get; set; }

        public string? TaskDescription { get; set; }

        public int? DependancyTaskID { get; set; }
    }
}
