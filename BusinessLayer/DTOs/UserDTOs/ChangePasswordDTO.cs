using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs.UserDTOs
{
    public class ChangePasswordDTO
    {
        public required string UserId {  get; set; }
        public required string CurrentPassword { get; set; }
        public required string NewPassword { get; set; }
    }
}
