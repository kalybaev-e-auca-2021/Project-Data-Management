﻿
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Employee
    {
        public Guid Id;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }

        [InverseProperty("Employee")]
        public virtual ICollection<EmployeeProject> Employees { get; set; }
    }
}