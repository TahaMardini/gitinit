using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessLayerDLL.Models
{
    public class TaskListTemplate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }

        [StringLength(100)]
        public string? TempName { get; set; }

        public ICollection<TemplateGroup>? TemplateGroups { get; set; }
    }
}
