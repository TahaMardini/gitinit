using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs.TemplateGroupDTO
{
    public class UpdateTemplateGroupDTO
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public int TemplateID { get; set; }
    }
}
