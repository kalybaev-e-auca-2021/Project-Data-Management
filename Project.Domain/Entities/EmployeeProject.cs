using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EmployeeProject
    {
        public Guid Id { get; set; }

        //[ForeignKey("EmployeeId")]
        //[InverseProperty("Employees")]
        //public virtual Employee? Employee { get; set; }

        public Guid EmployeeId { get; set; }

        //[ForeignKey("ProjectId")]
        //[InverseProperty("Employees")]
        //public virtual Project? Project { get; set; }

        public Guid ProjectId { get; set; }
    }
}
