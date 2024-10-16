using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs.GroupTaskDTOs
{
    public class GroupTaskDTO
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string TaskName { get; set; }

        public string? TaskDescription { get; set; }

        public int GroupID { get; set; }

        public string GroupName { get; set; }

        public int? DependancyTaskID { get; set; }

        public string DependancyTaskName { get; set; }

    }
}
