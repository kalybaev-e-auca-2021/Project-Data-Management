using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProjectExtensions
{
    public class ProjectLookUpDto
    {
        public IEnumerable<ProjectDto> Projects { get; set; } = new List<ProjectDto>();
    }
}
