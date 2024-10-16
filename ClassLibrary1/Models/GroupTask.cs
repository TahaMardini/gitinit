using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessLayerDLL.Models
{
    public class GroupTask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }

        [Required]
        [StringLength(100)]
        public string TaskName { get; set; }

        [StringLength(500)]
        public string TaskDescription { get; set; }

        [ForeignKey("TemplateGroup")]
        public int GroupID { get; set; }

        [ForeignKey("DependencyTask")]
        public int? DependancyTaskID { get; set; }

        public TemplateGroup TemplateGroups { get; set; }

        public GroupTask DependencyTask { get; set; }
    }
}
