using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessLayerDLL.Models
{
    public class TemplateGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }

        [StringLength(100)]
        public string? GroupName { get; set; }

        [ForeignKey("TaskListTemplate")]
        public int TemplateID { get; set; }

        public TaskListTemplate? TaskListTemplate { get; set; }

        public ICollection<GroupTask> GroupTasks { get; set; }
    }
}
