using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs.User
{
    public class AddUserDTO
    {
        public required string Username { get; set; }

        public string? PhoneNumber { get; set; }

        [EmailAddress]
        public required string Email { get; set; }

        [DataType(DataType.Password)]
        public required string Password { get; set; }

        public required string Role { get; set; }
    }
}
