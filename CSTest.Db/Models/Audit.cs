using System;
using System.Collections.Generic;

namespace CSTest.Db.Models;

public partial class Audit
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Action { get; set; }

    public DateTime? Date { get; set; }
}
