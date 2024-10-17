using System;
using System.Collections.Generic;

namespace KM.BackOffice.Database.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string HashPassword { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public bool? IsDeleted { get; set; }
}
