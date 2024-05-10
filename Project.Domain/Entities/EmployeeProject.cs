using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
    public class EmployeeProject
    {
        [ForeignKey("EmployeeId")]
        [InverseProperty("Employees")]
        public virtual Employee? Employee { get; set; }

        public int EmployeeId { get; set; }

        [ForeignKey("ProjectId")]
        [InverseProperty("Employees")]
        public virtual Project? Project { get; set; }

        public int ProjectId { get; set; }
    }
}
