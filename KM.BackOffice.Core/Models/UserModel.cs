using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KM.BackOffice.Core.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public bool IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
