using System.ComponentModel.DataAnnotations;
namespace HTPBE.Domain.Enums
{
    public enum PriorityEnums
    {
        [Display(ResourceType = typeof(string), Name = "High")] High = 1,
        [Display(ResourceType = typeof(string), Name = "Medium")] Medium = 2,
        [Display(ResourceType = typeof(string), Name = "Low")] Low = 3,
    }
}