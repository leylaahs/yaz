﻿using eBusiness.Models.Base;

namespace eBusiness.Models
{
    public class Position:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}
