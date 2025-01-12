﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KM.BackOffice.Application.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; } 

        public string? FirstName { get; set; } 

        public string? LastName { get; set; } 

        public bool IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
