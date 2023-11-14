using System;
using System.Collections.Generic;

namespace CSTest.Db.Models;

public partial class Product
{
    public int Id { get; set; }

    public string ProductName { get; set; } = null!;

    public int Amount { get; set; }

    public double Price { get; set; }
}
