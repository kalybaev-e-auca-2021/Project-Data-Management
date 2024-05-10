using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
    public class Project
    {
        public string Name { get; set; }
        public string ClientCompanyName { get; set; }
        public string PerformerCompanyName { get; set; }
        public DateTime StartProjectDate { get; set; }
        public DateTime FinishProjectDate { get; set; }

        [InverseProperty("Project")]
        public virtual ICollection<EmployeeProject> Employees { get; set; }
    }
}
