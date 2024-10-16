using AccessLayerDLL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs.TemplateGroupDTO
{
    public class TemplateGroupDTO
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string GroupName { get; set; }

        public int TemplateID { get; set; }

        public string TemplateName { get; set; }
    }
}
